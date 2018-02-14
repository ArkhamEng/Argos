namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serviceCodeLenght : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Operative.Service", "Code", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("Operative.Service", "Code", c => c.String(maxLength: 12));
        }
    }
}
