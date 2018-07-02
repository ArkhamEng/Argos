namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhoneChanges : DbMigration
    {
        public override void Up()
        {
            DropIndex("Business.PhoneNumber", "Unq_Phone");
            DropPrimaryKey("Business.PhoneNumber");
            AddPrimaryKey("Business.PhoneNumber", new[] { "EntityId", "Phone" });
            CreateIndex("Business.PhoneNumber", "EntityId");
            DropColumn("Business.PhoneNumber", "PhoneNumberId");
        }
        
        public override void Down()
        {
            AddColumn("Business.PhoneNumber", "PhoneNumberId", c => c.Int(nullable: false, identity: true));
            DropIndex("Business.PhoneNumber", new[] { "EntityId" });
            DropPrimaryKey("Business.PhoneNumber");
            AddPrimaryKey("Business.PhoneNumber", "PhoneNumberId");
            CreateIndex("Business.PhoneNumber", new[] { "EntityId", "Phone" }, unique: true, name: "Unq_Phone");
        }
    }
}
