namespace OnlineBookLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagNameUniqueMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "Name", c => c.String(nullable: false, maxLength: 450));
            CreateIndex("dbo.Tags", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tags", new[] { "Name" });
            AlterColumn("dbo.Tags", "Name", c => c.String(nullable: false));
        }
    }
}
