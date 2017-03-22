namespace MobileApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MobileApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MobileApi.Models.MobileApiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MobileApi.Models.MobileApiContext context)
        {
            // Seed the database
            context.PumaTypes.AddOrUpdate(
              p => p.Id,
              new PumaType { Id = 1, Type = "Cougar", Description = "Cougar Description" },
              new PumaType { Id = 2, Type = "Florida Panther", Description = "Florida Panther Description" }
            );

            context.PumaTypeImages.AddOrUpdate(
              p => p.Id,
              new PumaTypeImage { Id = 1, PumaTypeId = 1, ImageFilename = "cougar1.jpg" },
              new PumaTypeImage { Id = 2, PumaTypeId = 1, ImageFilename = "cougar2.jpg" }
            );

        }
    }
}
