namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gender : DbMigration
    {
        public override void Up()
        {
            AddColumn("Catalog.Employee", "Gender", c => c.String(maxLength: 1));
            AlterColumn("Operative.AccountAddress", "OutNumber", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("Operative.AccountAddress", "InNumber", c => c.String(maxLength: 6));
            AlterColumn("Config.Branch", "OutNumber", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("Config.Branch", "InNumber", c => c.String(maxLength: 6));
            AlterColumn("Catalog.Client", "OutNumber", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("Catalog.Client", "InNumber", c => c.String(maxLength: 6));
            AlterColumn("Catalog.Employee", "OutNumber", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("Catalog.Employee", "InNumber", c => c.String(maxLength: 6));
            AlterColumn("Security.Providers", "OutNumber", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("Security.Providers", "InNumber", c => c.String(maxLength: 6));
        }
        
        public override void Down()
        {
            AlterColumn("Security.Providers", "InNumber", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("Security.Providers", "OutNumber", c => c.String(maxLength: 6));
            AlterColumn("Catalog.Employee", "InNumber", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("Catalog.Employee", "OutNumber", c => c.String(maxLength: 6));
            AlterColumn("Catalog.Client", "InNumber", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("Catalog.Client", "OutNumber", c => c.String(maxLength: 6));
            AlterColumn("Config.Branch", "InNumber", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("Config.Branch", "OutNumber", c => c.String(maxLength: 6));
            AlterColumn("Operative.AccountAddress", "InNumber", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("Operative.AccountAddress", "OutNumber", c => c.String(maxLength: 6));
            DropColumn("Catalog.Employee", "Gender");
        }
    }
}
