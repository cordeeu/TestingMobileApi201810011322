using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobileApi.Models
{
    [Table("plant", Schema = "plant")]
    public class Plant
    {
        [Key]
        [Column("plant_id")]
        public int plant_id { get; set; }

        public int plant_imported_id { get; set; }
     
        public string common_name { get; set; }

         public string common_family_name { get; set; }

         public string scientific_family_name { get; set; }

           public string family_name_meaning { get; set; }

           public string family_characteristics { get; set; }
   
        public string classification { get; set; }

        public string sub_class { get; set; }

    }

    [Table("other_common_names", Schema = "plant")]
    public class OtherCommonName
    {
        [Key]
        [Column("other_common_name_id")]
        public int other_common_name_id { get; set; }

        public int plant_id { get; set; }

        public string other_common_name { get; set; }

        public virtual Plant plant { get; set; }
    }

    [Table("scientific_name", Schema = "plant")]
    public class ScientificName
    {
        [Key]
        [Column("scientific_name_id")]
        public int scientific_name_id { get; set; }

        public int plant_id { get; set; }

        public string subspecies { get; set; }

        public string variety { get; set; }

        public string authors { get; set; }

        public string scientific_name_meaning { get; set; }

        public virtual Plant plant { get; set; }
    }


    [Table("synonyms", Schema = "plant")]
    public class Synonym
    {
        [Key]
        [Column("synonym_id")]
        public int synonym_id { get; set; }

        public int plant_id { get; set; }

        public string synonym { get; set; }

        public virtual Plant plant { get; set; }
    }


    [Table("identifications", Schema = "plant")]
    public class Identification
    {
        [Key]
        [Column("identification_id")]
        public int identification_id { get; set; }

        public int plant_id { get; set; }

        public string key_characteristics { get; set; }

        public string mature_height { get; set; }

        public string mature_spread { get; set; }

        public string flower_cluster { get; set; }

        public string flower_color { get; set; }

        public string flower_shape { get; set; }

        public string flower_size { get; set; }

        public string flower_structure { get; set; }

        public string flower_symmetry { get; set; }

        public string fruit_color { get; set; }

        public string fruit_type { get; set; }

        public string leaf_type { get; set; }

        public string leaf_shape { get; set; }

        public virtual Plant plant { get; set; }
    }


    [Table("ecologies", Schema = "plant")]
    public class Ecology
    {
        [Key]
        [Column("ecology_id")]
        public int ecology_id { get; set; }

        public int plant_id { get; set; }

        public string origin { get; set; }

        public string conservation_status { get; set; }

        public string life_zone { get; set; }

        public string ecosystem_type { get; set; }

        public string habitat { get; set; }

        public string indicator_status { get; set; }

        public string endemic_location { get; set; }

        public string growth_from { get; set; }

        public string life_cycle { get; set; }

        public string monoecious { get; set; }

        public string fruit_type { get; set; }

        public string leaf_type { get; set; }

        public string leaf_shape { get; set; }

        public virtual Plant plant { get; set; }
    }


    [Table("landscapings", Schema = "plant")]
    public class Landscaping
    {
        [Key]
        [Column("landscaping_id")]
        public int landscaping_id { get; set; }

        public int plant_id { get; set; }

        public string landscaping_use { get; set; }

        public string moisture_requirement { get; set; }

        public string light_requirement { get; set; }

        public string soil_requirement { get; set; }

        public string seasonal_interest { get; set; }

        public string cultivars { get; set; }

        public string availability { get; set; }

        public virtual Plant plant { get; set; }
    }


    [Table("human_connections", Schema = "plant")]
    public class HumanConnection
    {
        [Key]
        [Column("landscaping_id")]
        public int human_connection_id { get; set; }

        public int plant_id { get; set; }

        public string landscaping_use { get; set; }

        public string livestock_uses { get; set; }

        public string fiber { get; set; }

        public string other { get; set; }

        public virtual Plant plant { get; set; }
    }

    [Table("images", Schema = "plant")]
    public class Image
    {
        [Key]
        [Column("image_id")]
        public int image_id { get; set; }

        public int plant_id { get; set; }

        public string lo_res_path { get; set; }

        public string high_res_path { get; set; }

        public virtual Plant plant { get; set; }
    }

    [Table("locations", Schema = "plant")]
    public class Location
    {
        [Key]
        [Column("location_id")]
        public int location_id { get; set; }

        public int plant_id { get; set; }

        public string state { get; set; }

        public string county { get; set; }

        public virtual Plant plant { get; set; }
    }
}