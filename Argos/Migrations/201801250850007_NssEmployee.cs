namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NssEmployee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Catalog.Employee", "SSN", c => c.String(maxLength: 11));
        }
        
        public override void Down()
        {
            AlterColumn("Catalog.Employee", "SSN", c => c.Int(nullable: false));
        }
    }
}
