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
    [Table("plants", Schema = "wetland")]
    public class WetlandPlant
    {
        [Key]
        [Column("plantid")]
        [DataMember]
        public int plantid { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string sections { get; set; }
        [DataMember]
        public string scinameauthor { get; set; }
        [DataMember]
        public string scinamenoauthor { get; set; }
        [DataMember]
        public string family { get; set; }
        [DataMember]
        public string commonname { get; set; }
        [DataMember]
        public string plantscode { get; set; }
        [DataMember]
        public string mapimg { get; set; }
        [DataMember]
        public string itiscode { get; set; }
        [DataMember]
        public string awwetcode { get; set; }
        [DataMember]
        public string gpwetcode { get; set; }
        [DataMember]
        public string wmvcwetcode { get; set; }
        [DataMember]
        public string cvalue { get; set; }
        [DataMember]
        public string grank { get; set; }
        [DataMember]
        public string federalstatus { get; set; }
        [DataMember]
        public string cosrank { get; set; }
        [DataMember]
        public string mtsrank { get; set; }
        [DataMember]
        public string ndsrank { get; set; }
        [DataMember]
        public string sdsrank { get; set; }
        [DataMember]
        public string utsrank { get; set; }
        [DataMember]
        public string wysrank { get; set; }
        [DataMember]
        public string nativity { get; set; }
        [DataMember]
        public string noxiousweed { get; set; }
        [DataMember]
        public int elevminfeet { get; set; }
        [DataMember]
        public int elevminm { get; set; }
        [DataMember]
        public int elevmaxfeet { get; set; }
        [DataMember]
        public int elevmaxm { get; set; }
        [DataMember]
        public string keychar1 { get; set; }
        [DataMember]
        public string keychar2 { get; set; }
        [DataMember]
        public string keychar3 { get; set; }
        [DataMember]
        public string keychar4 { get; set; }
        [DataMember]
        public string keychar5 { get; set; }
        [DataMember]
        public string keychar6 { get; set; }
        [DataMember]
        public string similarsp { get; set; }
        [DataMember]
        public string habitat { get; set; }
        [DataMember]
        public string comments { get; set; }
        [DataMember]
        public string animaluse { get; set; }
        [DataMember]
        public string ecologicalsystems { get; set; }
        [DataMember]
        public string synonyms { get; set; }
        [DataMember]
        public string topimgtopimg { get; set; }
        [DataMember]
        public string duration { get; set; }

        [DataMember]
        public virtual ICollection<ImagesWetland> Images { get; set; }
        [DataMember]
        public virtual ICollection<SimilarSpeciesWetland> SimilarSpeciesWetland { get; set; }
        [DataMember]
        public virtual ICollection<CountyPlantWetland> CountyPlantWetland { get; set; }
        [DataMember]
        public virtual ICollection<ReferenceWetland> References { get; set; }
        [DataMember]
        public virtual ICollection<FruitWetland> FruitWetland { get; set; }
        [DataMember]
        public virtual ICollection<DivisionWetland> DivisionWetland { get; set; }
        [DataMember]
        public virtual ICollection<ShapeWetland> ShapeWetland { get; set; }
        [DataMember]
        public virtual ICollection<ArrangementWetland> ArrangementWetland { get; set; }
        [DataMember]
        public virtual ICollection<SizeWetland> SizeWetland { get; set; }
        [DataMember]
        public virtual ICollection<RegionWetland> RegionWetland { get; set; }
    }

    [DataContract(IsReference = true)]
    [Table("images", Schema = "wetland")]
    public class ImagesWetland
    {
        [Key]
        [Column("imageid")]
        [DataMember]
        public int imageid { get; set; }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public int plantid { get; set; }

        [DataMember]
        public string filename { get; set; }

        [DataMember]
        public string credit { get; set; }

        [ForeignKey("plantid")]
        [DataMember]
        public virtual WetlandPlant WetlandPlant { get; set; }
    }



    [DataContract(IsReference = true)]
    [Table("similar_species", Schema = "wetland")]
    public class SimilarSpeciesWetland
    {
        [Key]
        [DataMember]
        [Column("similarspeciesid")]
        public int similarspeciesid { get; set; }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        public int plantid { get; set; }

        [DataMember]
        public string similarspicon { get; set; }

        [DataMember]
        public string similarspscinameauthor { get; set; }

        [DataMember]
        public string reason { get; set; }

        [ForeignKey("plantid")]
        [DataMember]
        public virtual WetlandPlant WetlandPlant { get; set; }
    }

    [DataContract(IsReference = true)]
    [Table("county_plant", Schema = "wetland")]
    public class CountyPlantWetland
    {
        [Key]
        [DataMember]
        [Column("countyplantid")]
        public int countyplantid { get; set; }

        [DataMember]
        public int county_id { get; set; }

        [DataMember]
        public int plantid { get; set; }

        [DataMember]
        public string name { get; set; }

        [ForeignKey("plantid")]
        [DataMember]
        public virtual WetlandPlant WetlandPlant { get; set; }
    }


    [DataContract(IsReference = true)]
    [Table("references", Schema = "wetland")]
    public class ReferenceWetland
    {
        [Key]
        [DataMember]
        [Column("referenceid")]
        public int referenceid { get; set; }

        [DataMember]
        public int plantid { get; set; }

        [DataMember]
        public int id { get; set; }

        [DataMember]
        [Column("reference")]
        public string reference { get; set; }

        [DataMember]
        public string fullcitation { get; set; }

        [ForeignKey("plantid")]
        [DataMember]
        public virtual WetlandPlant WetlandPlant { get; set; }
    }

    [DataContract(IsReference = true)]
    [Table("fruits", Schema = "wetland")]
    public class FruitWetland
    {
        [Key]
        [DataMember]
        [Column("fruitid")]
        public int fruitid { get; set; }

        [DataMember]
        public int plantid { get; set; }

        [DataMember]
        public int valueid { get; set; }

        [ForeignKey("plantid")]
        [DataMember]
        public virtual WetlandPlant WetlandPlant { get; set; }
    }

    [DataContract(IsReference = true)]
    [Table("division", Schema = "wetland")]
    public class DivisionWetland
    {
        [Key]
        [DataMember]
        [Column("divisionid")]
        public int divisionid { get; set; }

        [DataMember]
        public int plantid { get; set; }

        [DataMember]
        public int valueid { get; set; }

        [ForeignKey("plantid")]
        [DataMember]
        public virtual WetlandPlant WetlandPlant { get; set; }
    }

    [DataContract(IsReference = true)]
    [Table("shape", Schema = "wetland")]
    public class ShapeWetland
    {
        [Key]
        [DataMember]
        [Column("shapeid")]
        public int shapeid { get; set; }

        [DataMember]
        public int plantid { get; set; }

        [DataMember]
        public int valueid { get; set; }

        [ForeignKey("plantid")]
        [DataMember]
        public virtual WetlandPlant WetlandPlant { get; set; }
    }

    [DataContract(IsReference = true)]
    [Table("arrangement", Schema = "wetland")]
    public class ArrangementWetland
    {
        [Key]
        [DataMember]
        [Column("arrangementid")]
        public int arrangementid { get; set; }


        [DataMember]
        public int plantid { get; set; }

        [DataMember]
        public int valueid { get; set; }

        [ForeignKey("plantid")]
        [DataMember]
        public virtual WetlandPlant WetlandPlant { get; set; }
    }

    [DataContract(IsReference = true)]
    [Table("size", Schema = "wetland")]
    public class SizeWetland
    {
        [Key]
        [DataMember]
        [Column("sizeid")]
        public int sizeid { get; set; }


        [DataMember]
        public int plantid { get; set; }

        [DataMember]
        public int valueid { get; set; }

        [ForeignKey("plantid")]
        [DataMember]
        public virtual WetlandPlant WetlandPlant { get; set; }
    }

    [DataContract(IsReference = true)]
    [Table("regions", Schema = "wetland")]
    public class RegionWetland
    {
        [Key]
        [DataMember]
        [Column("regionid")]
        public int regionid { get; set; }

        [DataMember]
        public int plantid { get; set; }

        [DataMember]
        public int valueid { get; set; }

        [ForeignKey("plantid")]
        [DataMember]
        public virtual WetlandPlant WetlandPlant { get; set; }
    }

    [Table("settings", Schema = "wetland")]
    public class WetlandSetting
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