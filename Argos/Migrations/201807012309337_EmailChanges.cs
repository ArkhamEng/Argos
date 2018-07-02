namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailChanges : DbMigration
    {
        public override void Up()
        {
            DropIndex("Business.EmailAddress", "Unq_Email");
            DropPrimaryKey("Business.EmailAddress");
            AddPrimaryKey("Business.EmailAddress", new[] { "EntityId", "Email" });
            CreateIndex("Business.EmailAddress", "EntityId");
            DropColumn("Business.EmailAddress", "EmailAddressId");
        }
        
        public override void Down()
        {
            AddColumn("Business.EmailAddress", "EmailAddressId", c => c.Int(nullable: false, identity: true));
            DropIndex("Business.EmailAddress", new[] { "EntityId" });
            DropPrimaryKey("Business.EmailAddress");
            AddPrimaryKey("Business.EmailAddress", "EmailAddressId");
            CreateIndex("Business.EmailAddress", new[] { "EntityId", "Email" }, unique: true, name: "Unq_Email");
        }
    }
}
