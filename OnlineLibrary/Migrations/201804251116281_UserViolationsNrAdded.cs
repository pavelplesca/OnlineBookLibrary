namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserViolationsNrAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ViolationsNr", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ViolationsNr");
        }
    }
}
