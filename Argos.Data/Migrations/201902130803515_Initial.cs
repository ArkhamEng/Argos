namespace Argos.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Production.AccountHistory",
                c => new
                    {
                        AccountHistoryId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        Status = c.String(maxLength: 30),
                        PolicyCost = c.Double(nullable: false),
                        Comment = c.String(maxLength: 100),
                        MaintenancePeriod = c.Int(nullable: false),
                        PaymentPeriod = c.Int(nullable: false),
                        HasPolicy = c.Boolean(nullable: false),
                        HasMaintenance = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.AccountHistoryId)
                .ForeignKey("Production.Account", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "Production.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        AccountStatusId = c.Int(nullable: false),
                        AccountTypeId = c.Int(nullable: false),
                        Code = c.String(maxLength: 12),
                        PolicyCost = c.Double(nullable: false),
                        Comment = c.String(maxLength: 100),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        CutOffDate = c.DateTime(),
                        MaintPeriodId = c.Int(nullable: false),
                        PaymentPeriodId = c.Int(nullable: false),
                        HasPolicy = c.Boolean(nullable: false),
                        LastPaymentDate = c.DateTime(),
                        LastMaintenanceDate = c.DateTime(),
                        HasMaintenance = c.Boolean(nullable: false),
                        Sequential = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("Production.AccountStatus", t => t.AccountStatusId, cascadeDelete: true)
                .ForeignKey("Production.AccountType", t => t.AccountTypeId, cascadeDelete: true)
                .ForeignKey("Sales.Client", t => t.ClientId)
                .ForeignKey("Production.MaintPeriod", t => t.MaintPeriodId, cascadeDelete: true)
                .ForeignKey("Production.PaymentPeriod", t => t.PaymentPeriodId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.AccountStatusId)
                .Index(t => t.AccountTypeId)
                .Index(t => t.Code, name: "IDX_Code")
                .Index(t => t.CutOffDate, name: "IDX_CutOffDate")
                .Index(t => t.MaintPeriodId)
                .Index(t => t.PaymentPeriodId);
            
            CreateTable(
                "Production.AccountStatus",
                c => new
                    {
                        AccountStatusId = c.Int(nullable: false),
                        Code = c.String(maxLength: 10),
                        Name = c.String(maxLength: 30),
                        Description = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.AccountStatusId);
            
            CreateTable(
                "Production.AccountType",
                c => new
                    {
                        AccountTypeId = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 10),
                        Name = c.String(maxLength: 30),
                        Description = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.AccountTypeId);
            
            CreateTable(
                "Business.Entity",
                c => new
                    {
                        EntityId = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.EntityId);
            
            CreateTable(
                "Business.Address",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        AddressTypeId = c.Int(nullable: false),
                        TownId = c.String(nullable: false, maxLength: 6),
                        Street = c.String(nullable: false, maxLength: 80),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("Business.AddressType", t => t.AddressTypeId, cascadeDelete: true)
                .ForeignKey("Business.Entity", t => t.EntityId, cascadeDelete: true)
                .ForeignKey("Config.Town", t => t.TownId, cascadeDelete: true)
                .Index(t => t.EntityId)
                .Index(t => t.AddressTypeId)
                .Index(t => t.TownId);
            
            CreateTable(
                "Business.AddressType",
                c => new
                    {
                        AddressTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AddressTypeId);
            
            CreateTable(
                "Business.EmailAddress",
                c => new
                    {
                        EmailAddressId = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        EmailTypeId = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.EmailAddressId)
                .ForeignKey("Business.EmailType", t => t.EmailTypeId, cascadeDelete: true)
                .ForeignKey("Business.Entity", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId)
                .Index(t => t.EmailTypeId);
            
            CreateTable(
                "Business.EmailType",
                c => new
                    {
                        EmailTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.EmailTypeId);
            
            CreateTable(
                "Business.PhoneNumber",
                c => new
                    {
                        PhoneNumberId = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        PhoneTypeId = c.Int(nullable: false),
                        Phone = c.String(nullable: false, maxLength: 15),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.PhoneNumberId)
                .ForeignKey("Business.Entity", t => t.EntityId, cascadeDelete: true)
                .ForeignKey("Business.PhoneType", t => t.PhoneTypeId, cascadeDelete: true)
                .Index(t => t.EntityId)
                .Index(t => t.PhoneTypeId);
            
            CreateTable(
                "Business.PhoneType",
                c => new
                    {
                        PhoneTypeId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PhoneTypeId);
            
            CreateTable(
                "Security.SystemUser",
                c => new
                    {
                        SystemUserId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.SystemUserId)
                .ForeignKey("Business.Person", t => t.SystemUserId)
                .ForeignKey("Security.AspNetUsers", t => t.UserId)
                .Index(t => t.SystemUserId)
                .Index(t => t.UserId);
            
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
                "HumanResources.EmployeeBranch",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.BranchId })
                .ForeignKey("Config.Branch", t => t.BranchId)
                .ForeignKey("HumanResources.Employee", t => t.EmployeeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "Inventory.Storage",
                c => new
                    {
                        StorageId = c.Int(nullable: false, identity: true),
                        BranchId = c.Int(nullable: false),
                        Name = c.String(),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.StorageId)
                .ForeignKey("Config.Branch", t => t.BranchId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "Inventory.ItemHistory",
                c => new
                    {
                        ItemHistoryId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        Storage_StorageId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemHistoryId)
                .ForeignKey("Inventory.Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("Inventory.Storage", t => t.Storage_StorageId)
                .Index(t => t.ItemId)
                .Index(t => t.Storage_StorageId);
            
            CreateTable(
                "Inventory.Item",
                c => new
                    {
                        ItemId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        SerialNumber = c.String(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Inventory.Flow",
                c => new
                    {
                        FlowId = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        DetailId = c.Int(nullable: false),
                        Direction = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.FlowId)
                .ForeignKey("Business.Detail", t => t.DetailId, cascadeDelete: true)
                .ForeignKey("Inventory.FlowDirection", t => t.Direction, cascadeDelete: true)
                .ForeignKey("Inventory.Item", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.DetailId)
                .Index(t => t.Direction);
            
            CreateTable(
                "Business.Detail",
                c => new
                    {
                        DetailId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.DetailId)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: false)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Inventory.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        SubCategoryId = c.Int(nullable: false),
                        MeasureUnitId = c.String(maxLength: 5),
                        Code = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 250),
                        TradeMark = c.String(nullable: false, maxLength: 50),
                        SatCode = c.String(nullable: false, maxLength: 30),
                        BuyPrice = c.Double(nullable: false),
                        Profit = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        LowestProfit = c.Double(nullable: false),
                        LowestPrice = c.Double(nullable: false),
                        IsStockable = c.Boolean(nullable: false),
                        IsTrackable = c.Boolean(nullable: false),
                        IsForSale = c.Boolean(nullable: false),
                        IsForPurchase = c.Boolean(nullable: false),
                        MaxQuantity = c.Double(nullable: false),
                        MinQuantity = c.Double(nullable: false),
                        TaxedMinPrice = c.Double(nullable: false),
                        TaxedMaxPrice = c.Double(nullable: false),
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
                .Index(t => t.MeasureUnitId)
                .Index(t => new { t.Code, t.Description, t.TradeMark }, name: "IDX_Code_Description_TradeMark");
            
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
                "Inventory.MeasureUnit",
                c => new
                    {
                        MeasureUnitId = c.String(nullable: false, maxLength: 5),
                        Name = c.String(maxLength: 15),
                        Description = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.MeasureUnitId);
            
            CreateTable(
                "Inventory.PriceChange",
                c => new
                    {
                        ProductChangeId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Profit = c.Double(nullable: false),
                        LowetsProfit = c.Double(nullable: false),
                        BuyPrice = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        LowestPrice = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ProductChangeId)
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
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
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
                "Inventory.SupplierProduct",
                c => new
                    {
                        SupplierId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Code = c.String(),
                        SatCode = c.String(),
                        Cost = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => new { t.SupplierId, t.ProductId })
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("Purchasing.Supplier", t => t.SupplierId)
                .Index(t => t.SupplierId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Config.CreditStatus",
                c => new
                    {
                        CreditStatusId = c.Int(nullable: false),
                        Name = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.CreditStatusId);
            
            CreateTable(
                "Purchasing.Purchase",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Bill = c.String(nullable: false, maxLength: 10),
                        BranchId = c.Int(nullable: false),
                        ShipMethodId = c.Int(nullable: false),
                        PayFormId = c.Int(nullable: false),
                        PaymentType = c.String(maxLength: 8),
                        OrderDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        ShipDate = c.DateTime(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        TaxAmt = c.Double(nullable: false),
                        Freight = c.Double(nullable: false),
                        TotalDue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("Config.Branch", t => t.BranchId)
                .ForeignKey("Config.PayForm", t => t.PayFormId, cascadeDelete: true)
                .ForeignKey("Purchasing.Status", t => t.Status, cascadeDelete: true)
                .ForeignKey("Sales.ShipMethod", t => t.ShipMethodId, cascadeDelete: true)
                .ForeignKey("Purchasing.Supplier", t => t.SupplierId)
                .Index(t => new { t.SupplierId, t.Bill }, unique: true, name: "IDX_Supplier_Bill")
                .Index(t => t.Status)
                .Index(t => t.BranchId)
                .Index(t => t.ShipMethodId)
                .Index(t => t.PayFormId);
            
            CreateTable(
                "Config.PayForm",
                c => new
                    {
                        PayFormId = c.Int(nullable: false),
                        Name = c.String(maxLength: 8),
                    })
                .PrimaryKey(t => t.PayFormId);
            
            CreateTable(
                "Sales.Sale",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Folio = c.String(maxLength: 12),
                        IsOnline = c.Boolean(nullable: false),
                        BranchId = c.Int(nullable: false),
                        ShipMethodId = c.Int(nullable: false),
                        PayFormId = c.Int(nullable: false),
                        PaymentType = c.String(maxLength: 8),
                        OrderDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        ShipDate = c.DateTime(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        TaxAmt = c.Double(nullable: false),
                        Freight = c.Double(nullable: false),
                        TotalDue = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("Config.Branch", t => t.BranchId)
                .ForeignKey("Sales.Client", t => t.ClientId)
                .ForeignKey("HumanResources.Employee", t => t.EmployeeId)
                .ForeignKey("Config.PayForm", t => t.PayFormId, cascadeDelete: true)
                .ForeignKey("Sales.Status", t => t.Status, cascadeDelete: true)
                .ForeignKey("Sales.ShipMethod", t => t.ShipMethodId, cascadeDelete: true)
                .Index(t => t.Status)
                .Index(t => t.ClientId)
                .Index(t => t.EmployeeId)
                .Index(t => t.BranchId)
                .Index(t => t.ShipMethodId)
                .Index(t => t.PayFormId);
            
            CreateTable(
                "Sales.Status",
                c => new
                    {
                        SaleStatusId = c.Int(nullable: false),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.SaleStatusId);
            
            CreateTable(
                "Sales.ShipMethod",
                c => new
                    {
                        ShipMethodId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ShipMethodId);
            
            CreateTable(
                "Purchasing.Status",
                c => new
                    {
                        PurchaseStatusId = c.Int(nullable: false),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PurchaseStatusId);
            
            CreateTable(
                "Inventory.FlowDirection",
                c => new
                    {
                        FlowDirectionId = c.Int(nullable: false),
                        Name = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.FlowDirectionId);
            
            CreateTable(
                "Inventory.ItemStorage",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        StorageId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => new { t.ItemId, t.StorageId })
                .ForeignKey("Inventory.Item", t => t.ItemId, cascadeDelete: true)
                .ForeignKey("Inventory.Storage", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.StorageId);
            
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
                "Config.Town",
                c => new
                    {
                        TownId = c.String(nullable: false, maxLength: 6),
                        StateId = c.String(maxLength: 2),
                        Name = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.TownId)
                .ForeignKey("Config.State", t => t.StateId)
                .Index(t => t.StateId);
            
            CreateTable(
                "Config.Settlement",
                c => new
                    {
                        SettlementId = c.Int(nullable: false, identity: true),
                        TownId = c.String(maxLength: 6),
                        Code = c.String(maxLength: 6),
                        Name = c.String(maxLength: 100),
                        Type = c.String(),
                        Zone = c.String(),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.SettlementId)
                .ForeignKey("Config.Town", t => t.TownId)
                .Index(t => t.TownId)
                .Index(t => new { t.Code, t.Name }, name: "IDX_Code_Name");
            
            CreateTable(
                "Config.State",
                c => new
                    {
                        StateId = c.String(nullable: false, maxLength: 2),
                        Name = c.String(),
                        ShortName = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "Production.MaintPeriod",
                c => new
                    {
                        MaintPeriodId = c.Int(nullable: false),
                        Name = c.String(maxLength: 30),
                        Description = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.MaintPeriodId);
            
            CreateTable(
                "Production.Occurence",
                c => new
                    {
                        OccurenceId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        ScheduleDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        Comment = c.String(maxLength: 100),
                        IsDone = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.OccurenceId)
                .ForeignKey("Production.Account", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "Production.ServiceType",
                c => new
                    {
                        ServiceTypeId = c.Int(nullable: false),
                        Code = c.String(maxLength: 10),
                        Name = c.String(maxLength: 30),
                        Descriptión = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.ServiceTypeId);
            
            CreateTable(
                "Production.PaymentPeriod",
                c => new
                    {
                        PaymentPeriodId = c.Int(nullable: false),
                        Name = c.String(maxLength: 30),
                        Description = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.PaymentPeriodId);
            
            CreateTable(
                "Config.Configuration",
                c => new
                    {
                        ConfigurationId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 100),
                        Type = c.String(maxLength: 200),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ConfigurationId);
            
            CreateTable(
                "Analytic.ErroLog",
                c => new
                    {
                        ErrorLogId = c.Int(nullable: false, identity: true),
                        Action = c.String(maxLength: 50),
                        Controller = c.String(maxLength: 50),
                        Description = c.String(),
                        InsDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ErrorLogId);
            
            CreateTable(
                "Analytic.ExecutionTaskLog",
                c => new
                    {
                        ExecutionTaskLogId = c.Int(nullable: false, identity: true),
                        TaskName = c.String(maxLength: 50),
                        Comment = c.String(),
                        HasSucced = c.Boolean(nullable: false),
                        Duration = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ExecutionTaskLogId);
            
            CreateTable(
                "Security.PayMethods",
                c => new
                    {
                        PayMethodId = c.Int(nullable: false),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.PayMethodId);
            
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
                "Production.Charge",
                c => new
                    {
                        OccurenceId = c.Int(nullable: false),
                        PaymentDate = c.DateTime(),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.OccurenceId)
                .ForeignKey("Production.Occurence", t => t.OccurenceId)
                .Index(t => t.OccurenceId);
            
            CreateTable(
                "Business.Person",
                c => new
                    {
                        EntityId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        FTR = c.String(maxLength: 13),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.EntityId)
                .ForeignKey("Business.Entity", t => t.EntityId)
                .Index(t => t.EntityId);
            
            CreateTable(
                "Production.Service",
                c => new
                    {
                        OccurenceId = c.Int(nullable: false),
                        ServiceTypeId = c.Int(nullable: false),
                        StartDate = c.DateTime(),
                        CompletionDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.OccurenceId)
                .ForeignKey("Production.Occurence", t => t.OccurenceId)
                .ForeignKey("Production.ServiceType", t => t.ServiceTypeId, cascadeDelete: true)
                .Index(t => t.OccurenceId)
                .Index(t => t.ServiceTypeId);
            
            CreateTable(
                "Sales.Client",
                c => new
                    {
                        EntityId = c.Int(nullable: false),
                        BusinessName = c.String(maxLength: 200),
                        CreditStatusId = c.Int(nullable: false),
                        CreditLimit = c.Double(nullable: false),
                        CreditDays = c.Int(nullable: false),
                        CreditBalance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.EntityId)
                .ForeignKey("Business.Person", t => t.EntityId)
                .ForeignKey("Config.CreditStatus", t => t.CreditStatusId, cascadeDelete: true)
                .Index(t => t.EntityId)
                .Index(t => t.CreditStatusId);
            
            CreateTable(
                "HumanResources.Employee",
                c => new
                    {
                        EntityId = c.Int(nullable: false),
                        JobPositionId = c.Int(nullable: false),
                        Gender = c.String(maxLength: 1),
                        MaritalStatus = c.String(maxLength: 1),
                        HireDate = c.DateTime(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        SSN = c.String(maxLength: 11),
                        Commission = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EntityId)
                .ForeignKey("Business.Person", t => t.EntityId)
                .ForeignKey("HumanResources.JobPosition", t => t.JobPositionId, cascadeDelete: true)
                .Index(t => t.EntityId)
                .Index(t => t.JobPositionId);
            
            CreateTable(
                "Config.Branch",
                c => new
                    {
                        EntityId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                        ShortName = c.String(nullable: false, maxLength: 4),
                    })
                .PrimaryKey(t => t.EntityId)
                .ForeignKey("Business.Entity", t => t.EntityId)
                .Index(t => t.EntityId);
            
            CreateTable(
                "Purchasing.Supplier",
                c => new
                    {
                        EntityId = c.Int(nullable: false),
                        BusinessName = c.String(maxLength: 200),
                        WebSite = c.String(maxLength: 200),
                        Status = c.Int(nullable: false),
                        CreditLimit = c.Double(nullable: false),
                        CreditDays = c.Int(nullable: false),
                        CreditBalance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.EntityId)
                .ForeignKey("Business.Person", t => t.EntityId)
                .ForeignKey("Config.CreditStatus", t => t.Status, cascadeDelete: true)
                .Index(t => t.EntityId)
                .Index(t => t.Status);
            
            CreateTable(
                "Sales.SaleDetail",
                c => new
                    {
                        DetailId = c.Int(nullable: false),
                        SaleId = c.Int(nullable: false),
                        OrderQty = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        LineTotal = c.Double(nullable: false),
                        UnitPriceDiscount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DetailId)
                .ForeignKey("Business.Detail", t => t.DetailId)
                .ForeignKey("Sales.Sale", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.DetailId)
                .Index(t => t.SaleId);
            
            CreateTable(
                "Purchasing.PurchaseDetail",
                c => new
                    {
                        DetailId = c.Int(nullable: false),
                        PurchaseId = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        OrderQty = c.Double(nullable: false),
                        LineTotal = c.Double(nullable: false),
                        ReceivedQty = c.Double(nullable: false),
                        RejectedQty = c.Double(nullable: false),
                        StockQty = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DetailId)
                .ForeignKey("Business.Detail", t => t.DetailId)
                .ForeignKey("Purchasing.Purchase", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.DetailId)
                .Index(t => t.PurchaseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Purchasing.PurchaseDetail", "PurchaseId", "Purchasing.Purchase");
            DropForeignKey("Purchasing.PurchaseDetail", "DetailId", "Business.Detail");
            DropForeignKey("Sales.SaleDetail", "SaleId", "Sales.Sale");
            DropForeignKey("Sales.SaleDetail", "DetailId", "Business.Detail");
            DropForeignKey("Purchasing.Supplier", "Status", "Config.CreditStatus");
            DropForeignKey("Purchasing.Supplier", "EntityId", "Business.Person");
            DropForeignKey("Config.Branch", "EntityId", "Business.Entity");
            DropForeignKey("HumanResources.Employee", "JobPositionId", "HumanResources.JobPosition");
            DropForeignKey("HumanResources.Employee", "EntityId", "Business.Person");
            DropForeignKey("Sales.Client", "CreditStatusId", "Config.CreditStatus");
            DropForeignKey("Sales.Client", "EntityId", "Business.Person");
            DropForeignKey("Production.Service", "ServiceTypeId", "Production.ServiceType");
            DropForeignKey("Production.Service", "OccurenceId", "Production.Occurence");
            DropForeignKey("Business.Person", "EntityId", "Business.Entity");
            DropForeignKey("Production.Charge", "OccurenceId", "Production.Occurence");
            DropForeignKey("Security.AspNetUserRoles", "RoleId", "Security.AspNetRoles");
            DropForeignKey("Production.AccountHistory", "AccountId", "Production.Account");
            DropForeignKey("Production.Account", "PaymentPeriodId", "Production.PaymentPeriod");
            DropForeignKey("Production.Occurence", "AccountId", "Production.Account");
            DropForeignKey("Production.Account", "MaintPeriodId", "Production.MaintPeriod");
            DropForeignKey("Production.Account", "ClientId", "Sales.Client");
            DropForeignKey("Config.Town", "StateId", "Config.State");
            DropForeignKey("Config.Settlement", "TownId", "Config.Town");
            DropForeignKey("Business.Address", "TownId", "Config.Town");
            DropForeignKey("Business.Address", "EntityId", "Business.Entity");
            DropForeignKey("HumanResources.EmployeeBranch", "EmployeeId", "HumanResources.Employee");
            DropForeignKey("HumanResources.EmployeeBranch", "BranchId", "Config.Branch");
            DropForeignKey("Inventory.ItemHistory", "Storage_StorageId", "Inventory.Storage");
            DropForeignKey("Inventory.ItemStorage", "StorageId", "Inventory.Storage");
            DropForeignKey("Inventory.ItemStorage", "ItemId", "Inventory.Item");
            DropForeignKey("Inventory.ItemHistory", "ItemId", "Inventory.Item");
            DropForeignKey("Inventory.Flow", "ItemId", "Inventory.Item");
            DropForeignKey("Inventory.Flow", "Direction", "Inventory.FlowDirection");
            DropForeignKey("Inventory.SupplierProduct", "SupplierId", "Purchasing.Supplier");
            DropForeignKey("Purchasing.Purchase", "SupplierId", "Purchasing.Supplier");
            DropForeignKey("Purchasing.Purchase", "ShipMethodId", "Sales.ShipMethod");
            DropForeignKey("Purchasing.Purchase", "Status", "Purchasing.Status");
            DropForeignKey("Sales.Sale", "ShipMethodId", "Sales.ShipMethod");
            DropForeignKey("Sales.Sale", "Status", "Sales.Status");
            DropForeignKey("Sales.Sale", "PayFormId", "Config.PayForm");
            DropForeignKey("Sales.Sale", "EmployeeId", "HumanResources.Employee");
            DropForeignKey("Sales.Sale", "ClientId", "Sales.Client");
            DropForeignKey("Sales.Sale", "BranchId", "Config.Branch");
            DropForeignKey("Purchasing.Purchase", "PayFormId", "Config.PayForm");
            DropForeignKey("Purchasing.Purchase", "BranchId", "Config.Branch");
            DropForeignKey("Inventory.SupplierProduct", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Product", "SubCategoryId", "Inventory.SubCategory");
            DropForeignKey("Inventory.SubCategory", "CategoryId", "Inventory.Category");
            DropForeignKey("Inventory.ProductImage", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.PriceChange", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Product", "MeasureUnitId", "Inventory.MeasureUnit");
            DropForeignKey("Inventory.Item", "ProductId", "Inventory.Product");
            DropForeignKey("Business.Detail", "ProductId", "Inventory.Product");
            DropForeignKey("Config.Compatibility", "ProductId", "Inventory.Product");
            DropForeignKey("Config.Model", "MakerId", "Config.Maker");
            DropForeignKey("Config.Compatibility", "ModelId", "Config.Model");
            DropForeignKey("Inventory.Flow", "DetailId", "Business.Detail");
            DropForeignKey("Inventory.Storage", "BranchId", "Config.Branch");
            DropForeignKey("Security.SystemUser", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserRoles", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserLogins", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserClaims", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.SystemUser", "SystemUserId", "Business.Person");
            DropForeignKey("Business.PhoneNumber", "PhoneTypeId", "Business.PhoneType");
            DropForeignKey("Business.PhoneNumber", "EntityId", "Business.Entity");
            DropForeignKey("Business.EmailAddress", "EntityId", "Business.Entity");
            DropForeignKey("Business.EmailAddress", "EmailTypeId", "Business.EmailType");
            DropForeignKey("Business.Address", "AddressTypeId", "Business.AddressType");
            DropForeignKey("Production.Account", "AccountTypeId", "Production.AccountType");
            DropForeignKey("Production.Account", "AccountStatusId", "Production.AccountStatus");
            DropIndex("Purchasing.PurchaseDetail", new[] { "PurchaseId" });
            DropIndex("Purchasing.PurchaseDetail", new[] { "DetailId" });
            DropIndex("Sales.SaleDetail", new[] { "SaleId" });
            DropIndex("Sales.SaleDetail", new[] { "DetailId" });
            DropIndex("Purchasing.Supplier", new[] { "Status" });
            DropIndex("Purchasing.Supplier", new[] { "EntityId" });
            DropIndex("Config.Branch", new[] { "EntityId" });
            DropIndex("HumanResources.Employee", new[] { "JobPositionId" });
            DropIndex("HumanResources.Employee", new[] { "EntityId" });
            DropIndex("Sales.Client", new[] { "CreditStatusId" });
            DropIndex("Sales.Client", new[] { "EntityId" });
            DropIndex("Production.Service", new[] { "ServiceTypeId" });
            DropIndex("Production.Service", new[] { "OccurenceId" });
            DropIndex("Business.Person", new[] { "EntityId" });
            DropIndex("Production.Charge", new[] { "OccurenceId" });
            DropIndex("Security.AspNetRoles", "RoleNameIndex");
            DropIndex("Production.Occurence", new[] { "AccountId" });
            DropIndex("Config.Settlement", "IDX_Code_Name");
            DropIndex("Config.Settlement", new[] { "TownId" });
            DropIndex("Config.Town", new[] { "StateId" });
            DropIndex("Inventory.ItemStorage", new[] { "StorageId" });
            DropIndex("Inventory.ItemStorage", new[] { "ItemId" });
            DropIndex("Sales.Sale", new[] { "PayFormId" });
            DropIndex("Sales.Sale", new[] { "ShipMethodId" });
            DropIndex("Sales.Sale", new[] { "BranchId" });
            DropIndex("Sales.Sale", new[] { "EmployeeId" });
            DropIndex("Sales.Sale", new[] { "ClientId" });
            DropIndex("Sales.Sale", new[] { "Status" });
            DropIndex("Purchasing.Purchase", new[] { "PayFormId" });
            DropIndex("Purchasing.Purchase", new[] { "ShipMethodId" });
            DropIndex("Purchasing.Purchase", new[] { "BranchId" });
            DropIndex("Purchasing.Purchase", new[] { "Status" });
            DropIndex("Purchasing.Purchase", "IDX_Supplier_Bill");
            DropIndex("Inventory.SupplierProduct", new[] { "ProductId" });
            DropIndex("Inventory.SupplierProduct", new[] { "SupplierId" });
            DropIndex("Inventory.SubCategory", new[] { "CategoryId" });
            DropIndex("Inventory.ProductImage", new[] { "ProductId" });
            DropIndex("Inventory.PriceChange", new[] { "ProductId" });
            DropIndex("Config.Model", new[] { "MakerId" });
            DropIndex("Config.Compatibility", new[] { "ProductId" });
            DropIndex("Config.Compatibility", new[] { "ModelId" });
            DropIndex("Inventory.Product", "IDX_Code_Description_TradeMark");
            DropIndex("Inventory.Product", new[] { "MeasureUnitId" });
            DropIndex("Inventory.Product", new[] { "SubCategoryId" });
            DropIndex("Business.Detail", new[] { "ProductId" });
            DropIndex("Inventory.Flow", new[] { "Direction" });
            DropIndex("Inventory.Flow", new[] { "DetailId" });
            DropIndex("Inventory.Flow", new[] { "ItemId" });
            DropIndex("Inventory.Item", new[] { "ProductId" });
            DropIndex("Inventory.ItemHistory", new[] { "Storage_StorageId" });
            DropIndex("Inventory.ItemHistory", new[] { "ItemId" });
            DropIndex("Inventory.Storage", new[] { "BranchId" });
            DropIndex("HumanResources.EmployeeBranch", new[] { "BranchId" });
            DropIndex("HumanResources.EmployeeBranch", new[] { "EmployeeId" });
            DropIndex("Security.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("Security.AspNetUserRoles", new[] { "UserId" });
            DropIndex("Security.AspNetUserLogins", new[] { "UserId" });
            DropIndex("Security.AspNetUserClaims", new[] { "UserId" });
            DropIndex("Security.AspNetUsers", "UserNameIndex");
            DropIndex("Security.SystemUser", new[] { "UserId" });
            DropIndex("Security.SystemUser", new[] { "SystemUserId" });
            DropIndex("Business.PhoneNumber", new[] { "PhoneTypeId" });
            DropIndex("Business.PhoneNumber", new[] { "EntityId" });
            DropIndex("Business.EmailAddress", new[] { "EmailTypeId" });
            DropIndex("Business.EmailAddress", new[] { "EntityId" });
            DropIndex("Business.Address", new[] { "TownId" });
            DropIndex("Business.Address", new[] { "AddressTypeId" });
            DropIndex("Business.Address", new[] { "EntityId" });
            DropIndex("Production.Account", new[] { "PaymentPeriodId" });
            DropIndex("Production.Account", new[] { "MaintPeriodId" });
            DropIndex("Production.Account", "IDX_CutOffDate");
            DropIndex("Production.Account", "IDX_Code");
            DropIndex("Production.Account", new[] { "AccountTypeId" });
            DropIndex("Production.Account", new[] { "AccountStatusId" });
            DropIndex("Production.Account", new[] { "ClientId" });
            DropIndex("Production.AccountHistory", new[] { "AccountId" });
            DropTable("Purchasing.PurchaseDetail");
            DropTable("Sales.SaleDetail");
            DropTable("Purchasing.Supplier");
            DropTable("Config.Branch");
            DropTable("HumanResources.Employee");
            DropTable("Sales.Client");
            DropTable("Production.Service");
            DropTable("Business.Person");
            DropTable("Production.Charge");
            DropTable("Security.AspNetRoles");
            DropTable("Security.PayMethods");
            DropTable("Analytic.ExecutionTaskLog");
            DropTable("Analytic.ErroLog");
            DropTable("Config.Configuration");
            DropTable("Production.PaymentPeriod");
            DropTable("Production.ServiceType");
            DropTable("Production.Occurence");
            DropTable("Production.MaintPeriod");
            DropTable("Config.State");
            DropTable("Config.Settlement");
            DropTable("Config.Town");
            DropTable("HumanResources.JobPosition");
            DropTable("Inventory.ItemStorage");
            DropTable("Inventory.FlowDirection");
            DropTable("Purchasing.Status");
            DropTable("Sales.ShipMethod");
            DropTable("Sales.Status");
            DropTable("Sales.Sale");
            DropTable("Config.PayForm");
            DropTable("Purchasing.Purchase");
            DropTable("Config.CreditStatus");
            DropTable("Inventory.SupplierProduct");
            DropTable("Inventory.Category");
            DropTable("Inventory.SubCategory");
            DropTable("Inventory.ProductImage");
            DropTable("Inventory.PriceChange");
            DropTable("Inventory.MeasureUnit");
            DropTable("Config.Maker");
            DropTable("Config.Model");
            DropTable("Config.Compatibility");
            DropTable("Inventory.Product");
            DropTable("Business.Detail");
            DropTable("Inventory.Flow");
            DropTable("Inventory.Item");
            DropTable("Inventory.ItemHistory");
            DropTable("Inventory.Storage");
            DropTable("HumanResources.EmployeeBranch");
            DropTable("Security.AspNetUserRoles");
            DropTable("Security.AspNetUserLogins");
            DropTable("Security.AspNetUserClaims");
            DropTable("Security.AspNetUsers");
            DropTable("Security.SystemUser");
            DropTable("Business.PhoneType");
            DropTable("Business.PhoneNumber");
            DropTable("Business.EmailType");
            DropTable("Business.EmailAddress");
            DropTable("Business.AddressType");
            DropTable("Business.Address");
            DropTable("Business.Entity");
            DropTable("Production.AccountType");
            DropTable("Production.AccountStatus");
            DropTable("Production.Account");
            DropTable("Production.AccountHistory");
        }
    }
}
