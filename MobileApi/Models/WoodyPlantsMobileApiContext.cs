using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MobileApi.Models
{
    public class WoodyPlantsMobileApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public WoodyPlantsMobileApiContext() : base("name=MobileApiContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<WoodyPlant> Plants { get; set; }
        public DbSet<ImagesWetland> Images { get; set; }
        public DbSet<SimilarSpeciesWetland> SimalarSpecies { get; set; }
        public DbSet<CountyPlantWetland> CountyPlants { get; set; }
        public DbSet<ReferenceWetland> References { get; set; }
        public DbSet<FruitWetland> Fruits { get; set; }
        public DbSet<DivisionWetland> Divisions { get; set; }
        public DbSet<ShapeWetland> Shapes { get; set; }
        public DbSet<ArrangementWetland> Arrangements { get; set; }
        public DbSet<SizeWetland> Sizes { get; set; }
        public DbSet<RegionWetland> Regions { get; set; }

    }
}
