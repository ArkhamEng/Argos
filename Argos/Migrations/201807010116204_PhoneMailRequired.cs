namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhoneMailRequired : DbMigration
    {
        public override void Up()
        {
            DropIndex("Business.EmailAddress", "Unq_Email");
            DropIndex("Business.PhoneNumber", "Unq_Phone");
            AlterColumn("Business.EmailAddress", "Email", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("Business.PhoneNumber", "Phone", c => c.String(nullable: false, maxLength: 15));
            CreateIndex("Business.EmailAddress", new[] { "EntityId", "Email" }, unique: true, name: "Unq_Email");
            CreateIndex("Business.PhoneNumber", new[] { "EntityId", "Phone" }, unique: true, name: "Unq_Phone");
        }
        
        public override void Down()
        {
            DropIndex("Business.PhoneNumber", "Unq_Phone");
            DropIndex("Business.EmailAddress", "Unq_Email");
            AlterColumn("Business.PhoneNumber", "Phone", c => c.String(maxLength: 15));
            AlterColumn("Business.EmailAddress", "Email", c => c.String(maxLength: 150));
            CreateIndex("Business.PhoneNumber", new[] { "EntityId", "Phone" }, unique: true, name: "Unq_Phone");
            CreateIndex("Business.EmailAddress", new[] { "EntityId", "Email" }, unique: true, name: "Unq_Email");
        }
    }
}
