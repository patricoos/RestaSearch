namespace RestaSearch.Migrations
{
	using RestaSearch.DAL;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<RestaSearch.DAL.RSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RestaSearch.DAL.RSContext";
        }

        protected override void Seed(RestaSearch.DAL.RSContext context)
        {
			RSInitializer.SeedRestaSearchData(context);
			RSInitializer.SeedUzytkownicy(context);
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
		}
    }
}
