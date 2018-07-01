namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntityUniques : DbMigration
    {
        public override void Up()
        {
            DropIndex("Business.EmailAddress", new[] { "EntityId" });
            DropIndex("Business.EmailAddress", "Unq_Email");
            DropIndex("Business.PhoneNumber", new[] { "EntityId" });
            DropIndex("Business.PhoneNumber", "Unq_Phone");
            CreateIndex("Business.EmailAddress", new[] { "EntityId", "Email" }, unique: true, name: "Unq_Email");
            CreateIndex("Business.PhoneNumber", new[] { "EntityId", "Phone" }, unique: true, name: "Unq_Phone");
        }
        
        public override void Down()
        {
            DropIndex("Business.PhoneNumber", "Unq_Phone");
            DropIndex("Business.EmailAddress", "Unq_Email");
            CreateIndex("Business.PhoneNumber", "Phone", unique: true, name: "Unq_Phone");
            CreateIndex("Business.PhoneNumber", "EntityId");
            CreateIndex("Business.EmailAddress", "Email", unique: true, name: "Unq_Email");
            CreateIndex("Business.EmailAddress", "EntityId");
        }
    }
}
