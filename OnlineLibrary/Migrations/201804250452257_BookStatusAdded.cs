namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookStatusAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Books", "IsCurrentlyRented");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Books", "IsCurrentlyRented", c => c.Boolean(nullable: false));
            DropColumn("dbo.Books", "Status");
        }
    }
}
