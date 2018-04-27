namespace OnlineLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomethingStrangeHappened : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TestUsers", "ViolationsNr");
            DropColumn("dbo.TestUsers", "IsBanned");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestUsers", "IsBanned", c => c.Boolean(nullable: false));
            AddColumn("dbo.TestUsers", "ViolationsNr", c => c.Int(nullable: false));
        }
    }
}
