namespace RestaSearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Link : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lokal", "Link", c => c.String());
            AddColumn("dbo.Lokal", "FbLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lokal", "FbLink");
            DropColumn("dbo.Lokal", "Link");
        }
    }
}
