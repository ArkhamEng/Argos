namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class personRemoveLocation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("BusinessEntity.Person", "TownId", "Config.Town");
            DropIndex("BusinessEntity.Person", new[] { "TownId" });
            DropColumn("BusinessEntity.Person", "TownId");
            DropColumn("BusinessEntity.Person", "Street");
            DropColumn("BusinessEntity.Person", "OutNumber");
            DropColumn("BusinessEntity.Person", "InNumber");
            DropColumn("BusinessEntity.Person", "Location");
            DropColumn("BusinessEntity.Person", "ZipCode");
        }
        
        public override void Down()
        {
            AddColumn("BusinessEntity.Person", "ZipCode", c => c.String(maxLength: 10));
            AddColumn("BusinessEntity.Person", "Location", c => c.String(nullable: false, maxLength: 50));
            AddColumn("BusinessEntity.Person", "InNumber", c => c.String(maxLength: 6));
            AddColumn("BusinessEntity.Person", "OutNumber", c => c.String(nullable: false, maxLength: 6));
            AddColumn("BusinessEntity.Person", "Street", c => c.String(nullable: false, maxLength: 80));
            AddColumn("BusinessEntity.Person", "TownId", c => c.String(nullable: false, maxLength: 6));
            CreateIndex("BusinessEntity.Person", "TownId");
            AddForeignKey("BusinessEntity.Person", "TownId", "Config.Town", "TownId", cascadeDelete: true);
        }
    }
}
