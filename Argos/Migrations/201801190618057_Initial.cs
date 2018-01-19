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
                .ForeignKey("Config.State", t => t.StateId, cascadeDelete: false)
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
                        Code = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 100),
                        FTR = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
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
                .Index(t => t.Code, unique: true, name: "Unq_Code")
                .Index(t => t.FTR, unique: true, name: "Unq_FTR")
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.Email, unique: true, name: "Unq_Email")
                .Index(t => t.CityId);
            
            CreateTable(
                "Operative.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        AccountTypeId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        CityId = c.Int(),
                        Code = c.String(nullable: false, maxLength: 10),
                        ContractDate = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        Street = c.String(maxLength: 80),
                        Location = c.String(maxLength: 50),
                        ZipCode = c.String(maxLength: 10),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId)
                .ForeignKey("Config.AccountType", t => t.AccountTypeId, cascadeDelete: true)
                .ForeignKey("Config.City", t => t.CityId)
                .ForeignKey("Catalog.Client", t => t.ClientId, cascadeDelete: false)
                .Index(t => t.AccountTypeId)
                .Index(t => t.ClientId)
                .Index(t => t.CityId);
            
            CreateTable(
                "Config.AccountType",
                c => new
                    {
                        AccountTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 15),
                        ShortName = c.String(nullable: false, maxLength: 3),
                        Description = c.String(maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AccountTypeId);
            
            CreateTable(
                "Config.PolicyType",
                c => new
                    {
                        PolicyTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        Description = c.String(maxLength: 50),
                        PaymentPeriod = c.Int(nullable: false),
                        PaymentTolerance = c.Int(nullable: false),
                        MaintenancePeriod = c.Int(nullable: false),
                        MaintenanceTolerance = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                        AccountType_AccountTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.PolicyTypeId)
                .ForeignKey("Config.AccountType", t => t.AccountType_AccountTypeId)
                .Index(t => t.AccountType_AccountTypeId);
            
            CreateTable(
                "Operative.Policy",
                c => new
                    {
                        PolicyId = c.Int(nullable: false),
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
                .PrimaryKey(t => t.PolicyId)
                .ForeignKey("Operative.Account", t => t.PolicyId)
                .Index(t => t.PolicyId);
            
            CreateTable(
                "Operative.SchedulePayment",
                c => new
                    {
                        SchedulePaymentId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(),
                        PolicyId = c.Int(),
                        ExtensionId = c.Int(),
                        ServiceId = c.Int(),
                        PaymentDueDate = c.DateTime(nullable: false),
                        PaidDate = c.DateTime(nullable: false),
                        Comment = c.String(maxLength: 100),
                        Amount = c.Double(nullable: false),
                        Status = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SchedulePaymentId)
                .ForeignKey("Operative.Account", t => t.AccountId)
                .ForeignKey("Operative.Extension", t => t.ExtensionId)
                .ForeignKey("Operative.Policy", t => t.PolicyId)
                .Index(t => t.AccountId)
                .Index(t => t.PolicyId)
                .Index(t => t.ExtensionId);
            
            CreateTable(
                "Operative.Extension",
                c => new
                    {
                        ExtensionId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        ContractDate = c.DateTime(nullable: false),
                        Price = c.Double(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ExtensionId)
                .ForeignKey("Operative.Account", t => t.AccountId, cascadeDelete: true)
                .Index(t => t.AccountId);
            
            CreateTable(
                "Finances.SaleDetail",
                c => new
                    {
                        SaleDetailId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(),
                        ExtensionId = c.Int(),
                        PolicyId = c.Int(),
                        Quantity = c.Double(nullable: false),
                        Price = c.Double(nullable: false),
                        PartialAmount = c.Double(nullable: false),
                        Sale_SaleId = c.Int(),
                    })
                .PrimaryKey(t => t.SaleDetailId)
                .ForeignKey("Operative.Account", t => t.AccountId)
                .ForeignKey("Operative.Extension", t => t.ExtensionId)
                .ForeignKey("Operative.Policy", t => t.PolicyId)
                .ForeignKey("Finances.Sale", t => t.Sale_SaleId)
                .Index(t => t.AccountId)
                .Index(t => t.ExtensionId)
                .Index(t => t.PolicyId)
                .Index(t => t.Sale_SaleId);
            
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
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("Catalog.Client", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("Catalog.Employee", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.EmployeeId);
            
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
                        Code = c.String(nullable: false, maxLength: 10),
                        Name = c.String(nullable: false, maxLength: 100),
                        FTR = c.String(maxLength: 15),
                        Phone = c.String(maxLength: 15),
                        Email = c.String(maxLength: 150),
                        CityId = c.Int(nullable: false),
                        Street = c.String(nullable: false, maxLength: 80),
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
                .Index(t => t.Code, unique: true, name: "Unq_Code")
                .Index(t => t.FTR, unique: true, name: "Unq_FTR")
                .Index(t => t.Phone, unique: true, name: "Unq_Phone")
                .Index(t => t.Email, unique: true, name: "Unq_Email")
                .Index(t => t.CityId);
            
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
                "Operative.ScheduleService",
                c => new
                    {
                        ScheduleServiceId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(),
                        ExtensionId = c.Int(),
                        ProgrammedDate = c.DateTime(nullable: false),
                        BeginDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ScheduleServiceId)
                .ForeignKey("Operative.Account", t => t.AccountId)
                .ForeignKey("Operative.Extension", t => t.ExtensionId)
                .Index(t => t.AccountId)
                .Index(t => t.ExtensionId);
            
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
                "Security.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("Security.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("Security.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("Security.AspNetUserRoles", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserLogins", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserClaims", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserRoles", "RoleId", "Security.AspNetRoles");
            DropForeignKey("Catalog.Client", "CityId", "Config.City");
            DropForeignKey("Operative.SchedulePayment", "PolicyId", "Operative.Policy");
            DropForeignKey("Operative.ScheduleService", "ExtensionId", "Operative.Extension");
            DropForeignKey("Operative.ScheduleService", "AccountId", "Operative.Account");
            DropForeignKey("Operative.SchedulePayment", "ExtensionId", "Operative.Extension");
            DropForeignKey("Finances.SaleDetail", "Sale_SaleId", "Finances.Sale");
            DropForeignKey("Finances.Sale", "EmployeeId", "Catalog.Employee");
            DropForeignKey("Catalog.Employee", "JobPositionId", "Config.JobPosition");
            DropForeignKey("Catalog.Employee", "CityId", "Config.City");
            DropForeignKey("Finances.Commission", "CommissionId", "Finances.Sale");
            DropForeignKey("Finances.Sale", "ClientId", "Catalog.Client");
            DropForeignKey("Finances.SaleDetail", "PolicyId", "Operative.Policy");
            DropForeignKey("Finances.SaleDetail", "ExtensionId", "Operative.Extension");
            DropForeignKey("Finances.SaleDetail", "AccountId", "Operative.Account");
            DropForeignKey("Operative.Extension", "AccountId", "Operative.Account");
            DropForeignKey("Operative.SchedulePayment", "AccountId", "Operative.Account");
            DropForeignKey("Operative.Policy", "PolicyId", "Operative.Account");
            DropForeignKey("Operative.Account", "ClientId", "Catalog.Client");
            DropForeignKey("Operative.Account", "CityId", "Config.City");
            DropForeignKey("Config.PolicyType", "AccountType_AccountTypeId", "Config.AccountType");
            DropForeignKey("Operative.Account", "AccountTypeId", "Config.AccountType");
            DropForeignKey("Config.City", "StateId", "Config.State");
            DropIndex("Security.AspNetUserLogins", new[] { "UserId" });
            DropIndex("Security.AspNetUserClaims", new[] { "UserId" });
            DropIndex("Security.AspNetUsers", "UserNameIndex");
            DropIndex("Security.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("Security.AspNetUserRoles", new[] { "UserId" });
            DropIndex("Security.AspNetRoles", "RoleNameIndex");
            DropIndex("Operative.ScheduleService", new[] { "ExtensionId" });
            DropIndex("Operative.ScheduleService", new[] { "AccountId" });
            DropIndex("Catalog.Employee", new[] { "CityId" });
            DropIndex("Catalog.Employee", "Unq_Email");
            DropIndex("Catalog.Employee", "Unq_Phone");
            DropIndex("Catalog.Employee", "Unq_FTR");
            DropIndex("Catalog.Employee", "Unq_Code");
            DropIndex("Catalog.Employee", new[] { "JobPositionId" });
            DropIndex("Finances.Commission", new[] { "CommissionId" });
            DropIndex("Finances.Sale", new[] { "EmployeeId" });
            DropIndex("Finances.Sale", new[] { "ClientId" });
            DropIndex("Finances.SaleDetail", new[] { "Sale_SaleId" });
            DropIndex("Finances.SaleDetail", new[] { "PolicyId" });
            DropIndex("Finances.SaleDetail", new[] { "ExtensionId" });
            DropIndex("Finances.SaleDetail", new[] { "AccountId" });
            DropIndex("Operative.Extension", new[] { "AccountId" });
            DropIndex("Operative.SchedulePayment", new[] { "ExtensionId" });
            DropIndex("Operative.SchedulePayment", new[] { "PolicyId" });
            DropIndex("Operative.SchedulePayment", new[] { "AccountId" });
            DropIndex("Operative.Policy", new[] { "PolicyId" });
            DropIndex("Config.PolicyType", new[] { "AccountType_AccountTypeId" });
            DropIndex("Operative.Account", new[] { "CityId" });
            DropIndex("Operative.Account", new[] { "ClientId" });
            DropIndex("Operative.Account", new[] { "AccountTypeId" });
            DropIndex("Catalog.Client", new[] { "CityId" });
            DropIndex("Catalog.Client", "Unq_Email");
            DropIndex("Catalog.Client", "Unq_Phone");
            DropIndex("Catalog.Client", "Unq_FTR");
            DropIndex("Catalog.Client", "Unq_Code");
            DropIndex("Catalog.Client", "Unq_BusinessName");
            DropIndex("Config.State", "IDX_Name");
            DropIndex("Config.City", "IDX_StateId");
            DropTable("Security.AspNetUserLogins");
            DropTable("Security.AspNetUserClaims");
            DropTable("Security.AspNetUsers");
            DropTable("Security.AspNetUserRoles");
            DropTable("Security.AspNetRoles");
            DropTable("Operative.ScheduleService");
            DropTable("Config.JobPosition");
            DropTable("Catalog.Employee");
            DropTable("Finances.Commission");
            DropTable("Finances.Sale");
            DropTable("Finances.SaleDetail");
            DropTable("Operative.Extension");
            DropTable("Operative.SchedulePayment");
            DropTable("Operative.Policy");
            DropTable("Config.PolicyType");
            DropTable("Config.AccountType");
            DropTable("Operative.Account");
            DropTable("Catalog.Client");
            DropTable("Config.State");
            DropTable("Config.City");
        }
    }
}
