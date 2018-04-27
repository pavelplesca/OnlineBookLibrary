namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserPropToLoan : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Loans", "TestUser_ID", "dbo.TestUsers");
            DropIndex("dbo.Loans", new[] { "TestUser_ID" });
            RenameColumn(table: "dbo.Loans", name: "TestUser_ID", newName: "UserID");
            AlterColumn("dbo.Loans", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Loans", "UserID");
            AddForeignKey("dbo.Loans", "UserID", "dbo.TestUsers", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "UserID", "dbo.TestUsers");
            DropIndex("dbo.Loans", new[] { "UserID" });
            AlterColumn("dbo.Loans", "UserID", c => c.Int());
            RenameColumn(table: "dbo.Loans", name: "UserID", newName: "TestUser_ID");
            CreateIndex("dbo.Loans", "TestUser_ID");
            AddForeignKey("dbo.Loans", "TestUser_ID", "dbo.TestUsers", "ID");
        }
    }
}
