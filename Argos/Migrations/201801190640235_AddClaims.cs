namespace Argos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClaims : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Operative.Claim",
                c => new
                    {
                        ClaimId = c.Int(nullable: false, identity: true),
                        Folio = c.String(maxLength: 10),
                        ClaimDate = c.DateTime(nullable: false),
                        InsDate = c.DateTime(nullable: false),
                        UpdDate = c.DateTime(nullable: false),
                        InsUser = c.String(nullable: false),
                        UpdUser = c.String(nullable: false),
                        Account_AccountId = c.Int(),
                    })
                .PrimaryKey(t => t.ClaimId)
                .ForeignKey("Operative.Account", t => t.Account_AccountId)
                .Index(t => t.Account_AccountId);
            
            AddColumn("Operative.ScheduleService", "ClaimId", c => c.Int());
            CreateIndex("Operative.ScheduleService", "ClaimId");
            AddForeignKey("Operative.ScheduleService", "ClaimId", "Operative.Claim", "ClaimId");
        }
        
        public override void Down()
        {
            DropForeignKey("Operative.ScheduleService", "ClaimId", "Operative.Claim");
            DropForeignKey("Operative.Claim", "Account_AccountId", "Operative.Account");
            DropIndex("Operative.ScheduleService", new[] { "ClaimId" });
            DropIndex("Operative.Claim", new[] { "Account_AccountId" });
            DropColumn("Operative.ScheduleService", "ClaimId");
            DropTable("Operative.Claim");
        }
    }
}
