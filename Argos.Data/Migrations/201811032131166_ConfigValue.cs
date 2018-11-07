namespace Argos.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConfigValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("Config.Configuration", "Value", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("Config.Configuration", "Value");
        }
    }
}
