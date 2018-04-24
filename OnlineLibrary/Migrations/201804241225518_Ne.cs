namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ne : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Loans", new[] { "UserID" });
            CreateTable(
                "dbo.TestUsers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.Loans", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Loans", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Loans", new[] { "UserID" });
            AlterColumn("dbo.Loans", "UserID", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.TestUsers");
            CreateIndex("dbo.Loans", "UserID");
        }
    }
}
