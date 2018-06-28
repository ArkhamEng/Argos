namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("BusinessEntity.Person", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("BusinessEntity.Person", "ImagePath");
        }
    }
}
