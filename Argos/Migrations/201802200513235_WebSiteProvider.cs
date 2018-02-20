namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebSiteProvider : DbMigration
    {
        public override void Up()
        {
            AddColumn("Security.Providers", "WebSite", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("Security.Providers", "WebSite");
        }
    }
}
