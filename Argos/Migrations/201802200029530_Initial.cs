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
                        OutNumber = c.String(maxLength: 6),
                        InNumber = c.String(nullable: false, maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AccountAddressId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("Operative.Service", t => t.AccountAddressId)
                .Index(t => t.AccountAddressId)
                .Index(t => t.CityId);
            
            CreateTable(
                "Config.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        StateId = c.Int(nullable: false),
                        Code = c.String(maxLength: 15),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("Config.State", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId, name: "IDX_StateId");
            
            CreateTable(
                "Config.State",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 15),
                        Name = c.String(nullable: false, maxLength: 50),
                        ShorName = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.StateId)
                .Index(t => t.Name, unique: true, name: "IDX_Name");
            
            CreateTable(
                "Operative.Service",
                c => new
                    {
                        ServiceId = c.Int(nullable: false, identity: true),
                        ServiceStatusId = c.Int(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                        HirePrice = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
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
                .ForeignKey("Operative.ServiceStatus", t => t.ServiceStatusId, cascadeDelete: true)
                .ForeignKey("Operative.Service", t => t.ParentServiceId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Catalog.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Config.ServiceCategory", t => t.ServiceCategoryId, cascadeDelete: true)
                .Index(t => t.ServiceStatusId)
                .Index(t => t.ClientId)
                .Index(t => t.ServiceCategoryId)
                .Index(t => t.BranchId)
                .Index(t => t.ParentServiceId);
            
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
                        OutNumber = c.String(maxLength: 6),
                        InNumber = c.String(nullable: false, maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BranchId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.CityId);
            
            CreateTable(
                "Operative.Inventory",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        Available = c.Double(nullable: false),
                        Reserved = c.Double(nullable: false),
                        Row = c.String(maxLength: 40),
                        Ledge = c.String(maxLength: 40),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Catalog.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "Catalog.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductSubCategoryId = c.Int(nullable: false),
                        Code = c.String(maxLength: 30),
                        Description = c.String(maxLength: 250),
                        Cost = c.Double(nullable: false),
                        Percentage = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        MiddlePercentage = c.Int(nullable: false),
                        MiddlePrice = c.Double(nullable: false),
                        LowerPercentage = c.Int(nullable: false),
                        LowerPrice = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("Config.ProductSubCategory", t => t.ProductSubCategoryId, cascadeDelete: true)
                .Index(t => t.ProductSubCategoryId);
            
            CreateTable(
                "Config.ProductSubCategory",
                c => new
                    {
                        ProductSubCategoryId = c.Int(nullable: false, identity: true),
                        ProductCategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductSubCategoryId)
                .ForeignKey("Config.ProductCategory", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "Config.ProductCategory",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Description = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductCategoryId);
            
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
                .ForeignKey("Catalog.Product", t => t.ProductId)
                .ForeignKey("Operative.Service", t => t.ServiceId)
                .ForeignKey("Transaction.Sale", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.SaleId)
                .Index(t => t.ServiceId)
                .Index(t => t.ProductId);
            
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
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Catalog.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Catalog.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("Transaction.TransactionType", t => t.TransactionTypeId)
                .ForeignKey("Transaction.TransactionStatus", t => t.TransactionStatusId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.EmployeeId)
                .Index(t => t.BranchId)
                .Index(t => t.TransactionStatusId)
                .Index(t => t.TransactionTypeId);
            
            CreateTable(
                "Catalog.Client",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(maxLength: 200),
                        Name = c.String(nullable: false, maxLength: 150),
                        FTR = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(maxLength: 6),
                        InNumber = c.String(nullable: false, maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
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
                "Operative.Operation",
                c => new
                    {
                        OperationId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(nullable: false),
                        Folio = c.String(maxLength: 10),
                        RiseDate = c.DateTime(nullable: false),
                        ScheduleDate = c.DateTime(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OperationId)
                .ForeignKey("Operative.Service", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
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
                "Operative.ServiceStatus",
                c => new
                    {
                        ServiceStatusId = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ServiceStatusId);
            
            CreateTable(
                "Transaction.Commission",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        Percentage = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommissionId)
                .ForeignKey("Transaction.Sale", t => t.CommissionId)
                .Index(t => t.CommissionId);
            
            CreateTable(
                "Catalog.Employee",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        JobPositionId = c.Int(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        SSN = c.String(maxLength: 11),
                        Commission = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        FTR = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(maxLength: 6),
                        InNumber = c.String(nullable: false, maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: false)
                .ForeignKey("Catalog.JobPosition", t => t.JobPositionId, cascadeDelete: true)
                .Index(t => t.JobPositionId)
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
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
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
                        JobPositionId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Description = c.String(maxLength: 50),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.JobPositionId);
            
            CreateTable(
                "Transaction.TransactionStatus",
                c => new
                    {
                        TransactionStatusId = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 20),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TransactionStatusId);
            
            CreateTable(
                "Transaction.Purchase",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        ProviderId = c.Int(nullable: false),
                        Bill = c.String(maxLength: 10),
                        TransactionDate = c.DateTime(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        TransactionTypeId = c.Int(nullable: false),
                        TransactionStatusId = c.Int(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Catalog.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("Security.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("Transaction.TransactionStatus", t => t.TransactionStatusId, cascadeDelete: true)
                .ForeignKey("Transaction.TransactionType", t => t.TransactionTypeId, cascadeDelete: true)
                .Index(t => t.ProviderId)
                .Index(t => t.EmployeeId)
                .Index(t => t.BranchId)
                .Index(t => t.TransactionTypeId)
                .Index(t => t.TransactionStatusId);
            
            CreateTable(
                "Security.Providers",
                c => new
                    {
                        ProviderId = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(maxLength: 200),
                        Name = c.String(nullable: false, maxLength: 150),
                        FTR = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        IsActive = c.Boolean(nullable: false),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(maxLength: 6),
                        InNumber = c.String(nullable: false, maxLength: 6),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProviderId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: false)
                .Index(t => t.BusinessName, unique: true, name: "Unq_BusinessName")
                .Index(t => t.Name, name: "IDX_Name")
                .Index(t => t.FTR, unique: true, name: "Unq_FTR")
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.Email, unique: true, name: "Unq_Email")
                .Index(t => t.CityId);
            
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
                .ForeignKey("Catalog.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("Transaction.Purchase", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.PurchaseId)
                .Index(t => t.ProductId);
            
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
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceCategoryId);
            
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
            DropForeignKey("Operative.Service", "ServiceCategoryId", "Config.ServiceCategory");
            DropForeignKey("Operative.Service", "ClientId", "Catalog.Client");
            DropForeignKey("Operative.Service", "BranchId", "Config.Branch");
            DropForeignKey("Transaction.Sale", "TransactionType_TransactionTypeId2", "Transaction.TransactionType");
            DropForeignKey("Transaction.Sale", "TransactionStatusId", "Transaction.TransactionStatus");
            DropForeignKey("Transaction.Purchase", "TransactionTypeId", "Transaction.TransactionType");
            DropForeignKey("Transaction.Sale", "TransactionType_TransactionTypeId1", "Transaction.TransactionType");
            DropForeignKey("Transaction.Sale", "TransactionType_TransactionTypeId", "Transaction.TransactionType");
            DropForeignKey("Transaction.Purchase", "TransactionStatusId", "Transaction.TransactionStatus");
            DropForeignKey("Transaction.PurchaseDetail", "PurchaseId", "Transaction.Purchase");
            DropForeignKey("Transaction.PurchaseDetail", "ProductId", "Catalog.Product");
            DropForeignKey("Transaction.Purchase", "ProviderId", "Security.Providers");
            DropForeignKey("Security.Providers", "CityId", "Config.City");
            DropForeignKey("Transaction.Purchase", "EmployeeId", "Catalog.Employee");
            DropForeignKey("Transaction.Purchase", "BranchId", "Config.Branch");
            DropForeignKey("Transaction.SaleDetail", "SaleId", "Transaction.Sale");
            DropForeignKey("Transaction.Sale", "EmployeeId", "Catalog.Employee");
            DropForeignKey("Catalog.Employee", "JobPositionId", "Catalog.JobPosition");
            DropForeignKey("Security.UserEmployee", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserRoles", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserLogins", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserClaims", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.UserEmployee", "EmployeeId", "Catalog.Employee");
            DropForeignKey("Catalog.Employee", "CityId", "Config.City");
            DropForeignKey("Transaction.Commission", "CommissionId", "Transaction.Sale");
            DropForeignKey("Transaction.Sale", "ClientId", "Catalog.Client");
            DropForeignKey("Catalog.Client", "CityId", "Config.City");
            DropForeignKey("Operative.Service", "Client_ClientId", "Catalog.Client");
            DropForeignKey("Operative.Service", "ParentServiceId", "Operative.Service");
            DropForeignKey("Operative.Service", "ServiceStatusId", "Operative.ServiceStatus");
            DropForeignKey("Operative.ServiceAttachment", "ServiceId", "Operative.Service");
            DropForeignKey("Transaction.SaleDetail", "ServiceId", "Operative.Service");
            DropForeignKey("Operative.Operation", "ServiceId", "Operative.Service");
            DropForeignKey("Transaction.Sale", "BranchId", "Config.Branch");
            DropForeignKey("Transaction.SaleDetail", "ProductId", "Catalog.Product");
            DropForeignKey("Catalog.Product", "ProductSubCategoryId", "Config.ProductSubCategory");
            DropForeignKey("Config.ProductSubCategory", "ProductCategoryId", "Config.ProductCategory");
            DropForeignKey("Operative.Inventory", "ProductId", "Catalog.Product");
            DropForeignKey("Operative.Inventory", "BranchId", "Config.Branch");
            DropForeignKey("Config.Branch", "CityId", "Config.City");
            DropForeignKey("Operative.AccountAddress", "CityId", "Config.City");
            DropForeignKey("Config.City", "StateId", "Config.State");
            DropIndex("Security.AspNetRoles", "RoleNameIndex");
            DropIndex("Transaction.PurchaseDetail", new[] { "ProductId" });
            DropIndex("Transaction.PurchaseDetail", new[] { "PurchaseId" });
            DropIndex("Security.Providers", new[] { "CityId" });
            DropIndex("Security.Providers", "Unq_Email");
            DropIndex("Security.Providers", "Unq_Phone");
            DropIndex("Security.Providers", "Unq_FTR");
            DropIndex("Security.Providers", "IDX_Name");
            DropIndex("Security.Providers", "Unq_BusinessName");
            DropIndex("Transaction.Purchase", new[] { "TransactionStatusId" });
            DropIndex("Transaction.Purchase", new[] { "TransactionTypeId" });
            DropIndex("Transaction.Purchase", new[] { "BranchId" });
            DropIndex("Transaction.Purchase", new[] { "EmployeeId" });
            DropIndex("Transaction.Purchase", new[] { "ProviderId" });
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
            DropIndex("Catalog.Employee", new[] { "JobPositionId" });
            DropIndex("Transaction.Commission", new[] { "CommissionId" });
            DropIndex("Operative.ServiceAttachment", new[] { "ServiceId" });
            DropIndex("Operative.Operation", new[] { "ServiceId" });
            DropIndex("Catalog.Client", new[] { "CityId" });
            DropIndex("Catalog.Client", "Unq_Email");
            DropIndex("Catalog.Client", "Unq_Phone");
            DropIndex("Catalog.Client", "Unq_FTR");
            DropIndex("Catalog.Client", "IDX_Name");
            DropIndex("Catalog.Client", "Unq_BusinessName");
            DropIndex("Transaction.Sale", new[] { "TransactionType_TransactionTypeId2" });
            DropIndex("Transaction.Sale", new[] { "TransactionType_TransactionTypeId1" });
            DropIndex("Transaction.Sale", new[] { "TransactionType_TransactionTypeId" });
            DropIndex("Transaction.Sale", new[] { "TransactionStatusId" });
            DropIndex("Transaction.Sale", new[] { "BranchId" });
            DropIndex("Transaction.Sale", new[] { "EmployeeId" });
            DropIndex("Transaction.Sale", new[] { "ClientId" });
            DropIndex("Transaction.SaleDetail", new[] { "ProductId" });
            DropIndex("Transaction.SaleDetail", new[] { "ServiceId" });
            DropIndex("Transaction.SaleDetail", new[] { "SaleId" });
            DropIndex("Config.ProductSubCategory", new[] { "ProductCategoryId" });
            DropIndex("Catalog.Product", new[] { "ProductSubCategoryId" });
            DropIndex("Operative.Inventory", new[] { "BranchId" });
            DropIndex("Operative.Inventory", new[] { "ProductId" });
            DropIndex("Config.Branch", new[] { "CityId" });
            DropIndex("Config.Branch", "Unq_Phone");
            DropIndex("Operative.Service", new[] { "Client_ClientId" });
            DropIndex("Operative.Service", new[] { "ParentServiceId" });
            DropIndex("Operative.Service", new[] { "BranchId" });
            DropIndex("Operative.Service", new[] { "ServiceCategoryId" });
            DropIndex("Operative.Service", new[] { "ClientId" });
            DropIndex("Operative.Service", new[] { "ServiceStatusId" });
            DropIndex("Config.State", "IDX_Name");
            DropIndex("Config.City", "IDX_StateId");
            DropIndex("Operative.AccountAddress", new[] { "CityId" });
            DropIndex("Operative.AccountAddress", new[] { "AccountAddressId" });
            DropTable("Security.AspNetRoles");
            DropTable("Config.ServiceCategory");
            DropTable("Transaction.TransactionType");
            DropTable("Transaction.PurchaseDetail");
            DropTable("Security.Providers");
            DropTable("Transaction.Purchase");
            DropTable("Transaction.TransactionStatus");
            DropTable("Catalog.JobPosition");
            DropTable("Security.AspNetUserRoles");
            DropTable("Security.AspNetUserLogins");
            DropTable("Security.AspNetUserClaims");
            DropTable("Security.AspNetUsers");
            DropTable("Security.UserEmployee");
            DropTable("Catalog.Employee");
            DropTable("Transaction.Commission");
            DropTable("Operative.ServiceStatus");
            DropTable("Operative.ServiceAttachment");
            DropTable("Operative.Operation");
            DropTable("Catalog.Client");
            DropTable("Transaction.Sale");
            DropTable("Transaction.SaleDetail");
            DropTable("Config.ProductCategory");
            DropTable("Config.ProductSubCategory");
            DropTable("Catalog.Product");
            DropTable("Operative.Inventory");
            DropTable("Config.Branch");
            DropTable("Operative.Service");
            DropTable("Config.State");
            DropTable("Config.City");
            DropTable("Operative.AccountAddress");
        }
    }
}
