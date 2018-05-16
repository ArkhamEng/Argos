namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Production.AccountFile",
                c => new
                    {
                        AccountFileId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Path = c.String(nullable: false, maxLength: 250),
                        Size = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.AccountFileId)
                .ForeignKey("Production.Account", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "Production.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        AccountTypeId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Code = c.String(maxLength: 12),
                        HireDate = c.DateTime(nullable: false),
                        HirePrice = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("Production.AccountType", t => t.AccountTypeId, cascadeDelete: true)
                .ForeignKey("Operative.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Production.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.AccountTypeId)
                .Index(t => t.ClientId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "Production.AccountType",
                c => new
                    {
                        AccountTypeId = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 4),
                        Name = c.String(maxLength: 30),
                        LocationRequired = c.Boolean(nullable: false),
                        TrackingRequired = c.Boolean(nullable: false),
                        ContactsRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AccountTypeId);
            
            CreateTable(
                "Operative.Client",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(maxLength: 200),
                        Name = c.String(nullable: false, maxLength: 150),
                        FTR = c.String(maxLength: 13),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(nullable: false, maxLength: 6),
                        InNumber = c.String(maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        IsActive = c.Boolean(nullable: false),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.BusinessName, unique: true, name: "Unq_BusinessName")
                .Index(t => t.Name, name: "IDX_Name")
                .Index(t => t.FTR, unique: true, name: "Unq_FTR")
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.Email, unique: true, name: "Unq_Email")
                .Index(t => t.CityId);
            
            CreateTable(
                "Config.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        StateId = c.Int(nullable: false),
                        Code = c.String(maxLength: 30),
                        Name = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("Config.State", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "HumanResources.Employee",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        JobPositionId = c.Int(nullable: false),
                        Gender = c.String(maxLength: 10),
                        BirthDate = c.DateTime(nullable: false),
                        SSN = c.String(maxLength: 11),
                        Commission = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        FTR = c.String(maxLength: 13),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(nullable: false, maxLength: 6),
                        InNumber = c.String(maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        IsActive = c.Boolean(nullable: false),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("HumanResources.JobPosition", t => t.JobPositionId, cascadeDelete: true)
                .Index(t => t.JobPositionId)
                .Index(t => t.Name, name: "IDX_Name")
                .Index(t => t.FTR, unique: true, name: "Unq_FTR")
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.Email, unique: true, name: "Unq_Email")
                .Index(t => t.CityId);
            
            CreateTable(
                "Finance.CashSession",
                c => new
                    {
                        CashSessionId = c.Int(nullable: false, identity: true),
                        CashRegisterId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        OpeningAmount = c.Double(nullable: false),
                        OpeningDate = c.DateTime(nullable: false),
                        ClosingAmount = c.Double(nullable: false),
                        ClosingDate = c.DateTime(nullable: false),
                        ClosingUser = c.String(),
                    })
                .PrimaryKey(t => t.CashSessionId)
                .ForeignKey("Finance.CashRegister", t => t.CashRegisterId, cascadeDelete: true)
                .ForeignKey("HumanResources.Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.CashRegisterId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "Finance.CashRegister",
                c => new
                    {
                        CashRegisterId = c.Int(nullable: false, identity: true),
                        BranchId = c.Int(nullable: false),
                        Name = c.String(maxLength: 30),
                        Description = c.String(maxLength: 100),
                        IsOpen = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CashRegisterId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
            CreateTable(
                "Config.Branch",
                c => new
                    {
                        BranchId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        ShortName = c.String(nullable: false, maxLength: 3),
                        Phone = c.String(maxLength: 15),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(nullable: false, maxLength: 6),
                        InNumber = c.String(maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        IsActive = c.Boolean(nullable: false),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.BranchId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.CityId);
            
            CreateTable(
                "HumanResources.EmployeeBranch",
                c => new
                    {
                        BranchId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.BranchId, t.EmployeeId })
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("HumanResources.Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.BranchId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "Operative.Operation",
                c => new
                    {
                        OperationId = c.Int(nullable: false, identity: true),
                        BranchId = c.Int(nullable: false),
                        OperationDate = c.DateTime(nullable: false),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.OperationId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .Index(t => t.BranchId);
            
            CreateTable(
                "Operative.OperationDetail",
                c => new
                    {
                        OperationId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.OperationId, t.ProductId })
                .ForeignKey("Operative.Operation", t => t.OperationId, cascadeDelete: true)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OperationId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Inventory.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        SubCategoryId = c.Int(nullable: false),
                        Code = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 250),
                        TradeMark = c.String(nullable: false, maxLength: 50),
                        Cost = c.Double(nullable: false),
                        Profit = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        LowestProfit = c.Double(nullable: false),
                        LowestPrice = c.Double(nullable: false),
                        MeasureUnitId = c.String(maxLength: 5),
                        IsStockable = c.Boolean(nullable: false),
                        IsTrackable = c.Boolean(nullable: false),
                        IsForSale = c.Boolean(nullable: false),
                        IsForPurchase = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("Inventory.MeasureUnit", t => t.MeasureUnitId)
                .ForeignKey("Inventory.SubCategory", t => t.SubCategoryId, cascadeDelete: true)
                .Index(t => t.SubCategoryId)
                .Index(t => t.MeasureUnitId);
            
            CreateTable(
                "Config.Compatibility",
                c => new
                    {
                        CompatibilityId = c.Int(nullable: false, identity: true),
                        ModelId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CompatibilityId)
                .ForeignKey("Config.Model", t => t.ModelId, cascadeDelete: true)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ModelId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Config.Model",
                c => new
                    {
                        ModelId = c.Int(nullable: false, identity: true),
                        MakerId = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ModelId)
                .ForeignKey("Config.Maker", t => t.MakerId, cascadeDelete: true)
                .Index(t => t.MakerId);
            
            CreateTable(
                "Config.Maker",
                c => new
                    {
                        MakerId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.MakerId);
            
            CreateTable(
                "Inventory.ExternalProduct",
                c => new
                    {
                        ExternalProductId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        ProductId = c.Int(),
                        Code = c.String(),
                        SatCode = c.String(),
                        Description = c.String(),
                        TradeMark = c.String(),
                        Unit = c.String(),
                        Price = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        MeasureUnit_MeasureUnitId = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.ExternalProductId)
                .ForeignKey("Inventory.MeasureUnit", t => t.MeasureUnit_MeasureUnitId)
                .ForeignKey("Inventory.Product", t => t.ProductId)
                .ForeignKey("Operative.Supplier", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.ProductId)
                .Index(t => t.MeasureUnit_MeasureUnitId);
            
            CreateTable(
                "Inventory.MeasureUnit",
                c => new
                    {
                        MeasureUnitId = c.String(nullable: false, maxLength: 5),
                        Name = c.String(maxLength: 15),
                        Description = c.String(maxLength: 50),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.MeasureUnitId);
            
            CreateTable(
                "Operative.Supplier",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(maxLength: 200),
                        WebSite = c.String(maxLength: 200),
                        Name = c.String(nullable: false, maxLength: 150),
                        FTR = c.String(maxLength: 13),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(nullable: false, maxLength: 6),
                        InNumber = c.String(maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        IsActive = c.Boolean(nullable: false),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.SupplierId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.BusinessName, unique: true, name: "Unq_BusinessName")
                .Index(t => t.Name, name: "IDX_Name")
                .Index(t => t.FTR, unique: true, name: "Unq_FTR")
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.Email, unique: true, name: "Unq_Email")
                .Index(t => t.CityId);
            
            CreateTable(
                "Inventory.StockMovement",
                c => new
                    {
                        StockMovementId = c.Int(nullable: false, identity: true),
                        ProductStockId = c.Int(nullable: false),
                        OperationDetailId = c.Int(nullable: false),
                        SerialId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        LastStock = c.Double(nullable: false),
                        CurrentStock = c.Double(nullable: false),
                        IsEntry = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        OperationDetail_OperationId = c.Int(),
                        OperationDetail_ProductId = c.Int(),
                        Stock_BranchId = c.Int(),
                        Stock_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.StockMovementId)
                .ForeignKey("Operative.OperationDetail", t => new { t.OperationDetail_OperationId, t.OperationDetail_ProductId })
                .ForeignKey("Inventory.Stock", t => new { t.Stock_BranchId, t.Stock_ProductId })
                .ForeignKey("Inventory.Serial", t => t.SerialId, cascadeDelete: true)
                .Index(t => t.SerialId)
                .Index(t => new { t.OperationDetail_OperationId, t.OperationDetail_ProductId })
                .Index(t => new { t.Stock_BranchId, t.Stock_ProductId });
            
            CreateTable(
                "Inventory.Serial",
                c => new
                    {
                        SerialId = c.Int(nullable: false, identity: true),
                        ProductStockId = c.Int(nullable: false),
                        SerialNumber = c.String(maxLength: 30),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        ProductStock_BranchId = c.Int(),
                        ProductStock_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.SerialId)
                .ForeignKey("Inventory.Stock", t => new { t.ProductStock_BranchId, t.ProductStock_ProductId })
                .Index(t => new { t.ProductStock_BranchId, t.ProductStock_ProductId });
            
            CreateTable(
                "Inventory.Stock",
                c => new
                    {
                        BranchId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        SerialNumber = c.String(maxLength: 20),
                        Shelf = c.String(maxLength: 10),
                        Bin = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => new { t.BranchId, t.ProductId })
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.BranchId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Inventory.PriceChange",
                c => new
                    {
                        PriceChangeId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        HighestPrice = c.Double(nullable: false),
                        LowestPrice = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.PriceChangeId)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Inventory.ProductImage",
                c => new
                    {
                        ProductImageId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 80),
                        Path = c.String(nullable: false, maxLength: 150),
                        Type = c.String(nullable: false, maxLength: 30),
                        Size = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ProductImageId)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Inventory.SubCategory",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.SubCategoryId)
                .ForeignKey("Inventory.Category", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "Inventory.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Description = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "Operative.OperationChange",
                c => new
                    {
                        OperationChangeId = c.Int(nullable: false, identity: true),
                        StatusId = c.Int(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        User = c.String(maxLength: 50),
                        PurchaseId = c.Int(),
                        SaleId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Purchase_OperationId = c.Int(),
                        Sale_OperationId = c.Int(),
                    })
                .PrimaryKey(t => t.OperationChangeId)
                .ForeignKey("Operative.Purchase", t => t.Purchase_OperationId)
                .ForeignKey("Operative.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("Operative.Sale", t => t.Sale_OperationId)
                .Index(t => t.StatusId)
                .Index(t => t.Purchase_OperationId)
                .Index(t => t.Sale_OperationId);
            
            CreateTable(
                "Operative.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "HumanResources.Commission",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        Profit = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        PayDate = c.DateTime(),
                        EmployeeId = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.CommissionId)
                .ForeignKey("HumanResources.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("Operative.Sale", t => t.CommissionId)
                .Index(t => t.CommissionId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "Finance.FinantialMovement",
                c => new
                    {
                        FinantialMovementId = c.Int(nullable: false, identity: true),
                        CashSessionId = c.Int(),
                        SaleId = c.Int(),
                        CreditNoteId = c.Int(),
                        PayMethodId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(maxLength: 30),
                        CreditNote_CreditNoteId = c.Int(),
                        DerivedNote_CreditNoteId = c.Int(),
                        Sale_OperationId = c.Int(),
                    })
                .PrimaryKey(t => t.FinantialMovementId)
                .ForeignKey("Finance.CreditNote", t => t.CreditNote_CreditNoteId)
                .ForeignKey("Finance.CreditNote", t => t.CreditNoteId)
                .ForeignKey("Finance.CashSession", t => t.CashSessionId)
                .ForeignKey("Finance.CreditNote", t => t.DerivedNote_CreditNoteId)
                .ForeignKey("Finance.PaymentMethod", t => t.PayMethodId, cascadeDelete: true)
                .ForeignKey("Operative.Sale", t => t.Sale_OperationId)
                .Index(t => t.CashSessionId)
                .Index(t => t.CreditNoteId)
                .Index(t => t.PayMethodId)
                .Index(t => t.CreditNote_CreditNoteId)
                .Index(t => t.DerivedNote_CreditNoteId)
                .Index(t => t.Sale_OperationId);
            
            CreateTable(
                "Finance.CreditNote",
                c => new
                    {
                        CreditNoteId = c.Int(nullable: false),
                        Code = c.String(maxLength: 10),
                        Ident = c.String(maxLength: 15),
                        Amount = c.Double(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Year = c.Int(nullable: false),
                        Sequential = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.CreditNoteId)
                .ForeignKey("Finance.FinantialMovement", t => t.CreditNoteId)
                .Index(t => t.CreditNoteId)
                .Index(t => new { t.Code, t.Ident }, name: "IDX_Code_Indent")
                .Index(t => new { t.Year, t.Sequential }, name: "IDX_Sequential");
            
            CreateTable(
                "Finance.PaymentMethod",
                c => new
                    {
                        PayMethodId = c.Int(nullable: false),
                        Name = c.String(maxLength: 30),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PayMethodId);
            
            CreateTable(
                "Operative.Shipping",
                c => new
                    {
                        ShippingId = c.Int(nullable: false),
                        ShipMethodId = c.Int(nullable: false),
                        DeliveryAgent = c.String(maxLength: 50),
                        ShipDate = c.DateTime(),
                        ReceptionDate = c.DateTime(),
                        ReceivedBy = c.String(),
                        ReceivedSignature = c.String(),
                    })
                .PrimaryKey(t => t.ShippingId)
                .ForeignKey("Operative.Sale", t => t.ShippingId)
                .ForeignKey("Operative.ShipMethod", t => t.ShipMethodId, cascadeDelete: true)
                .Index(t => t.ShippingId)
                .Index(t => t.ShipMethodId);
            
            CreateTable(
                "Operative.ShipMethod",
                c => new
                    {
                        ShipMethodId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 100),
                        GuideRequired = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ShipMethodId);
            
            CreateTable(
                "Operative.TransType",
                c => new
                    {
                        OperationTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.OperationTypeId);
            
            CreateTable(
                "Security.UserEmployee",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        EmployeeId = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => new { t.UserId, t.EmployeeId })
                .ForeignKey("HumanResources.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "Security.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PicturePath = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "Security.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Security.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "Security.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("Security.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "HumanResources.JobPosition",
                c => new
                    {
                        JobPositionId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 50),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.JobPositionId);
            
            CreateTable(
                "Config.Locality",
                c => new
                    {
                        LocalityId = c.Int(nullable: false, identity: true),
                        CityId = c.Int(nullable: false),
                        ZipCode = c.String(maxLength: 5),
                        Location = c.String(maxLength: 150),
                        Type = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.LocalityId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "Production.Location",
                c => new
                    {
                        LocationId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(nullable: false, maxLength: 6),
                        InNumber = c.String(maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        IsActive = c.Boolean(nullable: false),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("Production.Account", t => t.LocationId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.LocationId)
                .Index(t => t.CityId);
            
            CreateTable(
                "Config.State",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        ShortName = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "Production.Policy",
                c => new
                    {
                        PolicyId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        CutOffDay = c.Int(nullable: false),
                        NextCutOff = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        PayFreq = c.Int(nullable: false),
                        MaintFreq = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.PolicyId)
                .ForeignKey("Production.Account", t => t.PolicyId)
                .ForeignKey("Production.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.PolicyId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "Production.PolicyCharge",
                c => new
                    {
                        PolicyChargeId = c.Int(nullable: false, identity: true),
                        PolicyId = c.Int(nullable: false),
                        ScheduleDate = c.DateTime(nullable: false),
                        PayedDate = c.DateTime(),
                        Amount = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.PolicyChargeId)
                .ForeignKey("Production.Policy", t => t.PolicyId, cascadeDelete: true)
                .Index(t => t.PolicyId);
            
            CreateTable(
                "Production.PolicyHistory",
                c => new
                    {
                        PolicyHistoryId = c.Int(nullable: false, identity: true),
                        PolicyId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        MaintFreq = c.Int(nullable: false),
                        PayFreq = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.PolicyHistoryId)
                .ForeignKey("Production.Policy", t => t.PolicyId, cascadeDelete: true)
                .ForeignKey("Production.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.PolicyId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "Production.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "Production.Service",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        ServiceTypeId = c.Int(nullable: false),
                        Folio = c.String(maxLength: 10),
                        ScheduleDate = c.DateTime(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ServiceId)
                .ForeignKey("Production.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("Production.ServiceType", t => t.ServiceTypeId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.ServiceTypeId);
            
            CreateTable(
                "Production.ServiceType",
                c => new
                    {
                        ServiceTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        AllowAttachment = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceTypeId);
            
            CreateTable(
                "Security.ClientUser",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClientId = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => new { t.UserId, t.ClientId })
                .ForeignKey("Operative.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "Config.Configuration",
                c => new
                    {
                        ConfigurationId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 100),
                        Type = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ConfigurationId);
            
            CreateTable(
                "Security.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "Operative.Purchase",
                c => new
                    {
                        OperationId = c.Int(nullable: false),
                        OperationStatus_StatusId = c.Int(),
                        OperationType_OperationTypeId = c.Int(),
                        SupplierId = c.Int(nullable: false),
                        Bill = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.OperationId)
                .ForeignKey("Operative.Operation", t => t.OperationId)
                .ForeignKey("Operative.Status", t => t.OperationStatus_StatusId)
                .ForeignKey("Operative.TransType", t => t.OperationType_OperationTypeId)
                .ForeignKey("Operative.Supplier", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.OperationId)
                .Index(t => t.OperationStatus_StatusId)
                .Index(t => t.OperationType_OperationTypeId)
                .Index(t => new { t.SupplierId, t.Bill }, unique: true, name: "IDX_Supplier_Bill");
            
            CreateTable(
                "Operative.PurchaseDetail",
                c => new
                    {
                        OperationId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Purchase_OperationId = c.Int(),
                        UnitPrice = c.Double(nullable: false),
                        ReceivedQty = c.Double(nullable: false),
                        RejectedQty = c.Double(nullable: false),
                        StockedQty = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.OperationId, t.ProductId })
                .ForeignKey("Operative.OperationDetail", t => new { t.OperationId, t.ProductId })
                .ForeignKey("Operative.Purchase", t => t.Purchase_OperationId)
                .Index(t => new { t.OperationId, t.ProductId })
                .Index(t => t.Purchase_OperationId);
            
            CreateTable(
                "Operative.SaleDetail",
                c => new
                    {
                        OperationId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        PriceDiscount = c.Double(nullable: false),
                        SpecialOfferId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OperationId, t.ProductId })
                .ForeignKey("Operative.OperationDetail", t => new { t.OperationId, t.ProductId })
                .Index(t => new { t.OperationId, t.ProductId });
            
            CreateTable(
                "Operative.TransferDetail",
                c => new
                    {
                        OperationId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Comment = c.String(maxLength: 100),
                        QtySend = c.Double(nullable: false),
                        QtyOnSite = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.OperationId, t.ProductId })
                .ForeignKey("Operative.OperationDetail", t => new { t.OperationId, t.ProductId })
                .Index(t => new { t.OperationId, t.ProductId });
            
            CreateTable(
                "Operative.Sale",
                c => new
                    {
                        OperationId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        SaleCode = c.String(maxLength: 10),
                        DueDate = c.DateTime(),
                        Tax = c.Double(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        TaxAmount = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Year = c.Int(nullable: false),
                        Month = c.Int(nullable: false),
                        Sequential = c.Int(nullable: false),
                        ForShipping = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OperationId)
                .ForeignKey("Operative.Operation", t => t.OperationId)
                .ForeignKey("Operative.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("HumanResources.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("Operative.TransType", t => t.TypeId, cascadeDelete: true)
                .ForeignKey("Operative.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.OperationId)
                .Index(t => t.ClientId)
                .Index(t => t.EmployeeId)
                .Index(t => t.TypeId)
                .Index(t => t.StatusId)
                .Index(t => new { t.Year, t.Month, t.Sequential }, unique: true, name: "IDX_Sequence");
            
            CreateTable(
                "Operative.Transfer",
                c => new
                    {
                        OperationId = c.Int(nullable: false),
                        DestBranchId = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        Receiver = c.String(maxLength: 100),
                        Carrier = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.OperationId)
                .ForeignKey("Operative.Operation", t => t.OperationId)
                .ForeignKey("Config.Branch", t => t.DestBranchId, cascadeDelete: true)
                .Index(t => t.OperationId)
                .Index(t => t.DestBranchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Operative.Transfer", "DestBranchId", "Config.Branch");
            DropForeignKey("Operative.Transfer", "OperationId", "Operative.Operation");
            DropForeignKey("Operative.Sale", "StatusId", "Operative.Status");
            DropForeignKey("Operative.Sale", "TypeId", "Operative.TransType");
            DropForeignKey("Operative.Sale", "EmployeeId", "HumanResources.Employee");
            DropForeignKey("Operative.Sale", "ClientId", "Operative.Client");
            DropForeignKey("Operative.Sale", "OperationId", "Operative.Operation");
            DropForeignKey("Operative.TransferDetail", new[] { "OperationId", "ProductId" }, "Operative.OperationDetail");
            DropForeignKey("Operative.SaleDetail", new[] { "OperationId", "ProductId" }, "Operative.OperationDetail");
            DropForeignKey("Operative.PurchaseDetail", "Purchase_OperationId", "Operative.Purchase");
            DropForeignKey("Operative.PurchaseDetail", new[] { "OperationId", "ProductId" }, "Operative.OperationDetail");
            DropForeignKey("Operative.Purchase", "SupplierId", "Operative.Supplier");
            DropForeignKey("Operative.Purchase", "OperationType_OperationTypeId", "Operative.TransType");
            DropForeignKey("Operative.Purchase", "OperationStatus_StatusId", "Operative.Status");
            DropForeignKey("Operative.Purchase", "OperationId", "Operative.Operation");
            DropForeignKey("Security.AspNetUserRoles", "RoleId", "Security.AspNetRoles");
            DropForeignKey("Operative.OperationChange", "Sale_OperationId", "Operative.Sale");
            DropForeignKey("Operative.OperationChange", "StatusId", "Operative.Status");
            DropForeignKey("Security.ClientUser", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.ClientUser", "ClientId", "Operative.Client");
            DropForeignKey("Production.Service", "ServiceTypeId", "Production.ServiceType");
            DropForeignKey("Production.Service", "AccountId", "Production.Account");
            DropForeignKey("Production.Policy", "StatusId", "Production.Status");
            DropForeignKey("Production.PolicyHistory", "StatusId", "Production.Status");
            DropForeignKey("Production.Account", "StatusId", "Production.Status");
            DropForeignKey("Production.PolicyHistory", "PolicyId", "Production.Policy");
            DropForeignKey("Production.PolicyCharge", "PolicyId", "Production.Policy");
            DropForeignKey("Production.Policy", "PolicyId", "Production.Account");
            DropForeignKey("Config.City", "StateId", "Config.State");
            DropForeignKey("Production.Location", "CityId", "Config.City");
            DropForeignKey("Production.Location", "LocationId", "Production.Account");
            DropForeignKey("Config.Locality", "CityId", "Config.City");
            DropForeignKey("HumanResources.Employee", "JobPositionId", "HumanResources.JobPosition");
            DropForeignKey("Security.UserEmployee", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserRoles", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserLogins", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserClaims", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.UserEmployee", "EmployeeId", "HumanResources.Employee");
            DropForeignKey("HumanResources.Employee", "CityId", "Config.City");
            DropForeignKey("Finance.CashSession", "EmployeeId", "HumanResources.Employee");
            DropForeignKey("Finance.CashSession", "CashRegisterId", "Finance.CashRegister");
            DropForeignKey("Operative.Shipping", "ShipMethodId", "Operative.ShipMethod");
            DropForeignKey("Operative.Shipping", "ShippingId", "Operative.Sale");
            DropForeignKey("Finance.FinantialMovement", "Sale_OperationId", "Operative.Sale");
            DropForeignKey("Finance.FinantialMovement", "PayMethodId", "Finance.PaymentMethod");
            DropForeignKey("Finance.FinantialMovement", "DerivedNote_CreditNoteId", "Finance.CreditNote");
            DropForeignKey("Finance.FinantialMovement", "CashSessionId", "Finance.CashSession");
            DropForeignKey("Finance.FinantialMovement", "CreditNoteId", "Finance.CreditNote");
            DropForeignKey("Finance.CreditNote", "CreditNoteId", "Finance.FinantialMovement");
            DropForeignKey("Finance.FinantialMovement", "CreditNote_CreditNoteId", "Finance.CreditNote");
            DropForeignKey("HumanResources.Commission", "CommissionId", "Operative.Sale");
            DropForeignKey("HumanResources.Commission", "EmployeeId", "HumanResources.Employee");
            DropForeignKey("Operative.OperationChange", "Purchase_OperationId", "Operative.Purchase");
            DropForeignKey("Inventory.Product", "SubCategoryId", "Inventory.SubCategory");
            DropForeignKey("Inventory.SubCategory", "CategoryId", "Inventory.Category");
            DropForeignKey("Inventory.ProductImage", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.PriceChange", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.StockMovement", "SerialId", "Inventory.Serial");
            DropForeignKey("Inventory.StockMovement", new[] { "Stock_BranchId", "Stock_ProductId" }, "Inventory.Stock");
            DropForeignKey("Inventory.Serial", new[] { "ProductStock_BranchId", "ProductStock_ProductId" }, "Inventory.Stock");
            DropForeignKey("Inventory.Stock", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Stock", "BranchId", "Config.Branch");
            DropForeignKey("Inventory.StockMovement", new[] { "OperationDetail_OperationId", "OperationDetail_ProductId" }, "Operative.OperationDetail");
            DropForeignKey("Operative.OperationDetail", "ProductId", "Inventory.Product");
            DropForeignKey("Operative.OperationDetail", "OperationId", "Operative.Operation");
            DropForeignKey("Inventory.ExternalProduct", "SupplierId", "Operative.Supplier");
            DropForeignKey("Operative.Supplier", "CityId", "Config.City");
            DropForeignKey("Inventory.ExternalProduct", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.ExternalProduct", "MeasureUnit_MeasureUnitId", "Inventory.MeasureUnit");
            DropForeignKey("Inventory.Product", "MeasureUnitId", "Inventory.MeasureUnit");
            DropForeignKey("Config.Compatibility", "ProductId", "Inventory.Product");
            DropForeignKey("Config.Model", "MakerId", "Config.Maker");
            DropForeignKey("Config.Compatibility", "ModelId", "Config.Model");
            DropForeignKey("Operative.Operation", "BranchId", "Config.Branch");
            DropForeignKey("HumanResources.EmployeeBranch", "EmployeeId", "HumanResources.Employee");
            DropForeignKey("HumanResources.EmployeeBranch", "BranchId", "Config.Branch");
            DropForeignKey("Config.Branch", "CityId", "Config.City");
            DropForeignKey("Finance.CashRegister", "BranchId", "Config.Branch");
            DropForeignKey("Operative.Client", "CityId", "Config.City");
            DropForeignKey("Production.Account", "ClientId", "Operative.Client");
            DropForeignKey("Production.Account", "AccountTypeId", "Production.AccountType");
            DropForeignKey("Production.AccountFile", "AccountId", "Production.Account");
            DropIndex("Operative.Transfer", new[] { "DestBranchId" });
            DropIndex("Operative.Transfer", new[] { "OperationId" });
            DropIndex("Operative.Sale", "IDX_Sequence");
            DropIndex("Operative.Sale", new[] { "StatusId" });
            DropIndex("Operative.Sale", new[] { "TypeId" });
            DropIndex("Operative.Sale", new[] { "EmployeeId" });
            DropIndex("Operative.Sale", new[] { "ClientId" });
            DropIndex("Operative.Sale", new[] { "OperationId" });
            DropIndex("Operative.TransferDetail", new[] { "OperationId", "ProductId" });
            DropIndex("Operative.SaleDetail", new[] { "OperationId", "ProductId" });
            DropIndex("Operative.PurchaseDetail", new[] { "Purchase_OperationId" });
            DropIndex("Operative.PurchaseDetail", new[] { "OperationId", "ProductId" });
            DropIndex("Operative.Purchase", "IDX_Supplier_Bill");
            DropIndex("Operative.Purchase", new[] { "OperationType_OperationTypeId" });
            DropIndex("Operative.Purchase", new[] { "OperationStatus_StatusId" });
            DropIndex("Operative.Purchase", new[] { "OperationId" });
            DropIndex("Security.AspNetRoles", "RoleNameIndex");
            DropIndex("Security.ClientUser", new[] { "ClientId" });
            DropIndex("Security.ClientUser", new[] { "UserId" });
            DropIndex("Production.Service", new[] { "ServiceTypeId" });
            DropIndex("Production.Service", new[] { "AccountId" });
            DropIndex("Production.PolicyHistory", new[] { "StatusId" });
            DropIndex("Production.PolicyHistory", new[] { "PolicyId" });
            DropIndex("Production.PolicyCharge", new[] { "PolicyId" });
            DropIndex("Production.Policy", new[] { "StatusId" });
            DropIndex("Production.Policy", new[] { "PolicyId" });
            DropIndex("Production.Location", new[] { "CityId" });
            DropIndex("Production.Location", new[] { "LocationId" });
            DropIndex("Config.Locality", new[] { "CityId" });
            DropIndex("Security.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("Security.AspNetUserRoles", new[] { "UserId" });
            DropIndex("Security.AspNetUserLogins", new[] { "UserId" });
            DropIndex("Security.AspNetUserClaims", new[] { "UserId" });
            DropIndex("Security.AspNetUsers", "UserNameIndex");
            DropIndex("Security.UserEmployee", new[] { "EmployeeId" });
            DropIndex("Security.UserEmployee", new[] { "UserId" });
            DropIndex("Operative.Shipping", new[] { "ShipMethodId" });
            DropIndex("Operative.Shipping", new[] { "ShippingId" });
            DropIndex("Finance.CreditNote", "IDX_Sequential");
            DropIndex("Finance.CreditNote", "IDX_Code_Indent");
            DropIndex("Finance.CreditNote", new[] { "CreditNoteId" });
            DropIndex("Finance.FinantialMovement", new[] { "Sale_OperationId" });
            DropIndex("Finance.FinantialMovement", new[] { "DerivedNote_CreditNoteId" });
            DropIndex("Finance.FinantialMovement", new[] { "CreditNote_CreditNoteId" });
            DropIndex("Finance.FinantialMovement", new[] { "PayMethodId" });
            DropIndex("Finance.FinantialMovement", new[] { "CreditNoteId" });
            DropIndex("Finance.FinantialMovement", new[] { "CashSessionId" });
            DropIndex("HumanResources.Commission", new[] { "EmployeeId" });
            DropIndex("HumanResources.Commission", new[] { "CommissionId" });
            DropIndex("Operative.OperationChange", new[] { "Sale_OperationId" });
            DropIndex("Operative.OperationChange", new[] { "Purchase_OperationId" });
            DropIndex("Operative.OperationChange", new[] { "StatusId" });
            DropIndex("Inventory.SubCategory", new[] { "CategoryId" });
            DropIndex("Inventory.ProductImage", new[] { "ProductId" });
            DropIndex("Inventory.PriceChange", new[] { "ProductId" });
            DropIndex("Inventory.Stock", new[] { "ProductId" });
            DropIndex("Inventory.Stock", new[] { "BranchId" });
            DropIndex("Inventory.Serial", new[] { "ProductStock_BranchId", "ProductStock_ProductId" });
            DropIndex("Inventory.StockMovement", new[] { "Stock_BranchId", "Stock_ProductId" });
            DropIndex("Inventory.StockMovement", new[] { "OperationDetail_OperationId", "OperationDetail_ProductId" });
            DropIndex("Inventory.StockMovement", new[] { "SerialId" });
            DropIndex("Operative.Supplier", new[] { "CityId" });
            DropIndex("Operative.Supplier", "Unq_Email");
            DropIndex("Operative.Supplier", "Unq_Phone");
            DropIndex("Operative.Supplier", "Unq_FTR");
            DropIndex("Operative.Supplier", "IDX_Name");
            DropIndex("Operative.Supplier", "Unq_BusinessName");
            DropIndex("Inventory.ExternalProduct", new[] { "MeasureUnit_MeasureUnitId" });
            DropIndex("Inventory.ExternalProduct", new[] { "ProductId" });
            DropIndex("Inventory.ExternalProduct", new[] { "SupplierId" });
            DropIndex("Config.Model", new[] { "MakerId" });
            DropIndex("Config.Compatibility", new[] { "ProductId" });
            DropIndex("Config.Compatibility", new[] { "ModelId" });
            DropIndex("Inventory.Product", new[] { "MeasureUnitId" });
            DropIndex("Inventory.Product", new[] { "SubCategoryId" });
            DropIndex("Operative.OperationDetail", new[] { "ProductId" });
            DropIndex("Operative.OperationDetail", new[] { "OperationId" });
            DropIndex("Operative.Operation", new[] { "BranchId" });
            DropIndex("HumanResources.EmployeeBranch", new[] { "EmployeeId" });
            DropIndex("HumanResources.EmployeeBranch", new[] { "BranchId" });
            DropIndex("Config.Branch", new[] { "CityId" });
            DropIndex("Config.Branch", "Unq_Phone");
            DropIndex("Finance.CashRegister", new[] { "BranchId" });
            DropIndex("Finance.CashSession", new[] { "EmployeeId" });
            DropIndex("Finance.CashSession", new[] { "CashRegisterId" });
            DropIndex("HumanResources.Employee", new[] { "CityId" });
            DropIndex("HumanResources.Employee", "Unq_Email");
            DropIndex("HumanResources.Employee", "Unq_Phone");
            DropIndex("HumanResources.Employee", "Unq_FTR");
            DropIndex("HumanResources.Employee", "IDX_Name");
            DropIndex("HumanResources.Employee", new[] { "JobPositionId" });
            DropIndex("Config.City", new[] { "StateId" });
            DropIndex("Operative.Client", new[] { "CityId" });
            DropIndex("Operative.Client", "Unq_Email");
            DropIndex("Operative.Client", "Unq_Phone");
            DropIndex("Operative.Client", "Unq_FTR");
            DropIndex("Operative.Client", "IDX_Name");
            DropIndex("Operative.Client", "Unq_BusinessName");
            DropIndex("Production.Account", new[] { "StatusId" });
            DropIndex("Production.Account", new[] { "ClientId" });
            DropIndex("Production.Account", new[] { "AccountTypeId" });
            DropIndex("Production.AccountFile", new[] { "AccountId" });
            DropTable("Operative.Transfer");
            DropTable("Operative.Sale");
            DropTable("Operative.TransferDetail");
            DropTable("Operative.SaleDetail");
            DropTable("Operative.PurchaseDetail");
            DropTable("Operative.Purchase");
            DropTable("Security.AspNetRoles");
            DropTable("Config.Configuration");
            DropTable("Security.ClientUser");
            DropTable("Production.ServiceType");
            DropTable("Production.Service");
            DropTable("Production.Status");
            DropTable("Production.PolicyHistory");
            DropTable("Production.PolicyCharge");
            DropTable("Production.Policy");
            DropTable("Config.State");
            DropTable("Production.Location");
            DropTable("Config.Locality");
            DropTable("HumanResources.JobPosition");
            DropTable("Security.AspNetUserRoles");
            DropTable("Security.AspNetUserLogins");
            DropTable("Security.AspNetUserClaims");
            DropTable("Security.AspNetUsers");
            DropTable("Security.UserEmployee");
            DropTable("Operative.TransType");
            DropTable("Operative.ShipMethod");
            DropTable("Operative.Shipping");
            DropTable("Finance.PaymentMethod");
            DropTable("Finance.CreditNote");
            DropTable("Finance.FinantialMovement");
            DropTable("HumanResources.Commission");
            DropTable("Operative.Status");
            DropTable("Operative.OperationChange");
            DropTable("Inventory.Category");
            DropTable("Inventory.SubCategory");
            DropTable("Inventory.ProductImage");
            DropTable("Inventory.PriceChange");
            DropTable("Inventory.Stock");
            DropTable("Inventory.Serial");
            DropTable("Inventory.StockMovement");
            DropTable("Operative.Supplier");
            DropTable("Inventory.MeasureUnit");
            DropTable("Inventory.ExternalProduct");
            DropTable("Config.Maker");
            DropTable("Config.Model");
            DropTable("Config.Compatibility");
            DropTable("Inventory.Product");
            DropTable("Operative.OperationDetail");
            DropTable("Operative.Operation");
            DropTable("HumanResources.EmployeeBranch");
            DropTable("Config.Branch");
            DropTable("Finance.CashRegister");
            DropTable("Finance.CashSession");
            DropTable("HumanResources.Employee");
            DropTable("Config.City");
            DropTable("Operative.Client");
            DropTable("Production.AccountType");
            DropTable("Production.Account");
            DropTable("Production.AccountFile");
        }
    }
}
