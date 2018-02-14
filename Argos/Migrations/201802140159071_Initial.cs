namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                "Catalog.Client",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(maxLength: 130),
                        Name = c.String(nullable: false, maxLength: 100),
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
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                        ClientId = c.Int(),
                        Code = c.String(maxLength: 12),
                        ServiceTypeId = c.Int(),
                        ParentServiceId = c.Int(),
                        PaymentPeriod = c.Int(),
                        PaymentTolerance = c.Int(),
                        MaintenancePeriod = c.Int(),
                        MaintenanceTolerance = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Branch_BranchId = c.Int(),
                        Client_ClientId = c.Int(),
                    })
                .PrimaryKey(t => t.ServiceId)
                .ForeignKey("Catalog.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Config.ServiceStatus", t => t.ServiceStatusId, cascadeDelete: true)
                .ForeignKey("Catalog.ServiceType", t => t.ServiceTypeId, cascadeDelete: true)
                .ForeignKey("Config.Branch", t => t.Branch_BranchId)
                .ForeignKey("Operative.Service", t => t.ParentServiceId)
                .ForeignKey("Catalog.Client", t => t.Client_ClientId)
                .Index(t => t.ServiceStatusId)
                .Index(t => t.ClientId)
                .Index(t => t.ServiceTypeId)
                .Index(t => t.ParentServiceId)
                .Index(t => t.Branch_BranchId)
                .Index(t => t.Client_ClientId);
            
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
                "Sales.SaleDetail",
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
                .ForeignKey("Sales.Sale", t => t.SaleId, cascadeDelete: true)
                .ForeignKey("Operative.Service", t => t.ServiceId)
                .Index(t => t.SaleId)
                .Index(t => t.ServiceId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Catalog.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductTypeId = c.Int(nullable: false),
                        Code = c.String(maxLength: 30),
                        Description = c.String(),
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
                .ForeignKey("Catalog.ProductType", t => t.ProductTypeId, cascadeDelete: true)
                .Index(t => t.ProductTypeId);
            
            CreateTable(
                "Catalog.ProductStock",
                c => new
                    {
                        ProductStockId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        BranchId = c.Int(nullable: false),
                        Quantity = c.Double(nullable: false),
                        Reserved = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductStockId)
                .ForeignKey("Config.Branch", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("Catalog.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
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
                "Operative.OperationType",
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
                "Config.ServiceStatus",
                c => new
                    {
                        ServiceStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ServiceStatusId);
            
            CreateTable(
                "Catalog.ServiceType",
                c => new
                    {
                        ServiceTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        ShortName = c.String(nullable: false, maxLength: 4),
                        Description = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceTypeId);
            
            CreateTable(
                "Catalog.ProductType",
                c => new
                    {
                        ProductTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Description = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductTypeId);
            
            CreateTable(
                "Sales.Sale",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        Folio = c.String(maxLength: 10),
                        SaleDate = c.DateTime(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        ClientId = c.Int(nullable: false),
                        SaleStatusId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("Catalog.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Catalog.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("Config.SaleStatus", t => t.SaleStatusId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.SaleStatusId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "Sales.Commission",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        Percentage = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommissionId)
                .ForeignKey("Sales.Sale", t => t.CommissionId)
                .Index(t => t.CommissionId);
            
            CreateTable(
                "Catalog.Employee",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        JobPositionId = c.Int(nullable: false),
                        SSN = c.String(maxLength: 11),
                        Commission = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
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
                "Config.SaleStatus",
                c => new
                    {
                        SaleStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.SaleStatusId);
            
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
            DropForeignKey("Catalog.Client", "CityId", "Config.City");
            DropForeignKey("Operative.Service", "Client_ClientId", "Catalog.Client");
            DropForeignKey("Operative.Service", "ParentServiceId", "Operative.Service");
            DropForeignKey("Sales.SaleDetail", "ServiceId", "Operative.Service");
            DropForeignKey("Sales.Sale", "SaleStatusId", "Config.SaleStatus");
            DropForeignKey("Sales.SaleDetail", "SaleId", "Sales.Sale");
            DropForeignKey("Sales.Sale", "EmployeeId", "Catalog.Employee");
            DropForeignKey("Catalog.Employee", "JobPositionId", "Catalog.JobPosition");
            DropForeignKey("Security.UserEmployee", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserRoles", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserLogins", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserClaims", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.UserEmployee", "EmployeeId", "Catalog.Employee");
            DropForeignKey("Catalog.Employee", "CityId", "Config.City");
            DropForeignKey("Sales.Commission", "CommissionId", "Sales.Sale");
            DropForeignKey("Sales.Sale", "ClientId", "Catalog.Client");
            DropForeignKey("Sales.SaleDetail", "ProductId", "Catalog.Product");
            DropForeignKey("Catalog.Product", "ProductTypeId", "Catalog.ProductType");
            DropForeignKey("Catalog.ProductStock", "ProductId", "Catalog.Product");
            DropForeignKey("Operative.Service", "Branch_BranchId", "Config.Branch");
            DropForeignKey("Operative.Service", "ServiceTypeId", "Catalog.ServiceType");
            DropForeignKey("Operative.Service", "ServiceStatusId", "Config.ServiceStatus");
            DropForeignKey("Operative.OperationType", "ServiceId", "Operative.Service");
            DropForeignKey("Operative.Service", "ClientId", "Catalog.Client");
            DropForeignKey("Operative.AccountAddress", "AccountAddressId", "Operative.Service");
            DropForeignKey("Operative.AccountAddress", "CityId", "Config.City");
            DropForeignKey("Catalog.ProductStock", "BranchId", "Config.Branch");
            DropForeignKey("Config.Branch", "CityId", "Config.City");
            DropForeignKey("Operative.Operation", "ServiceId", "Operative.Service");
            DropForeignKey("Config.City", "StateId", "Config.State");
            DropIndex("Security.AspNetRoles", "RoleNameIndex");
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
            DropIndex("Catalog.Employee", new[] { "JobPositionId" });
            DropIndex("Sales.Commission", new[] { "CommissionId" });
            DropIndex("Sales.Sale", new[] { "EmployeeId" });
            DropIndex("Sales.Sale", new[] { "SaleStatusId" });
            DropIndex("Sales.Sale", new[] { "ClientId" });
            DropIndex("Operative.OperationType", new[] { "ServiceId" });
            DropIndex("Operative.AccountAddress", new[] { "CityId" });
            DropIndex("Operative.AccountAddress", new[] { "AccountAddressId" });
            DropIndex("Config.Branch", new[] { "CityId" });
            DropIndex("Config.Branch", "Unq_Phone");
            DropIndex("Catalog.ProductStock", new[] { "BranchId" });
            DropIndex("Catalog.ProductStock", new[] { "ProductId" });
            DropIndex("Catalog.Product", new[] { "ProductTypeId" });
            DropIndex("Sales.SaleDetail", new[] { "ProductId" });
            DropIndex("Sales.SaleDetail", new[] { "ServiceId" });
            DropIndex("Sales.SaleDetail", new[] { "SaleId" });
            DropIndex("Operative.Operation", new[] { "ServiceId" });
            DropIndex("Operative.Service", new[] { "Client_ClientId" });
            DropIndex("Operative.Service", new[] { "Branch_BranchId" });
            DropIndex("Operative.Service", new[] { "ParentServiceId" });
            DropIndex("Operative.Service", new[] { "ServiceTypeId" });
            DropIndex("Operative.Service", new[] { "ClientId" });
            DropIndex("Operative.Service", new[] { "ServiceStatusId" });
            DropIndex("Catalog.Client", new[] { "CityId" });
            DropIndex("Catalog.Client", "Unq_Email");
            DropIndex("Catalog.Client", "Unq_Phone");
            DropIndex("Catalog.Client", "Unq_FTR");
            DropIndex("Catalog.Client", "Unq_BusinessName");
            DropIndex("Config.State", "IDX_Name");
            DropIndex("Config.City", "IDX_StateId");
            DropTable("Security.AspNetRoles");
            DropTable("Config.SaleStatus");
            DropTable("Catalog.JobPosition");
            DropTable("Security.AspNetUserRoles");
            DropTable("Security.AspNetUserLogins");
            DropTable("Security.AspNetUserClaims");
            DropTable("Security.AspNetUsers");
            DropTable("Security.UserEmployee");
            DropTable("Catalog.Employee");
            DropTable("Sales.Commission");
            DropTable("Sales.Sale");
            DropTable("Catalog.ProductType");
            DropTable("Catalog.ServiceType");
            DropTable("Config.ServiceStatus");
            DropTable("Operative.OperationType");
            DropTable("Operative.AccountAddress");
            DropTable("Config.Branch");
            DropTable("Catalog.ProductStock");
            DropTable("Catalog.Product");
            DropTable("Sales.SaleDetail");
            DropTable("Operative.Operation");
            DropTable("Operative.Service");
            DropTable("Catalog.Client");
            DropTable("Config.State");
            DropTable("Config.City");
        }
    }
}
