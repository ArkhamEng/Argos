namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSchemaProvider : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Security.Providers", newName: "Provider");
            MoveTable(name: "Security.Provider", newSchema: "Catalog");
        }
        
        public override void Down()
        {
            MoveTable(name: "Catalog.Provider", newSchema: "Security");
            RenameTable(name: "Security.Provider", newName: "Providers");
        }
    }
}
