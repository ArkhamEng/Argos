namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndexSettlement : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Config.Settlement", "Name", c => c.String(maxLength: 100));
            CreateIndex("Config.Settlement", new[] { "Code", "Name" }, name: "IDX_Code_Name");
        }
        
        public override void Down()
        {
            DropIndex("Config.Settlement", "IDX_Code_Name");
            AlterColumn("Config.Settlement", "Name", c => c.String());
        }
    }
}
