namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFildsForBookUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "IsCurrentlyRented", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsBanned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsBanned");
            DropColumn("dbo.Books", "IsCurrentlyRented");
        }
    }
}
