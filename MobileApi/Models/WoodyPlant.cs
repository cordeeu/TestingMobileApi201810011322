using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MobileApi.Models
{
    [DataContract(IsReference = true)]
    [Table("plants", Schema = "woody")]
    public class WoodyPlant
    {
        [Key]
        [Column("plant_id")]
        [DataMember]
        public int plant_id { get; set; }
        [DataMember]
        public int plant_imported_id { get; set; }
        [DataMember]
        public string family { get; set; }
        [DataMember]
        public string scientificNameWeber { get; set; }
        [DataMember]
        public string leafType { get; set; }
        [DataMember]
        public string leafShape { get; set; }
        [DataMember]
        public string growthForm { get; set; }
        [DataMember]
        public string growthDuration { get; set; }
        [DataMember]
        public string scientificNameOther { get; set; }
        [DataMember]
        public string commonName { get; set; }
        [DataMember]
        public string commonNameSecondary { get; set; }
        [DataMember]
        public string scientificNameMeaningWeber { get; set; }
        [DataMember]
        public string plantClass { get; set; }
        [DataMember]
        public string plantSubClass { get; set; }
        [DataMember]
        public string origin { get; set; }
        [DataMember]
        public string weedManagement { get; set; }
        [DataMember]
        public string edibility { get; set; }
        [DataMember]
        public string livestock { get; set; }
        [DataMember]
        public string toxicity { get; set; }
        [DataMember]
        public string ecologicalRelationships { get; set; }
        [DataMember]
        public string frequency { get; set; }
        [DataMember]
        public string habitat { get; set; }
        [DataMember]
        public string scientificNameNelson { get; set; }
        [DataMember]
        public string scientificNameMeaningNelson { get; set; }
        [DataMember]
        public string seasonOfBloom { get; set; }
        [DataMember]
        public string familyCharacteristics { get; set; }
        [DataMember]
        public string flowerSymmetry { get; set; }
        [DataMember]
        public string flowerCluster { get; set; }
        [DataMember]
        public string flowerShape { get; set; }
        [DataMember]
        public string commonNameDerivation { get; set; }
        [DataMember]
        public string landscapingCultivar { get; set; }
        [DataMember]
        public string flowerColor { get; set; }
        [DataMember]
        public string fruitColor { get; set; }
        [DataMember]
        public string availability { get; set; }
        [DataMember]
        public string keyCharacteristics { get; set; }
        [DataMember]
        public string lifeZone { get; set; }
        [DataMember]
        public string endemicLocation { get; set; }
        [DataMember]
        public string landscapingUse { get; set; }
        [DataMember]
        public string matureHeight { get; set; }
        [DataMember]
        public string matureSpread { get; set; }
        [DataMember]
        public string lightRequirements { get; set; }
        [DataMember]
        public string soilRequirements { get; set; }
        [DataMember]
        public string fiber { get; set; }
        [DataMember]
        public string otherInformation { get; set; }
        [DataMember]
        public string flowerSize { get; set; }
        [DataMember]
        public string petalNumber { get; set; }
        [DataMember]
        public string flowerStructure { get; set; }
        [DataMember]
        public string moistureRequirements { get; set; }
        [DataMember]
        public string pronunciation { get; set; }
        [DataMember]
        public string fruitType { get; set; }
        [DataMember]
        public string subspecies { get; set; }
        [DataMember]
        public string variety { get; set; }
        [DataMember]
        public string forma { get; set; }
        [DataMember]
        public string legalStatus { get; set; }
        [DataMember]
        public string guanellaPass { get; set; }
        [DataMember]
        public string plainCc { get; set; }
        [DataMember]
        public string noNameCreek { get; set; }
        [DataMember]
        public string maloitPark { get; set; }
        [DataMember]
        public string vailNc { get; set; }
        [DataMember]
        public string lovelandPass { get; set; }
        [DataMember]
        public string roxborough { get; set; }
        [DataMember]
        public string castlewood { get; set; }
        [DataMember]
        public string custerCounty { get; set; }
        [DataMember]
        public string dbg { get; set; }
        [DataMember]
        public string grassesAtGreenMtn { get; set; }
        [DataMember]
        public string eastPortal { get; set; }
        [DataMember]
        public string mesaCounty { get; set; }
        [DataMember]
        public string tellerCounty { get; set; }
        [DataMember]
        public string goldenGate { get; set; }
        [DataMember]
        public string southPlattePark { get; set; }
        [DataMember]
        public string greenMt { get; set; }
        [DataMember]
        public string reynolds { get; set; }
        [DataMember]
        public string grassesManual { get; set; }
        [DataMember]
        public string falcon { get; set; }
        [DataMember]
        public string lookoutMt { get; set; }
        [DataMember]
        public string southValley { get; set; }
        [DataMember]
        public string deerCreek { get; set; }
        [DataMember]
        public string lairOTheBear { get; set; }
        [DataMember]
        public string print { get; set; }
        [DataMember]
        public string highPlains { get; set; }
        [DataMember]
        public string shrubs { get; set; }

        [DataMember]
        public virtual ICollection<ImageWoody> Images { get; set; }
    }

    [Table("other_common_names", Schema = "plant")]
    public class OtherCommonNameWoody
    {
        [Key]
        [Column("other_common_name_id")]
        public int other_common_name_id { get; set; }

        public int plant_id { get; set; }

        public string other_common_name { get; set; }

        public virtual WoodyPlant plant { get; set; }
    }

    [Table("scientific_name", Schema = "plant")]
    public class ScientificNameWoody
    {
        [Key]
        [Column("scientific_name_id")]
        public int scientific_name_id { get; set; }

        public int plant_id { get; set; }

        public string subspecies { get; set; }

        public string variety { get; set; }

        public string authors { get; set; }

        public string scientific_name_meaning { get; set; }

        public virtual WoodyPlant plant { get; set; }
    }


    [Table("synonyms", Schema = "plant")]
    public class SynonymWoody
    {
        [Key]
        [Column("synonym_id")]
        public int synonym_id { get; set; }

        public int plant_id { get; set; }

        public string synonym { get; set; }

        public virtual WoodyPlant plant { get; set; }
    }


    [Table("identifications", Schema = "plant")]
    public class IdentificationWoody
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

        public virtual WoodyPlant plant { get; set; }
    }


    [Table("ecologies", Schema = "plant")]
    public class EcologyWoody
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

        public virtual WoodyPlant plant { get; set; }
    }


    [Table("landscapings", Schema = "plant")]
    public class LandscapingWoody
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

        public virtual WoodyPlant plant { get; set; }
    }


    [Table("human_connections", Schema = "plant")]
    public class HumanConnectionWoody
    {
        [Key]
        [Column("landscaping_id")]
        public int human_connection_id { get; set; }

        public int plant_id { get; set; }

        public string landscaping_use { get; set; }

        public string livestock_uses { get; set; }

        public string fiber { get; set; }

        public string other { get; set; }

        public virtual WoodyPlant plant { get; set; }
    }

    [DataContract(IsReference = true)]
    [Table("images", Schema = "plant")]
    public class ImageWoody
    {
        [Key]
        [Column("imageId")]
        [DataMember]
        public int imageId { get; set; }
        [DataMember]
        public int plantId { get; set; }
        [DataMember]
        public string loResPath { get; set; }
        [DataMember]
        public string highResPath { get; set; }

        [ForeignKey("plantId")]
        [DataMember]
        public virtual WoodyPlant plant { get; set; }
    }

    [Table("locations", Schema = "plant")]
    public class LocationWoody
    {
        [Key]
        [Column("location_id")]
        public int location_id { get; set; }

        public int plant_id { get; set; }

        public string state { get; set; }

        public string county { get; set; }

        public virtual WoodyPlant plant { get; set; }
    }

    [Table("settings", Schema = "woody")]
    public class WoodySetting
    {
        [Key]
        [Column("settingid")]
        public int settingid { get; set; }
        public string name { get; set; }
        public DateTime? valuetimestamp { get; set; }
        public string valuetext { get; set; }
        public decimal? valueamount { get; set; }
        public bool? valuebool { get; set; }
        public long? valueint { get; set; }
    }
}