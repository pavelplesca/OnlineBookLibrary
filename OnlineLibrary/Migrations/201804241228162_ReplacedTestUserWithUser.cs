namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReplacedTestUserWithUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Loans", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Loans", new[] { "UserID" });
            AddColumn("dbo.Loans", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Loans", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Loans", "User_Id");
            AddForeignKey("dbo.Loans", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Loans", new[] { "User_Id" });
            AlterColumn("dbo.Loans", "UserID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Loans", "User_Id");
            CreateIndex("dbo.Loans", "UserID");
            AddForeignKey("dbo.Loans", "UserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
