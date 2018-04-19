namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoansTestUsersAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DueDate = c.DateTime(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                        ReturnedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                        TestUser_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.TestUsers", t => t.TestUser_ID)
                .Index(t => t.BookID)
                .Index(t => t.TestUser_ID);
            
            CreateTable(
                "dbo.TestUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "TestUser_ID", "dbo.TestUsers");
            DropForeignKey("dbo.Loans", "BookID", "dbo.Books");
            DropIndex("dbo.Loans", new[] { "TestUser_ID" });
            DropIndex("dbo.Loans", new[] { "BookID" });
            DropTable("dbo.TestUsers");
            DropTable("dbo.Loans");
        }
    }
}
