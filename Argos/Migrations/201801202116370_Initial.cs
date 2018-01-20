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
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(nullable: false, maxLength: 30),
                        InNumber = c.String(nullable: false, maxLength: 30),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
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
                        ParentServiceId = c.Int(),
                        ServiceTypeId = c.Int(nullable: false),
                        ServiceStatusId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        Code = c.String(maxLength: 10),
                        HireDate = c.DateTime(nullable: false),
                        HirePrice = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceId)
                .ForeignKey("Catalog.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Operative.Service", t => t.ParentServiceId)
                .ForeignKey("Operative.ServiceStatus", t => t.ServiceStatusId, cascadeDelete: true)
                .ForeignKey("Config.ServiceType", t => t.ServiceTypeId, cascadeDelete: true)
                .Index(t => t.ParentServiceId)
                .Index(t => t.ServiceTypeId)
                .Index(t => t.ServiceStatusId)
                .Index(t => t.ClientId);
            
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
                "Operative.PolicyDetail",
                c => new
                    {
                        PolicyDetailId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        PaymentPeriod = c.Int(nullable: false),
                        PaymentTolerance = c.Int(nullable: false),
                        MaintenancePeriod = c.Int(nullable: false),
                        MaintenanceTolerance = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PolicyDetailId)
                .ForeignKey("Operative.Service", t => t.PolicyDetailId)
                .Index(t => t.PolicyDetailId);
            
            CreateTable(
                "Finances.SaleDetail",
                c => new
                    {
                        SaleDetailId = c.Int(nullable: false, identity: true),
                        ServiceId = c.Int(),
                        ProductId = c.Int(),
                        Quantity = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        PartialAmount = c.Double(nullable: false),
                        Sale_SaleId = c.Int(),
                    })
                .PrimaryKey(t => t.SaleDetailId)
                .ForeignKey("Catalog.Product", t => t.ProductId)
                .ForeignKey("Finances.Sale", t => t.Sale_SaleId)
                .ForeignKey("Operative.Service", t => t.ServiceId)
                .Index(t => t.ServiceId)
                .Index(t => t.ProductId)
                .Index(t => t.Sale_SaleId);
            
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
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("Catalog.ProductType", t => t.ProductTypeId, cascadeDelete: true)
                .Index(t => t.ProductTypeId);
            
            CreateTable(
                "Catalog.ProductType",
                c => new
                    {
                        ProductTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ProductTypeId);
            
            CreateTable(
                "Finances.Sale",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Folio = c.String(maxLength: 10),
                        SaleDate = c.DateTime(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                        SaleStatus_SaleId = c.Int(),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("Catalog.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Catalog.Employee", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("Finances.SaleStatus", t => t.SaleStatus_SaleId)
                .Index(t => t.ClientId)
                .Index(t => t.EmployeeId)
                .Index(t => t.SaleStatus_SaleId);
            
            CreateTable(
                "Finances.Commission",
                c => new
                    {
                        CommissionId = c.Int(nullable: false),
                        Percentage = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                        PaymentDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.CommissionId)
                .ForeignKey("Finances.Sale", t => t.CommissionId)
                .Index(t => t.CommissionId);
            
            CreateTable(
                "Catalog.Employee",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        JobPositionId = c.Int(nullable: false),
                        SSN = c.Int(nullable: false),
                        Commission = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        FTR = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(nullable: false, maxLength: 30),
                        InNumber = c.String(nullable: false, maxLength: 30),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: false)
                .ForeignKey("Config.JobPosition", t => t.JobPositionId, cascadeDelete: true)
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
                "Config.JobPosition",
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
                "Operative.ServiceAddress",
                c => new
                    {
                        ServiceAddressId = c.Int(nullable: false),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
                        OutNumber = c.String(nullable: false, maxLength: 30),
                        InNumber = c.String(nullable: false, maxLength: 30),
                        Location = c.String(nullable: false, maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceAddressId)
                .ForeignKey("Config.City", t => t.CityId, cascadeDelete: true)
                .ForeignKey("Operative.Service", t => t.ServiceAddressId)
                .Index(t => t.ServiceAddressId)
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
                "Operative.ServiceStatus",
                c => new
                    {
                        ServiceStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.ServiceStatusId);
            
            CreateTable(
                "Config.ServiceType",
                c => new
                    {
                        ServiceTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                        ShortName = c.String(nullable: false, maxLength: 3),
                        Description = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceTypeId);
            
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
                "Finances.SaleStatus",
                c => new
                    {
                        SaleId = c.Int(nullable: false),
                        SaleStatusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("Finances.Sale", t => t.SaleId)
                .Index(t => t.SaleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Finances.SaleStatus", "SaleId", "Finances.Sale");
            DropForeignKey("Security.AspNetUserRoles", "RoleId", "Security.AspNetRoles");
            DropForeignKey("Catalog.Client", "CityId", "Config.City");
            DropForeignKey("Operative.Service", "ServiceTypeId", "Config.ServiceType");
            DropForeignKey("Operative.Service", "ServiceStatusId", "Operative.ServiceStatus");
            DropForeignKey("Operative.OperationType", "ServiceId", "Operative.Service");
            DropForeignKey("Operative.ServiceAddress", "ServiceAddressId", "Operative.Service");
            DropForeignKey("Operative.ServiceAddress", "CityId", "Config.City");
            DropForeignKey("Finances.SaleDetail", "ServiceId", "Operative.Service");
            DropForeignKey("Finances.Sale", "SaleStatus_SaleId", "Finances.SaleStatus");
            DropForeignKey("Finances.SaleDetail", "Sale_SaleId", "Finances.Sale");
            DropForeignKey("Finances.Sale", "EmployeeId", "Catalog.Employee");
            DropForeignKey("Catalog.Employee", "JobPositionId", "Config.JobPosition");
            DropForeignKey("Security.UserEmployee", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserRoles", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserLogins", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserClaims", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.UserEmployee", "EmployeeId", "Catalog.Employee");
            DropForeignKey("Catalog.Employee", "CityId", "Config.City");
            DropForeignKey("Finances.Commission", "CommissionId", "Finances.Sale");
            DropForeignKey("Finances.Sale", "ClientId", "Catalog.Client");
            DropForeignKey("Finances.SaleDetail", "ProductId", "Catalog.Product");
            DropForeignKey("Catalog.Product", "ProductTypeId", "Catalog.ProductType");
            DropForeignKey("Operative.PolicyDetail", "PolicyDetailId", "Operative.Service");
            DropForeignKey("Operative.Service", "ParentServiceId", "Operative.Service");
            DropForeignKey("Operative.Operation", "ServiceId", "Operative.Service");
            DropForeignKey("Operative.Service", "ClientId", "Catalog.Client");
            DropForeignKey("Config.City", "StateId", "Config.State");
            DropIndex("Finances.SaleStatus", new[] { "SaleId" });
            DropIndex("Security.AspNetRoles", "RoleNameIndex");
            DropIndex("Operative.OperationType", new[] { "ServiceId" });
            DropIndex("Operative.ServiceAddress", new[] { "CityId" });
            DropIndex("Operative.ServiceAddress", new[] { "ServiceAddressId" });
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
            DropIndex("Finances.Commission", new[] { "CommissionId" });
            DropIndex("Finances.Sale", new[] { "SaleStatus_SaleId" });
            DropIndex("Finances.Sale", new[] { "EmployeeId" });
            DropIndex("Finances.Sale", new[] { "ClientId" });
            DropIndex("Catalog.Product", new[] { "ProductTypeId" });
            DropIndex("Finances.SaleDetail", new[] { "Sale_SaleId" });
            DropIndex("Finances.SaleDetail", new[] { "ProductId" });
            DropIndex("Finances.SaleDetail", new[] { "ServiceId" });
            DropIndex("Operative.PolicyDetail", new[] { "PolicyDetailId" });
            DropIndex("Operative.Operation", new[] { "ServiceId" });
            DropIndex("Operative.Service", new[] { "ClientId" });
            DropIndex("Operative.Service", new[] { "ServiceStatusId" });
            DropIndex("Operative.Service", new[] { "ServiceTypeId" });
            DropIndex("Operative.Service", new[] { "ParentServiceId" });
            DropIndex("Catalog.Client", new[] { "CityId" });
            DropIndex("Catalog.Client", "Unq_Email");
            DropIndex("Catalog.Client", "Unq_Phone");
            DropIndex("Catalog.Client", "Unq_FTR");
            DropIndex("Catalog.Client", "Unq_BusinessName");
            DropIndex("Config.State", "IDX_Name");
            DropIndex("Config.City", "IDX_StateId");
            DropTable("Finances.SaleStatus");
            DropTable("Security.AspNetRoles");
            DropTable("Config.ServiceType");
            DropTable("Operative.ServiceStatus");
            DropTable("Operative.OperationType");
            DropTable("Operative.ServiceAddress");
            DropTable("Config.JobPosition");
            DropTable("Security.AspNetUserRoles");
            DropTable("Security.AspNetUserLogins");
            DropTable("Security.AspNetUserClaims");
            DropTable("Security.AspNetUsers");
            DropTable("Security.UserEmployee");
            DropTable("Catalog.Employee");
            DropTable("Finances.Commission");
            DropTable("Finances.Sale");
            DropTable("Catalog.ProductType");
            DropTable("Catalog.Product");
            DropTable("Finances.SaleDetail");
            DropTable("Operative.PolicyDetail");
            DropTable("Operative.Operation");
            DropTable("Operative.Service");
            DropTable("Catalog.Client");
            DropTable("Config.State");
            DropTable("Config.City");
        }
    }
}
