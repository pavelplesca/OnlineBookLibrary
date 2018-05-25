namespace OnlineBookLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChangedPassField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ChangedPassword", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ChangedPassword");
        }
    }
}
