namespace Projekt.Migrations
{
    using Projekt.DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Projekt.DAL.SklepContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Projekt.DAL.SklepContext";
        }

        protected override void Seed(Projekt.DAL.SklepContext context)
        {
            SklepInitializer.SeedSklepData(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
