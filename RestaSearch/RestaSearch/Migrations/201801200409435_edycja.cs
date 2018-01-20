namespace RestaSearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edycja : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Kategoria", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kategoria", "MyProperty", c => c.String());
        }
    }
}
