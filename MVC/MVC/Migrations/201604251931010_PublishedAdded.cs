namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublishedAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Publisher", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Publisher");
        }
    }
}
