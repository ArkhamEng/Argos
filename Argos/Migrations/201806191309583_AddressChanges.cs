namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressChanges : DbMigration
    {
        public override void Up()
        {
            DropColumn("BusinessEntity.Address", "OutNumber");
            DropColumn("BusinessEntity.Address", "InNumber");
            DropColumn("Config.Branch", "OutNumber");
            DropColumn("Config.Branch", "InNumber");
            DropColumn("Production.Location", "OutNumber");
            DropColumn("Production.Location", "InNumber");
        }
        
        public override void Down()
        {
            AddColumn("Production.Location", "InNumber", c => c.String(maxLength: 6));
            AddColumn("Production.Location", "OutNumber", c => c.String(nullable: false, maxLength: 6));
            AddColumn("Config.Branch", "InNumber", c => c.String(maxLength: 6));
            AddColumn("Config.Branch", "OutNumber", c => c.String(nullable: false, maxLength: 6));
            AddColumn("BusinessEntity.Address", "InNumber", c => c.String(maxLength: 6));
            AddColumn("BusinessEntity.Address", "OutNumber", c => c.String(nullable: false, maxLength: 6));
        }
    }
}
