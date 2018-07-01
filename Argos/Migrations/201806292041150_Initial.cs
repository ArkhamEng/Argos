namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                        LockEndDate = c.DateTime(),
                        LockUser = c.String(maxLength: 30),
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
                "Business.EmailAddress",
                c => new
                    {
                        EmailAddressId = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        Email = c.String(maxLength: 150),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.EmailAddressId)
                .ForeignKey("Business.Entity", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId)
                .Index(t => t.Email, unique: true, name: "Unq_Email");
            
            CreateTable(
                "Business.PhoneNumber",
                c => new
                    {
                        PhoneNumberId = c.Int(nullable: false, identity: true),
                        PhoneTypeId = c.Int(nullable: false),
                        EntityId = c.Int(nullable: false),
                        Phone = c.String(maxLength: 15),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.PhoneNumberId)
                .ForeignKey("Business.Entity", t => t.EntityId, cascadeDelete: true)
                .ForeignKey("Business.PhoneType", t => t.PhoneTypeId, cascadeDelete: true)
                .Index(t => t.PhoneTypeId)
                .Index(t => t.EntityId)
                .Index(t => t.Phone, unique: true, name: "Unq_Phone");
            
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
                        Quantity = c.Double(nullable: false),
                        Direction = c.Int(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false, maxLength: 30),
                        UpdDate = c.DateTime(nullable: false),
                        UpdUser = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.FlowId)
                .ForeignKey("Operative.Detail", t => t.DetailId, cascadeDelete: true)
                .ForeignKey("Inventory.Item", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.DetailId);
            
            CreateTable(
                "Operative.Detail",
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
                .ForeignKey("Operative.Supplier", t => t.SupplierId)
                .Index(t => t.SupplierId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "Transaction.Purchase",
                c => new
                    {
                        PurchaseId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        Bill = c.String(nullable: false, maxLength: 10),
                        BranchId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        ShipDate = c.DateTime(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        TaxAmt = c.Double(nullable: false),
                        Freight = c.Double(nullable: false),
                        TotalDue = c.Double(nullable: false),
                        StatusId = c.Int(nullable: false),
                        ShipMethod_ShipMethodId = c.Int(),
                    })
                .PrimaryKey(t => t.PurchaseId)
                .ForeignKey("Config.Branch", t => t.BranchId)
                .ForeignKey("Transaction.ShipMethod", t => t.ShipMethod_ShipMethodId)
                .ForeignKey("Transaction.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("Operative.Supplier", t => t.SupplierId)
                .Index(t => new { t.SupplierId, t.Bill }, unique: true, name: "IDX_Supplier_Bill")
                .Index(t => t.BranchId)
                .Index(t => t.StatusId)
                .Index(t => t.ShipMethod_ShipMethodId);
            
            CreateTable(
                "Transaction.ShipMethod",
                c => new
                    {
                        ShipMethodId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ShipMethodId);
            
            CreateTable(
                "Transaction.Sale",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        Folio = c.String(maxLength: 12),
                        IsOnline = c.Boolean(nullable: false),
                        BranchId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        ShipDate = c.DateTime(nullable: false),
                        SubTotal = c.Double(nullable: false),
                        TaxAmt = c.Double(nullable: false),
                        Freight = c.Double(nullable: false),
                        TotalDue = c.Double(nullable: false),
                        StatusId = c.Int(nullable: false),
                        ShipMethod_ShipMethodId = c.Int(),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("Config.Branch", t => t.BranchId)
                .ForeignKey("Operative.Client", t => t.ClientId)
                .ForeignKey("HumanResources.Employee", t => t.EmployeeId)
                .ForeignKey("Transaction.ShipMethod", t => t.ShipMethod_ShipMethodId)
                .ForeignKey("Transaction.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.EmployeeId)
                .Index(t => t.BranchId)
                .Index(t => t.StatusId)
                .Index(t => t.ShipMethod_ShipMethodId);
            
            CreateTable(
                "Transaction.Status",
                c => new
                    {
                        StatusId = c.Int(nullable: false),
                        Name = c.String(maxLength: 20),
                        Description = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.StatusId);
            
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
                .Index(t => t.EntityId)
                .Index(t => t.Name, name: "IDX_Name")
                .Index(t => t.FTR, unique: true, name: "Unq_FTR");
            
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
                "Operative.Supplier",
                c => new
                    {
                        EntityId = c.Int(nullable: false),
                        BusinessName = c.String(maxLength: 200),
                        WebSite = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.EntityId)
                .ForeignKey("Business.Person", t => t.EntityId)
                .Index(t => t.EntityId)
                .Index(t => t.BusinessName, unique: true, name: "Unq_BusinessName");
            
            CreateTable(
                "Transaction.PurchaseDetail",
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
                .ForeignKey("Operative.Detail", t => t.DetailId)
                .ForeignKey("Transaction.Purchase", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.DetailId)
                .Index(t => t.PurchaseId);
            
            CreateTable(
                "Operative.Client",
                c => new
                    {
                        EntityId = c.Int(nullable: false),
                        BusinessName = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.EntityId)
                .ForeignKey("Business.Person", t => t.EntityId)
                .Index(t => t.EntityId)
                .Index(t => t.BusinessName, unique: true, name: "Unq_BusinessName");
            
            CreateTable(
                "Transaction.SaleDetail",
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
                .ForeignKey("Operative.Detail", t => t.DetailId)
                .ForeignKey("Transaction.Sale", t => t.SaleId, cascadeDelete: true)
                .Index(t => t.DetailId)
                .Index(t => t.SaleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Transaction.SaleDetail", "SaleId", "Transaction.Sale");
            DropForeignKey("Transaction.SaleDetail", "DetailId", "Operative.Detail");
            DropForeignKey("Operative.Client", "EntityId", "Business.Person");
            DropForeignKey("Transaction.PurchaseDetail", "PurchaseId", "Transaction.Purchase");
            DropForeignKey("Transaction.PurchaseDetail", "DetailId", "Operative.Detail");
            DropForeignKey("Operative.Supplier", "EntityId", "Business.Person");
            DropForeignKey("Config.Branch", "EntityId", "Business.Entity");
            DropForeignKey("HumanResources.Employee", "JobPositionId", "HumanResources.JobPosition");
            DropForeignKey("HumanResources.Employee", "EntityId", "Business.Person");
            DropForeignKey("Business.Person", "EntityId", "Business.Entity");
            DropForeignKey("Security.AspNetUserRoles", "RoleId", "Security.AspNetRoles");
            DropForeignKey("Config.Town", "StateId", "Config.State");
            DropForeignKey("Config.Settlement", "TownId", "Config.Town");
            DropForeignKey("Business.Address", "TownId", "Config.Town");
            DropForeignKey("HumanResources.EmployeeBranch", "EmployeeId", "HumanResources.Employee");
            DropForeignKey("HumanResources.EmployeeBranch", "BranchId", "Config.Branch");
            DropForeignKey("Inventory.ItemHistory", "Storage_StorageId", "Inventory.Storage");
            DropForeignKey("Inventory.ItemStorage", "StorageId", "Inventory.Storage");
            DropForeignKey("Inventory.ItemStorage", "ItemId", "Inventory.Item");
            DropForeignKey("Inventory.ItemHistory", "ItemId", "Inventory.Item");
            DropForeignKey("Inventory.Flow", "ItemId", "Inventory.Item");
            DropForeignKey("Inventory.SupplierProduct", "SupplierId", "Operative.Supplier");
            DropForeignKey("Transaction.Purchase", "SupplierId", "Operative.Supplier");
            DropForeignKey("Transaction.Sale", "StatusId", "Transaction.Status");
            DropForeignKey("Transaction.Purchase", "StatusId", "Transaction.Status");
            DropForeignKey("Transaction.Sale", "ShipMethod_ShipMethodId", "Transaction.ShipMethod");
            DropForeignKey("Transaction.Sale", "EmployeeId", "HumanResources.Employee");
            DropForeignKey("Transaction.Sale", "ClientId", "Operative.Client");
            DropForeignKey("Transaction.Sale", "BranchId", "Config.Branch");
            DropForeignKey("Transaction.Purchase", "ShipMethod_ShipMethodId", "Transaction.ShipMethod");
            DropForeignKey("Transaction.Purchase", "BranchId", "Config.Branch");
            DropForeignKey("Inventory.SupplierProduct", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Product", "SubCategoryId", "Inventory.SubCategory");
            DropForeignKey("Inventory.SubCategory", "CategoryId", "Inventory.Category");
            DropForeignKey("Inventory.ProductImage", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.PriceChange", "ProductId", "Inventory.Product");
            DropForeignKey("Inventory.Product", "MeasureUnitId", "Inventory.MeasureUnit");
            DropForeignKey("Inventory.Item", "ProductId", "Inventory.Product");
            DropForeignKey("Operative.Detail", "ProductId", "Inventory.Product");
            DropForeignKey("Config.Compatibility", "ProductId", "Inventory.Product");
            DropForeignKey("Config.Model", "MakerId", "Config.Maker");
            DropForeignKey("Config.Compatibility", "ModelId", "Config.Model");
            DropForeignKey("Inventory.Flow", "DetailId", "Operative.Detail");
            DropForeignKey("Inventory.Storage", "BranchId", "Config.Branch");
            DropForeignKey("Security.SystemUser", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserRoles", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserLogins", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.AspNetUserClaims", "UserId", "Security.AspNetUsers");
            DropForeignKey("Security.SystemUser", "SystemUserId", "Business.Person");
            DropForeignKey("Business.PhoneNumber", "PhoneTypeId", "Business.PhoneType");
            DropForeignKey("Business.PhoneNumber", "EntityId", "Business.Entity");
            DropForeignKey("Business.EmailAddress", "EntityId", "Business.Entity");
            DropForeignKey("Business.Address", "EntityId", "Business.Entity");
            DropForeignKey("Business.Address", "AddressTypeId", "Business.AddressType");
            DropIndex("Transaction.SaleDetail", new[] { "SaleId" });
            DropIndex("Transaction.SaleDetail", new[] { "DetailId" });
            DropIndex("Operative.Client", "Unq_BusinessName");
            DropIndex("Operative.Client", new[] { "EntityId" });
            DropIndex("Transaction.PurchaseDetail", new[] { "PurchaseId" });
            DropIndex("Transaction.PurchaseDetail", new[] { "DetailId" });
            DropIndex("Operative.Supplier", "Unq_BusinessName");
            DropIndex("Operative.Supplier", new[] { "EntityId" });
            DropIndex("Config.Branch", new[] { "EntityId" });
            DropIndex("HumanResources.Employee", new[] { "JobPositionId" });
            DropIndex("HumanResources.Employee", new[] { "EntityId" });
            DropIndex("Business.Person", "Unq_FTR");
            DropIndex("Business.Person", "IDX_Name");
            DropIndex("Business.Person", new[] { "EntityId" });
            DropIndex("Security.AspNetRoles", "RoleNameIndex");
            DropIndex("Config.Settlement", "IDX_Code_Name");
            DropIndex("Config.Settlement", new[] { "TownId" });
            DropIndex("Config.Town", new[] { "StateId" });
            DropIndex("Inventory.ItemStorage", new[] { "StorageId" });
            DropIndex("Inventory.ItemStorage", new[] { "ItemId" });
            DropIndex("Transaction.Sale", new[] { "ShipMethod_ShipMethodId" });
            DropIndex("Transaction.Sale", new[] { "StatusId" });
            DropIndex("Transaction.Sale", new[] { "BranchId" });
            DropIndex("Transaction.Sale", new[] { "EmployeeId" });
            DropIndex("Transaction.Sale", new[] { "ClientId" });
            DropIndex("Transaction.Purchase", new[] { "ShipMethod_ShipMethodId" });
            DropIndex("Transaction.Purchase", new[] { "StatusId" });
            DropIndex("Transaction.Purchase", new[] { "BranchId" });
            DropIndex("Transaction.Purchase", "IDX_Supplier_Bill");
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
            DropIndex("Operative.Detail", new[] { "ProductId" });
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
            DropIndex("Business.PhoneNumber", "Unq_Phone");
            DropIndex("Business.PhoneNumber", new[] { "EntityId" });
            DropIndex("Business.PhoneNumber", new[] { "PhoneTypeId" });
            DropIndex("Business.EmailAddress", "Unq_Email");
            DropIndex("Business.EmailAddress", new[] { "EntityId" });
            DropIndex("Business.Address", new[] { "TownId" });
            DropIndex("Business.Address", new[] { "AddressTypeId" });
            DropIndex("Business.Address", new[] { "EntityId" });
            DropTable("Transaction.SaleDetail");
            DropTable("Operative.Client");
            DropTable("Transaction.PurchaseDetail");
            DropTable("Operative.Supplier");
            DropTable("Config.Branch");
            DropTable("HumanResources.Employee");
            DropTable("Business.Person");
            DropTable("Security.AspNetRoles");
            DropTable("Config.Configuration");
            DropTable("Config.State");
            DropTable("Config.Settlement");
            DropTable("Config.Town");
            DropTable("HumanResources.JobPosition");
            DropTable("Inventory.ItemStorage");
            DropTable("Transaction.Status");
            DropTable("Transaction.Sale");
            DropTable("Transaction.ShipMethod");
            DropTable("Transaction.Purchase");
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
            DropTable("Operative.Detail");
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
            DropTable("Business.EmailAddress");
            DropTable("Business.Entity");
            DropTable("Business.AddressType");
            DropTable("Business.Address");
        }
    }
}
