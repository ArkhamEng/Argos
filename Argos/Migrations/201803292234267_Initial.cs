namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Operative.AccountFile",
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
                .ForeignKey("Operative.Account", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "Operative.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        AccountTypeId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Code = c.String(maxLength: 12),
                        HireDate = c.DateTime(nullable: false),
                        HirePrice = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("Operative.AccountType", t => t.AccountTypeId, cascadeDelete: true)
                .ForeignKey("Transaction.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Operative.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.AccountTypeId)
                .Index(t => t.ClientId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "Operative.AccountType",
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
                "Transaction.Client",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(maxLength: 200),
                        Name = c.String(nullable: false, maxLength: 150),
                        FTR = c.String(maxLength: 13),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(nullable: false, maxLength: 6),
                        InNumber = c.String(maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
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
                        JobPositionId = c.String(),
                        Gender = c.String(maxLength: 10),
                        BirthDate = c.DateTime(nullable: false),
                        SSN = c.String(maxLength: 11),
                        Commission = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        FTR = c.String(maxLength: 13),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(nullable: false, maxLength: 6),
                        InNumber = c.String(maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                        JobPosition_JobPositionId = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("HumanResources.JobPosition", t => t.JobPosition_JobPositionId)
                .Index(t => t.Name, name: "IDX_Name")
                .Index(t => t.FTR, unique: true, name: "Unq_FTR")
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.Email, unique: true, name: "Unq_Email")
                .Index(t => t.CityId)
                .Index(t => t.JobPosition_JobPositionId);
            
            CreateTable(
                "Security.UserEmployee",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        EmployeeId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
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
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.JobPositionId);
            
            CreateTable(
                "Transaction.Sale",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        SaleCode = c.String(maxLength: 10),
                        SaleDate = c.DateTime(nullable: false),
                        ForShipping = c.Boolean(nullable: false),
                        BranchId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        DuDate = c.DateTime(),
                        TaxPercentage = c.Int(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        TaxAmount = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Transaction.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("HumanResources.Employee", t => t.EmployeeId, cascadeDelete: false)
                .ForeignKey("Transaction.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("Transaction.TransType", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.EmployeeId)
                .Index(t => t.BranchId)
                .Index(t => t.TypeId)
                .Index(t => t.StatusId);
            
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
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.BranchId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: false)
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.CityId);
            
            CreateTable(
                "Transaction.Purchase",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        Bill = c.String(maxLength: 10),
                        PurchaseDate = c.DateTime(nullable: false),
                        BranchId = c.Int(nullable: false),
                        TypeId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        DuDate = c.DateTime(),
                        TaxPercentage = c.Int(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        TaxAmount = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Transaction.Supplier", t => t.SupplierId, cascadeDelete: true)
                .ForeignKey("Transaction.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("Transaction.TransType", t => t.TypeId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.BranchId)
                .Index(t => t.TypeId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "Transaction.PurchaseDetail",
                c => new
                    {
                        PurchaseDetailId = c.Int(nullable: false, identity: true),
                        PurchaseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        TaxPercent = c.Int(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        TaxAmount = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseDetailId)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("Transaction.Purchase", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.PurchaseId)
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
                        HighestProfit = c.Int(nullable: false),
                        HighestPrice = c.Double(nullable: false),
                        LowestProfit = c.Int(nullable: false),
                        LowestPrice = c.Double(nullable: false),
                        MeasureUnitId = c.String(maxLength: 5),
                        StockRequired = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("Inventory.MeasureUnit", t => t.MeasureUnitId)
                .ForeignKey("Inventory.SubCategory", t => t.SubCategoryId, cascadeDelete: true)
                .Index(t => t.SubCategoryId)
                .Index(t => t.MeasureUnitId);
            
            CreateTable(
                "Inventory.Compatibility",
                c => new
                    {
                        CarYearId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => new { t.CarYearId, t.ProductId })
                .ForeignKey("Config.CarYear", t => t.CarYearId, cascadeDelete: true)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.CarYearId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Config.CarYear",
                c => new
                    {
                        CarYearId = c.Int(nullable: false, identity: true),
                        CarModelId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.CarYearId)
                .ForeignKey("Config.CarModel", t => t.CarModelId, cascadeDelete: true)
                .Index(t => t.CarModelId);
            
            CreateTable(
                "Config.CarModel",
                c => new
                    {
                        CarModelId = c.Int(nullable: false, identity: true),
                        CarMakeId = c.Int(nullable: false),
                        Name = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.CarModelId)
                .ForeignKey("Config.CarMake", t => t.CarMakeId, cascadeDelete: true)
                .Index(t => t.CarMakeId);
            
            CreateTable(
                "Config.CarMake",
                c => new
                    {
                        CarMakeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.CarMakeId);
            
            CreateTable(
                "Inventory.Equivalence",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        ExternalProductId = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => new { t.ProductId, t.ExternalProductId })
                .ForeignKey("Inventory.ExternalProduct", t => t.ExternalProductId, cascadeDelete: true)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ExternalProductId);
            
            CreateTable(
                "Inventory.ExternalProduct",
                c => new
                    {
                        ExternalProductId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        Code = c.String(),
                        SatCode = c.String(),
                        Description = c.String(),
                        TradeMark = c.String(),
                        Unit = c.String(),
                        BuyPrice = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ExternalProductId)
                .ForeignKey("Transaction.Supplier", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "Transaction.Supplier",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(maxLength: 200),
                        WebSite = c.String(maxLength: 200),
                        Name = c.String(nullable: false, maxLength: 150),
                        FTR = c.String(maxLength: 13),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(nullable: false, maxLength: 6),
                        InNumber = c.String(maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
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
                "Inventory.MeasureUnit",
                c => new
                    {
                        MeasureUnitId = c.String(nullable: false, maxLength: 5),
                        Description = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.MeasureUnitId);
            
            CreateTable(
                "Inventory.PackageDetail",
                c => new
                    {
                        PackageId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.PackageId, t.ProductId })
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: false)
                .ForeignKey("Inventory.Product", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Inventory.PriceHistory",
                c => new
                    {
                        PriceHistoryId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        HighestPrice = c.Double(nullable: false),
                        LowestPrice = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.PriceHistoryId)
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
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ProductImageId)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Transaction.SaleDetail",
                c => new
                    {
                        SaleDetailId = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        TaxPercent = c.Int(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        TaxAmount = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.SaleDetailId)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("Transaction.Sale", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.SaleId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Inventory.Stock",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        Available = c.Double(nullable: false),
                        Reserved = c.Double(nullable: false),
                        MaxQuantity = c.Double(nullable: false),
                        MinQuantity = c.Double(nullable: false),
                        Comment = c.String(maxLength: 100),
                        Row = c.String(maxLength: 40),
                        Ledge = c.String(maxLength: 40),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.StockId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.BranchId);
            
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
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "Transaction.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        ForSale = c.Boolean(nullable: false),
                        ForPurchase = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "Transaction.TransType",
                c => new
                    {
                        TransTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ForSale = c.Boolean(nullable: false),
                        ForPurchase = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TransTypeId);
            
            CreateTable(
                "HumanResources.Commission",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        Profit = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        PayDate = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.CommissionId)
                .ForeignKey("Transaction.Sale", t => t.CommissionId)
                .Index(t => t.CommissionId);
            
            CreateTable(
                "Transaction.Shipping",
                c => new
                    {
                        ShippingId = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        ShipMethodId = c.Int(nullable: false),
                        ShipDate = c.DateTime(),
                        ReceptionDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ShippingId)
                .ForeignKey("Transaction.Sale", t => t.SaleId, cascadeDelete: true)
                .ForeignKey("Transaction.ShipMethod", t => t.ShipMethodId, cascadeDelete: true)
                .Index(t => t.SaleId)
                .Index(t => t.ShipMethodId);
            
            CreateTable(
                "Transaction.ShipMethod",
                c => new
                    {
                        ShipMethodId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ShipMethodId);
            
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
                "Operative.Location",
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
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.LocationId)
                .ForeignKey("Operative.Account", t => t.LocationId)
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
                "Operative.Policy",
                c => new
                    {
                        PolicyId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        CutOffDay = c.Int(nullable: false),
                        NextCutOff = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        PayFreq = c.Int(nullable: false),
                        MaintFreq = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.PolicyId)
                .ForeignKey("Operative.Account", t => t.PolicyId)
                .ForeignKey("Operative.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.PolicyId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "Operative.PolicyCharge",
                c => new
                    {
                        PolicyChargeId = c.Int(nullable: false, identity: true),
                        PolicyId = c.Int(nullable: false),
                        ScheduleDate = c.DateTime(nullable: false),
                        PayedDate = c.DateTime(),
                        Amount = c.Double(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.PolicyChargeId)
                .ForeignKey("Operative.Policy", t => t.PolicyId, cascadeDelete: true)
                .Index(t => t.PolicyId);
            
            CreateTable(
                "Operative.PolicyHistory",
                c => new
                    {
                        PolicyHistoryId = c.Int(nullable: false, identity: true),
                        PolicyId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        MaintFreq = c.Int(nullable: false),
                        PayFreq = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.PolicyHistoryId)
                .ForeignKey("Operative.Policy", t => t.PolicyId, cascadeDelete: true)
                .ForeignKey("Operative.Status", t => t.StatusId, cascadeDelete: false)
                .Index(t => t.PolicyId)
                .Index(t => t.StatusId);
            
            CreateTable(
                "Operative.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.StatusId);
            
            CreateTable(
                "Operative.Service",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        ServiceTypeId = c.Int(nullable: false),
                        Folio = c.String(maxLength: 10),
                        ScheduleDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ServiceId)
                .ForeignKey("Operative.Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("Operative.ServiceType", t => t.ServiceTypeId, cascadeDelete: true)
                .Index(t => t.AccountId)
                .Index(t => t.ServiceTypeId);
            
            CreateTable(
                "Operative.ServiceType",
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
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => new { t.UserId, t.ClientId })
                .ForeignKey("Transaction.Client", t => t.ClientId, cascadeDelete: true)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("Security.AspNetUserRoles", "RoleId", "Security.AspNetRoles");
            DropForeignKey("Security.ClientUser", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.ClientUser", "ClientId", "Transaction.Client");
            DropForeignKey("Operative.Service", "ServiceTypeId", "Operative.ServiceType");
            DropForeignKey("Operative.Service", "AccountId", "Operative.Account");
            DropForeignKey("Operative.Policy", "StatusId", "Operative.Status");
            DropForeignKey("Operative.PolicyHistory", "StatusId", "Operative.Status");
            DropForeignKey("Operative.Account", "StatusId", "Operative.Status");
            DropForeignKey("Operative.PolicyHistory", "PolicyId", "Operative.Policy");
            DropForeignKey("Operative.PolicyCharge", "PolicyId", "Operative.Policy");
            DropForeignKey("Operative.Policy", "PolicyId", "Operative.Account");
            DropForeignKey("Config.City", "StateId", "Config.State");
            DropForeignKey("Operative.Location", "CityId", "Config.City");
            DropForeignKey("Operative.Location", "LocationId", "Operative.Account");
            DropForeignKey("Config.Locality", "CityId", "Config.City");
            DropForeignKey("Transaction.Sale", "TypeId", "Transaction.TransType");
            DropForeignKey("Transaction.Sale", "StatusId", "Transaction.Status");
            DropForeignKey("Transaction.Shipping", "ShipMethodId", "Transaction.ShipMethod");
            DropForeignKey("Transaction.Shipping", "SaleId", "Transaction.Sale");
            DropForeignKey("Transaction.Sale", "EmployeeId", "HumanResources.Employee");
            DropForeignKey("HumanResources.Commission", "CommissionId", "Transaction.Sale");
            DropForeignKey("Transaction.Sale", "ClientId", "Transaction.Client");
            DropForeignKey("Transaction.Sale", "BranchId", "Config.Branch");
            DropForeignKey("Transaction.Purchase", "TypeId", "Transaction.TransType");
            DropForeignKey("Transaction.Purchase", "StatusId", "Transaction.Status");
            DropForeignKey("Transaction.PurchaseDetail", "PurchaseId", "Transaction.Purchase");
            DropForeignKey("Transaction.PurchaseDetail", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Product", "SubCategoryId", "Inventory.SubCategory");
            DropForeignKey("Inventory.SubCategory", "CategoryId", "Inventory.Category");
            DropForeignKey("Inventory.Stock", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Stock", "BranchId", "Config.Branch");
            DropForeignKey("Transaction.SaleDetail", "SaleId", "Transaction.Sale");
            DropForeignKey("Transaction.SaleDetail", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.ProductImage", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.PriceHistory", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.PackageDetail", "PackageId", "Inventory.Product");
            DropForeignKey("Inventory.PackageDetail", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Product", "MeasureUnitId", "Inventory.MeasureUnit");
            DropForeignKey("Inventory.Equivalence", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Equivalence", "ExternalProductId", "Inventory.ExternalProduct");
            DropForeignKey("Inventory.ExternalProduct", "SupplierId", "Transaction.Supplier");
            DropForeignKey("Transaction.Purchase", "SupplierId", "Transaction.Supplier");
            DropForeignKey("Transaction.Supplier", "CityId", "Config.City");
            DropForeignKey("Inventory.Compatibility", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Compatibility", "CarYearId", "Config.CarYear");
            DropForeignKey("Config.CarYear", "CarModelId", "Config.CarModel");
            DropForeignKey("Config.CarModel", "CarMakeId", "Config.CarMake");
            DropForeignKey("Transaction.Purchase", "BranchId", "Config.Branch");
            DropForeignKey("Config.Branch", "CityId", "Config.City");
            DropForeignKey("HumanResources.Employee", "JobPosition_JobPositionId", "HumanResources.JobPosition");
            DropForeignKey("Security.UserEmployee", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserRoles", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserLogins", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserClaims", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.UserEmployee", "EmployeeId", "HumanResources.Employee");
            DropForeignKey("HumanResources.Employee", "CityId", "Config.City");
            DropForeignKey("Transaction.Client", "CityId", "Config.City");
            DropForeignKey("Operative.Account", "ClientId", "Transaction.Client");
            DropForeignKey("Operative.Account", "AccountTypeId", "Operative.AccountType");
            DropForeignKey("Operative.AccountFile", "AccountId", "Operative.Account");
            DropIndex("Security.AspNetRoles", "RoleNameIndex");
            DropIndex("Security.ClientUser", new[] { "ClientId" });
            DropIndex("Security.ClientUser", new[] { "UserId" });
            DropIndex("Operative.Service", new[] { "ServiceTypeId" });
            DropIndex("Operative.Service", new[] { "AccountId" });
            DropIndex("Operative.PolicyHistory", new[] { "StatusId" });
            DropIndex("Operative.PolicyHistory", new[] { "PolicyId" });
            DropIndex("Operative.PolicyCharge", new[] { "PolicyId" });
            DropIndex("Operative.Policy", new[] { "StatusId" });
            DropIndex("Operative.Policy", new[] { "PolicyId" });
            DropIndex("Operative.Location", new[] { "CityId" });
            DropIndex("Operative.Location", new[] { "LocationId" });
            DropIndex("Config.Locality", new[] { "CityId" });
            DropIndex("Transaction.Shipping", new[] { "ShipMethodId" });
            DropIndex("Transaction.Shipping", new[] { "SaleId" });
            DropIndex("HumanResources.Commission", new[] { "CommissionId" });
            DropIndex("Inventory.SubCategory", new[] { "CategoryId" });
            DropIndex("Inventory.Stock", new[] { "BranchId" });
            DropIndex("Inventory.Stock", new[] { "ProductId" });
            DropIndex("Transaction.SaleDetail", new[] { "ProductId" });
            DropIndex("Transaction.SaleDetail", new[] { "SaleId" });
            DropIndex("Inventory.ProductImage", new[] { "ProductId" });
            DropIndex("Inventory.PriceHistory", new[] { "ProductId" });
            DropIndex("Inventory.PackageDetail", new[] { "ProductId" });
            DropIndex("Inventory.PackageDetail", new[] { "PackageId" });
            DropIndex("Transaction.Supplier", new[] { "CityId" });
            DropIndex("Transaction.Supplier", "Unq_Email");
            DropIndex("Transaction.Supplier", "Unq_Phone");
            DropIndex("Transaction.Supplier", "Unq_FTR");
            DropIndex("Transaction.Supplier", "IDX_Name");
            DropIndex("Transaction.Supplier", "Unq_BusinessName");
            DropIndex("Inventory.ExternalProduct", new[] { "SupplierId" });
            DropIndex("Inventory.Equivalence", new[] { "ExternalProductId" });
            DropIndex("Inventory.Equivalence", new[] { "ProductId" });
            DropIndex("Config.CarModel", new[] { "CarMakeId" });
            DropIndex("Config.CarYear", new[] { "CarModelId" });
            DropIndex("Inventory.Compatibility", new[] { "ProductId" });
            DropIndex("Inventory.Compatibility", new[] { "CarYearId" });
            DropIndex("Inventory.Product", new[] { "MeasureUnitId" });
            DropIndex("Inventory.Product", new[] { "SubCategoryId" });
            DropIndex("Transaction.PurchaseDetail", new[] { "ProductId" });
            DropIndex("Transaction.PurchaseDetail", new[] { "PurchaseId" });
            DropIndex("Transaction.Purchase", new[] { "StatusId" });
            DropIndex("Transaction.Purchase", new[] { "TypeId" });
            DropIndex("Transaction.Purchase", new[] { "BranchId" });
            DropIndex("Transaction.Purchase", new[] { "SupplierId" });
            DropIndex("Config.Branch", new[] { "CityId" });
            DropIndex("Config.Branch", "Unq_Phone");
            DropIndex("Transaction.Sale", new[] { "StatusId" });
            DropIndex("Transaction.Sale", new[] { "TypeId" });
            DropIndex("Transaction.Sale", new[] { "BranchId" });
            DropIndex("Transaction.Sale", new[] { "EmployeeId" });
            DropIndex("Transaction.Sale", new[] { "ClientId" });
            DropIndex("Security.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("Security.AspNetUserRoles", new[] { "UserId" });
            DropIndex("Security.AspNetUserLogins", new[] { "UserId" });
            DropIndex("Security.AspNetUserClaims", new[] { "UserId" });
            DropIndex("Security.AspNetUsers", "UserNameIndex");
            DropIndex("Security.UserEmployee", new[] { "EmployeeId" });
            DropIndex("Security.UserEmployee", new[] { "UserId" });
            DropIndex("HumanResources.Employee", new[] { "JobPosition_JobPositionId" });
            DropIndex("HumanResources.Employee", new[] { "CityId" });
            DropIndex("HumanResources.Employee", "Unq_Email");
            DropIndex("HumanResources.Employee", "Unq_Phone");
            DropIndex("HumanResources.Employee", "Unq_FTR");
            DropIndex("HumanResources.Employee", "IDX_Name");
            DropIndex("Config.City", new[] { "StateId" });
            DropIndex("Transaction.Client", new[] { "CityId" });
            DropIndex("Transaction.Client", "Unq_Email");
            DropIndex("Transaction.Client", "Unq_Phone");
            DropIndex("Transaction.Client", "Unq_FTR");
            DropIndex("Transaction.Client", "IDX_Name");
            DropIndex("Transaction.Client", "Unq_BusinessName");
            DropIndex("Operative.Account", new[] { "StatusId" });
            DropIndex("Operative.Account", new[] { "ClientId" });
            DropIndex("Operative.Account", new[] { "AccountTypeId" });
            DropIndex("Operative.AccountFile", new[] { "AccountId" });
            DropTable("Security.AspNetRoles");
            DropTable("Config.Configuration");
            DropTable("Security.ClientUser");
            DropTable("Operative.ServiceType");
            DropTable("Operative.Service");
            DropTable("Operative.Status");
            DropTable("Operative.PolicyHistory");
            DropTable("Operative.PolicyCharge");
            DropTable("Operative.Policy");
            DropTable("Config.State");
            DropTable("Operative.Location");
            DropTable("Config.Locality");
            DropTable("Transaction.ShipMethod");
            DropTable("Transaction.Shipping");
            DropTable("HumanResources.Commission");
            DropTable("Transaction.TransType");
            DropTable("Transaction.Status");
            DropTable("Inventory.Category");
            DropTable("Inventory.SubCategory");
            DropTable("Inventory.Stock");
            DropTable("Transaction.SaleDetail");
            DropTable("Inventory.ProductImage");
            DropTable("Inventory.PriceHistory");
            DropTable("Inventory.PackageDetail");
            DropTable("Inventory.MeasureUnit");
            DropTable("Transaction.Supplier");
            DropTable("Inventory.ExternalProduct");
            DropTable("Inventory.Equivalence");
            DropTable("Config.CarMake");
            DropTable("Config.CarModel");
            DropTable("Config.CarYear");
            DropTable("Inventory.Compatibility");
            DropTable("Inventory.Product");
            DropTable("Transaction.PurchaseDetail");
            DropTable("Transaction.Purchase");
            DropTable("Config.Branch");
            DropTable("Transaction.Sale");
            DropTable("HumanResources.JobPosition");
            DropTable("Security.AspNetUserRoles");
            DropTable("Security.AspNetUserLogins");
            DropTable("Security.AspNetUserClaims");
            DropTable("Security.AspNetUsers");
            DropTable("Security.UserEmployee");
            DropTable("HumanResources.Employee");
            DropTable("Config.City");
            DropTable("Transaction.Client");
            DropTable("Operative.AccountType");
            DropTable("Operative.Account");
            DropTable("Operative.AccountFile");
        }
    }
}
