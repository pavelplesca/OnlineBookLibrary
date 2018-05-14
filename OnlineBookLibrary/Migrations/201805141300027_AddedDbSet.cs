namespace OnlineBookLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDbSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        Description = c.String(),
                        Image = c.String(),
                        Year = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DueDate = c.DateTime(nullable: false),
                        BorrowDate = c.DateTime(nullable: false),
                        ReturnedDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                        UserID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID, cascadeDelete: true)
                .Index(t => t.BookID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.TagBooks",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Book_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Book_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Loans", "BookID", "dbo.Books");
            DropForeignKey("dbo.TagBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.TagBooks", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagBooks", new[] { "Book_Id" });
            DropIndex("dbo.TagBooks", new[] { "Tag_Id" });
            DropIndex("dbo.Loans", new[] { "UserID" });
            DropIndex("dbo.Loans", new[] { "BookID" });
            DropTable("dbo.TagBooks");
            DropTable("dbo.Loans");
            DropTable("dbo.Tags");
            DropTable("dbo.Books");
        }
    }
}
