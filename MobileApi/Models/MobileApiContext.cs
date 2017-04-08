using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MobileApi.Models
{
    public class MobileApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MobileApiContext() : base("name=MobileApiContext")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Plant> Plants { get; set; }
        public DbSet<OtherCommonName> OtherCommonNames { get; set; }
        public DbSet<ScientificName> ScientificNames { get; set; }
        public DbSet<Identification> Identifications { get; set; }
        public DbSet<Ecology> Ecologies { get; set; }
        public DbSet<Landscaping> Landscapings { get; set; }
        public DbSet<HumanConnection> HumanConnections { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
