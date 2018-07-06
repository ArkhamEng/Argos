namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PurchaseStatus : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "Purchasing.Supplier", name: "CreditStatusId", newName: "Status");
            RenameIndex(table: "Purchasing.Supplier", name: "IX_CreditStatusId", newName: "IX_Status");
        }
        
        public override void Down()
        {
            RenameIndex(table: "Purchasing.Supplier", name: "IX_Status", newName: "IX_CreditStatusId");
            RenameColumn(table: "Purchasing.Supplier", name: "Status", newName: "CreditStatusId");
        }
    }
}
