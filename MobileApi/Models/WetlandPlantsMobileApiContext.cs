using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MobileApi.Models
{
    public class WetlandPlantsMobileApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public WetlandPlantsMobileApiContext() : base("name=MobileApiContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<WetlandPlant> Plants { get; set; }
        public DbSet<WetlandSetting> WetlandSettings { get; set; }
        public DbSet<WetlandGlossary> WetlandGlossary { get; set; }
        public DbSet<ImagesWetland> ImagesWetland { get; set; }
        public DbSet<SimilarSpeciesWetland> SimilarSpeciesWetland { get; set; }
        public DbSet<CountyPlantWetland> CountyPlantWetland { get; set; }
        public DbSet<ReferenceWetland> ReferenceWetland { get; set; }
        public DbSet<FruitWetland> FruitWetland { get; set; }
        public DbSet<DivisionWetland> DivisionWetland { get; set; }
        public DbSet<ShapeWetland> ShapeWetland { get; set; }
        public DbSet<ArrangementWetland> ArrangementWetland { get; set; }
        public DbSet<SizeWetland> SizeWetland { get; set; }
        public DbSet<RegionWetland> RegionWetland { get; set; }
    }
}
