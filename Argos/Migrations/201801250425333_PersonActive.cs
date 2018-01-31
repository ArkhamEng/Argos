namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("Catalog.Client", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("Catalog.Employee", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Catalog.Employee", "IsActive");
            DropColumn("Catalog.Client", "IsActive");
        }
    }
}
