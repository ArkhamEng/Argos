namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceLocatableField : DbMigration
    {
        public override void Up()
        {
            AddColumn("Catalog.ServiceType", "IsLocatable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Catalog.ServiceType", "IsLocatable");
        }
    }
}
