namespace MobileApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MobileApi.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MobileApiContext>
    {
        public Configuration()
        {
          /*  AutomaticMigrationsEnabled = false;*/
        }

        protected override void Seed(MobileApiContext context)
        {
        /*    // Seed the database
            context.Pumas.AddOrUpdate(
              p => p.Id,
              new Puma { Id = 1, Name = "Cougar", Description = "Cougar Description" },
              new Puma { Id = 2, Name = "Florida Panther", Description = "Florida Panther Description" }
            );

            context.PumaImages.AddOrUpdate(
              p => p.Id,
              new PumaImage { Id = 1, PumaId = 1, ImageFilename = "cougar1.jpg" },
              new PumaImage { Id = 2, PumaId = 1, ImageFilename = "cougar2.jpg" }
            );*/

        }
    }
}
