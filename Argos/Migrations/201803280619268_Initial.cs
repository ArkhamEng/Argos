namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Operative.AccountAddress",
                c => new
                    {
                        AccountAddressId = c.Int(nullable: false),
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
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.AccountAddressId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: false)
                .ForeignKey("Operative.Service", t => t.AccountAddressId)
                .Index(t => t.AccountAddressId)
                .Index(t => t.CityId);
            
            CreateTable(
                "Config.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        StateId = c.Int(nullable: false),
                        Name = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("Config.State", t => t.StateId, cascadeDelete: false)
                .Index(t => t.StateId);
            
            CreateTable(
                "Catalog.Client",
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
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: false)
                .Index(t => t.BusinessName, unique: true, name: "Unq_BusinessName")
                .Index(t => t.Name, name: "IDX_Name")
                .Index(t => t.FTR, unique: true, name: "Unq_FTR")
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.Email, unique: true, name: "Unq_Email")
                .Index(t => t.CityId);
            
            CreateTable(
                "Operative.Service",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceStatusId = c.Int(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        HirePrice = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                        Code = c.String(maxLength: 20),
                        ClientId = c.Int(),
                        ServiceTypeId = c.Int(),
                        ServiceCategoryId = c.Int(),
                        BranchId = c.Int(),
                        ParentServiceId = c.Int(),
                        PaymentPeriod = c.Int(),
                        PaymentTolerance = c.Int(),
                        MaintenancePeriod = c.Int(),
                        MaintenanceTolerance = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ServiceId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Catalog.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Config.ServiceCategory", t => t.ServiceCategoryId, cascadeDelete: true)
                .ForeignKey("Operative.ServiceStatus", t => t.ServiceStatusId, cascadeDelete: true)
                .ForeignKey("Operative.Service", t => t.ParentServiceId)
                .Index(t => t.ServiceStatusId)
                .Index(t => t.ClientId)
                .Index(t => t.ServiceCategoryId)
                .Index(t => t.BranchId)
                .Index(t => t.ParentServiceId);
            
            CreateTable(
                "Operative.Operation",
                c => new
                    {
                        OperationId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        Folio = c.String(maxLength: 10),
                        RiseDate = c.DateTime(nullable: false),
                        ScheduleDate = c.DateTime(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.OperationId)
                .ForeignKey("Operative.Service", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "Transaction.SaleDetail",
                c => new
                    {
                        SaleDetailId = c.Int(nullable: false, identity: true),
                        SaleId = c.Int(nullable: false),
                        ServiceId = c.Int(),
                        ProductId = c.Int(),
                        Quantity = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        PartialAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.SaleDetailId)
                .ForeignKey("Transaction.Sale", t => t.SaleId, cascadeDelete: true)
                .ForeignKey("Inventory.Product", t => t.ProductId)
                .ForeignKey("Operative.Service", t => t.ServiceId)
                .Index(t => t.SaleId)
                .Index(t => t.ServiceId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Inventory.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        SubCategoryId = c.Int(nullable: false),
                        Code = c.String(maxLength: 30),
                        Description = c.String(maxLength: 250),
                        TradeMark = c.String(maxLength: 50),
                        Cost = c.Double(nullable: false),
                        Percentage = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        MiddlePercentage = c.Int(nullable: false),
                        MiddlePrice = c.Double(nullable: false),
                        LowerPercentage = c.Int(nullable: false),
                        LowerPrice = c.Double(nullable: false),
                        SoldAs = c.String(maxLength: 10),
                        MeasureUnitId = c.String(maxLength: 5),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
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
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
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
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
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
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
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
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
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
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ExternalProductId)
                .ForeignKey("Catalog.Supplier", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "Catalog.Supplier",
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
                        LockDate = c.DateTime(),
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
                "Transaction.Purchase",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        Bill = c.String(maxLength: 10),
                        TransactionDate = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        TransactionTypeId = c.Int(nullable: false),
                        TransactionStatusId = c.Int(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Transaction.TransactionStatus", t => t.TransactionStatusId, cascadeDelete: true)
                .ForeignKey("Catalog.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("Catalog.Supplier", t => t.SupplierId, cascadeDelete: true)
                .ForeignKey("Transaction.TransactionType", t => t.TransactionTypeId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.EmployeeId)
                .Index(t => t.BranchId)
                .Index(t => t.TransactionTypeId)
                .Index(t => t.TransactionStatusId);
            
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
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.BranchId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.CityId);
            
            CreateTable(
                "Transaction.Sale",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Folio = c.String(maxLength: 10),
                        EmployeeId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        TransactionTypeId = c.Int(nullable: false),
                        TransactionStatusId = c.Int(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        TransactionDate = c.DateTime(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                  
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Catalog.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Catalog.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("Transaction.TransactionStatus", t => t.TransactionStatusId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.EmployeeId)
                .Index(t => t.BranchId)
                .Index(t => t.TransactionStatusId);
            
            CreateTable(
                "Transaction.Commission",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        Percentage = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.CommissionId)
                .ForeignKey("Transaction.Sale", t => t.CommissionId)
                .Index(t => t.CommissionId);
            
            CreateTable(
                "Catalog.Employee",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        JobPositionName = c.String(maxLength: 20),
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
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: false)
                .ForeignKey("Catalog.JobPosition", t => t.JobPositionName)
                .Index(t => t.JobPositionName)
                .Index(t => t.Name, name: "IDX_Name")
                .Index(t => t.FTR, unique: true, name: "Unq_FTR")
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.Email, unique: true, name: "Unq_Email")
                .Index(t => t.CityId);
            
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
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => new { t.UserId, t.EmployeeId })
                .ForeignKey("Catalog.Employee", t => t.EmployeeId, cascadeDelete: true)
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
                "Catalog.JobPosition",
                c => new
                    {
                        JobPositionName = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 50),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.JobPositionName);
            
            CreateTable(
                "Transaction.TransactionStatus",
                c => new
                    {
                        TransactionStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Code = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.TransactionStatusId);
            
            CreateTable(
                "Transaction.TransactionType",
                c => new
                    {
                        TransactionTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.TransactionTypeId);
            
            CreateTable(
                "Operative.ServiceAttachment",
                c => new
                    {
                        ServiceAttachmentId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 30),
                        Comment = c.String(maxLength: 100),
                        PathFile = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceAttachmentId)
                .ForeignKey("Operative.Service", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "Config.ServiceCategory",
                c => new
                    {
                        ServiceCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        ShortName = c.String(nullable: false, maxLength: 4),
                        Description = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        IsLocatable = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.ServiceCategoryId);
            
            CreateTable(
                "Operative.ServiceStatus",
                c => new
                    {
                        ServiceStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Code = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.ServiceStatusId);
            
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
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.StockId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "Transaction.PurchaseDetail",
                c => new
                    {
                        PurchaseDetailId = c.Int(nullable: false, identity: true),
                        PurchaseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        PartialAmount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseDetailId)
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("Transaction.Purchase", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.PurchaseId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Inventory.MeasureUnit",
                c => new
                    {
                        MeasureUnitId = c.String(nullable: false, maxLength: 5),
                        Description = c.String(maxLength: 50),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                        LockDate = c.DateTime(),
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
                .ForeignKey("Inventory.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("Inventory.Product", t => t.PackageId, cascadeDelete: true)
                .Index(t => t.PackageId)
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
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
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
                        LockDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.CategoryId);
            
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
                "Config.State",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
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
            DropForeignKey("Operative.AccountAddress", "AccountAddressId", "Operative.Service");
            DropForeignKey("Operative.AccountAddress", "CityId", "Config.City");
            DropForeignKey("Config.City", "StateId", "Config.State");
            DropForeignKey("Config.Locality", "CityId", "Config.City");
            DropForeignKey("Catalog.Client", "CityId", "Config.City");
            DropForeignKey("Operative.Service", "Client_ClientId", "Catalog.Client");
            DropForeignKey("Operative.Service", "ParentServiceId", "Operative.Service");
            DropForeignKey("Transaction.SaleDetail", "ServiceId", "Operative.Service");
            DropForeignKey("Inventory.Product", "SubCategoryId", "Inventory.SubCategory");
            DropForeignKey("Inventory.SubCategory", "CategoryId", "Inventory.Category");
            DropForeignKey("Transaction.SaleDetail", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.ProductImage", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.PackageDetail", "PackageId", "Inventory.Product");
            DropForeignKey("Inventory.PackageDetail", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Product", "MeasureUnitId", "Inventory.MeasureUnit");
            DropForeignKey("Inventory.Equivalence", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Equivalence", "ExternalProductId", "Inventory.ExternalProduct");
            DropForeignKey("Inventory.ExternalProduct", "SupplierId", "Catalog.Supplier");
            DropForeignKey("Transaction.Purchase", "TransactionTypeId", "Transaction.TransactionType");
            DropForeignKey("Transaction.Purchase", "SupplierId", "Catalog.Supplier");
            DropForeignKey("Transaction.PurchaseDetail", "PurchaseId", "Transaction.Purchase");
            DropForeignKey("Transaction.PurchaseDetail", "ProductId", "Inventory.Product");
            DropForeignKey("Transaction.Purchase", "EmployeeId", "Catalog.Employee");
            DropForeignKey("Inventory.Stock", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Stock", "BranchId", "Config.Branch");
            DropForeignKey("Operative.Service", "ServiceStatusId", "Operative.ServiceStatus");
            DropForeignKey("Operative.Service", "ServiceCategoryId", "Config.ServiceCategory");
            DropForeignKey("Operative.ServiceAttachment", "ServiceId", "Operative.Service");
            DropForeignKey("Operative.Service", "ClientId", "Catalog.Client");
            DropForeignKey("Operative.Service", "BranchId", "Config.Branch");
            DropForeignKey("Transaction.Sale", "TransactionType_TransactionTypeId2", "Transaction.TransactionType");
            DropForeignKey("Transaction.Sale", "TransactionType_TransactionTypeId1", "Transaction.TransactionType");
            DropForeignKey("Transaction.Sale", "TransactionType_TransactionTypeId", "Transaction.TransactionType");
            DropForeignKey("Transaction.Sale", "TransactionStatusId", "Transaction.TransactionStatus");
            DropForeignKey("Transaction.Purchase", "TransactionStatusId", "Transaction.TransactionStatus");
            DropForeignKey("Transaction.SaleDetail", "SaleId", "Transaction.Sale");
            DropForeignKey("Transaction.Sale", "EmployeeId", "Catalog.Employee");
            DropForeignKey("Catalog.Employee", "JobPositionName", "Catalog.JobPosition");
            DropForeignKey("Security.UserEmployee", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserRoles", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserLogins", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserClaims", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.UserEmployee", "EmployeeId", "Catalog.Employee");
            DropForeignKey("Catalog.Employee", "CityId", "Config.City");
            DropForeignKey("Transaction.Commission", "CommissionId", "Transaction.Sale");
            DropForeignKey("Transaction.Sale", "ClientId", "Catalog.Client");
            DropForeignKey("Transaction.Sale", "BranchId", "Config.Branch");
            DropForeignKey("Transaction.Purchase", "BranchId", "Config.Branch");
            DropForeignKey("Config.Branch", "CityId", "Config.City");
            DropForeignKey("Catalog.Supplier", "CityId", "Config.City");
            DropForeignKey("Inventory.Compatibility", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Compatibility", "CarYearId", "Config.CarYear");
            DropForeignKey("Config.CarYear", "CarModelId", "Config.CarModel");
            DropForeignKey("Config.CarModel", "CarMakeId", "Config.CarMake");
            DropForeignKey("Operative.Operation", "ServiceId", "Operative.Service");
            DropIndex("Security.AspNetRoles", "RoleNameIndex");
            DropIndex("Config.Locality", new[] { "CityId" });
            DropIndex("Inventory.SubCategory", new[] { "CategoryId" });
            DropIndex("Inventory.ProductImage", new[] { "ProductId" });
            DropIndex("Inventory.PackageDetail", new[] { "ProductId" });
            DropIndex("Inventory.PackageDetail", new[] { "PackageId" });
            DropIndex("Transaction.PurchaseDetail", new[] { "ProductId" });
            DropIndex("Transaction.PurchaseDetail", new[] { "PurchaseId" });
            DropIndex("Inventory.Stock", new[] { "BranchId" });
            DropIndex("Inventory.Stock", new[] { "ProductId" });
            DropIndex("Operative.ServiceAttachment", new[] { "ServiceId" });
            DropIndex("Security.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("Security.AspNetUserRoles", new[] { "UserId" });
            DropIndex("Security.AspNetUserLogins", new[] { "UserId" });
            DropIndex("Security.AspNetUserClaims", new[] { "UserId" });
            DropIndex("Security.AspNetUsers", "UserNameIndex");
            DropIndex("Security.UserEmployee", new[] { "EmployeeId" });
            DropIndex("Security.UserEmployee", new[] { "UserId" });
            DropIndex("Catalog.Employee", new[] { "CityId" });
            DropIndex("Catalog.Employee", "Unq_Email");
            DropIndex("Catalog.Employee", "Unq_Phone");
            DropIndex("Catalog.Employee", "Unq_FTR");
            DropIndex("Catalog.Employee", "IDX_Name");
            DropIndex("Catalog.Employee", new[] { "JobPositionName" });
            DropIndex("Transaction.Commission", new[] { "CommissionId" });
            DropIndex("Transaction.Sale", new[] { "TransactionType_TransactionTypeId2" });
            DropIndex("Transaction.Sale", new[] { "TransactionType_TransactionTypeId1" });
            DropIndex("Transaction.Sale", new[] { "TransactionType_TransactionTypeId" });
            DropIndex("Transaction.Sale", new[] { "TransactionStatusId" });
            DropIndex("Transaction.Sale", new[] { "BranchId" });
            DropIndex("Transaction.Sale", new[] { "EmployeeId" });
            DropIndex("Transaction.Sale", new[] { "ClientId" });
            DropIndex("Config.Branch", new[] { "CityId" });
            DropIndex("Config.Branch", "Unq_Phone");
            DropIndex("Transaction.Purchase", new[] { "TransactionStatusId" });
            DropIndex("Transaction.Purchase", new[] { "TransactionTypeId" });
            DropIndex("Transaction.Purchase", new[] { "BranchId" });
            DropIndex("Transaction.Purchase", new[] { "EmployeeId" });
            DropIndex("Transaction.Purchase", new[] { "SupplierId" });
            DropIndex("Catalog.Supplier", new[] { "CityId" });
            DropIndex("Catalog.Supplier", "Unq_Email");
            DropIndex("Catalog.Supplier", "Unq_Phone");
            DropIndex("Catalog.Supplier", "Unq_FTR");
            DropIndex("Catalog.Supplier", "IDX_Name");
            DropIndex("Catalog.Supplier", "Unq_BusinessName");
            DropIndex("Inventory.ExternalProduct", new[] { "SupplierId" });
            DropIndex("Inventory.Equivalence", new[] { "ExternalProductId" });
            DropIndex("Inventory.Equivalence", new[] { "ProductId" });
            DropIndex("Config.CarModel", new[] { "CarMakeId" });
            DropIndex("Config.CarYear", new[] { "CarModelId" });
            DropIndex("Inventory.Compatibility", new[] { "ProductId" });
            DropIndex("Inventory.Compatibility", new[] { "CarYearId" });
            DropIndex("Inventory.Product", new[] { "MeasureUnitId" });
            DropIndex("Inventory.Product", new[] { "SubCategoryId" });
            DropIndex("Transaction.SaleDetail", new[] { "ProductId" });
            DropIndex("Transaction.SaleDetail", new[] { "ServiceId" });
            DropIndex("Transaction.SaleDetail", new[] { "SaleId" });
            DropIndex("Operative.Operation", new[] { "ServiceId" });
            DropIndex("Operative.Service", new[] { "Client_ClientId" });
            DropIndex("Operative.Service", new[] { "ParentServiceId" });
            DropIndex("Operative.Service", new[] { "BranchId" });
            DropIndex("Operative.Service", new[] { "ServiceCategoryId" });
            DropIndex("Operative.Service", new[] { "ClientId" });
            DropIndex("Operative.Service", new[] { "ServiceStatusId" });
            DropIndex("Catalog.Client", new[] { "CityId" });
            DropIndex("Catalog.Client", "Unq_Email");
            DropIndex("Catalog.Client", "Unq_Phone");
            DropIndex("Catalog.Client", "Unq_FTR");
            DropIndex("Catalog.Client", "IDX_Name");
            DropIndex("Catalog.Client", "Unq_BusinessName");
            DropIndex("Config.City", new[] { "StateId" });
            DropIndex("Operative.AccountAddress", new[] { "CityId" });
            DropIndex("Operative.AccountAddress", new[] { "AccountAddressId" });
            DropTable("Security.AspNetRoles");
            DropTable("Config.State");
            DropTable("Config.Locality");
            DropTable("Inventory.Category");
            DropTable("Inventory.SubCategory");
            DropTable("Inventory.ProductImage");
            DropTable("Inventory.PackageDetail");
            DropTable("Inventory.MeasureUnit");
            DropTable("Transaction.PurchaseDetail");
            DropTable("Inventory.Stock");
            DropTable("Operative.ServiceStatus");
            DropTable("Config.ServiceCategory");
            DropTable("Operative.ServiceAttachment");
            DropTable("Transaction.TransactionType");
            DropTable("Transaction.TransactionStatus");
            DropTable("Catalog.JobPosition");
            DropTable("Security.AspNetUserRoles");
            DropTable("Security.AspNetUserLogins");
            DropTable("Security.AspNetUserClaims");
            DropTable("Security.AspNetUsers");
            DropTable("Security.UserEmployee");
            DropTable("Catalog.Employee");
            DropTable("Transaction.Commission");
            DropTable("Transaction.Sale");
            DropTable("Config.Branch");
            DropTable("Transaction.Purchase");
            DropTable("Catalog.Supplier");
            DropTable("Inventory.ExternalProduct");
            DropTable("Inventory.Equivalence");
            DropTable("Config.CarMake");
            DropTable("Config.CarModel");
            DropTable("Config.CarYear");
            DropTable("Inventory.Compatibility");
            DropTable("Inventory.Product");
            DropTable("Transaction.SaleDetail");
            DropTable("Operative.Operation");
            DropTable("Operative.Service");
            DropTable("Catalog.Client");
            DropTable("Config.City");
            DropTable("Operative.AccountAddress");
        }
    }
}
