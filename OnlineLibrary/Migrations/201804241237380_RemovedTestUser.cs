namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedTestUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Loans", "UserID", "dbo.TestUsers");
            DropIndex("dbo.Loans", new[] { "UserID" });
            DropColumn("dbo.Loans", "UserID");
            DropTable("dbo.TestUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TestUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Loans", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Loans", "UserID");
            AddForeignKey("dbo.Loans", "UserID", "dbo.TestUsers", "ID", cascadeDelete: true);
        }
    }
}
