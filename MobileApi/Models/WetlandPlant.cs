using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MobileApi.Models
{
    [Table("plants", Schema = "wetland")]
    public class WetlandPlant
    {
        [Key]
        [Column("plantid")]
        public int plantid { get; set; }
        public int id { get; set; }
             public string scinameauthor { get; set; }
         public string scinamenoauthor { get; set; }
         public string family { get; set; }
           public string commonname { get; set; }
           public string plantscode { get; set; }
           public string mapimg { get; set; }
        public string itiscode { get; set; }
        public string awwetcode { get; set; }
        public string gpwetcode { get; set; }
        public string wmvcwetcode { get; set; }
        public string cvalue { get; set; }
        public string grank { get; set; }
        public string federalstatus { get; set; }
        public string cosrank { get; set; }
        public string mtsrank { get; set; }
        public string ndsrank { get; set; }
        public string sdsrank { get; set; }
        public string utsrank { get; set; }
        public string wysrank { get; set; }
        public string nativity { get; set; }
        public string noxiousweed { get; set; }
        public int elevminfeet { get; set; }
        public int elevminm { get; set; }
        public int elevmaxfeet { get; set; }
        public int elevmaxm { get; set; }
        public string keychar1 { get; set; }
        public string keychar2 { get; set; }
        public string keychar3 { get; set; }
        public string keychar4 { get; set; }
        public string keychar5 { get; set; }
        public string keychar6 { get; set; }
        public string similarsp { get; set; }
        public string habitat { get; set; }
        public string comments { get; set; }
        public string animaluse { get; set; }
        public string ecologicalsystems { get; set; }
        public string synonyms { get; set; }
        public string topimgtopimg { get; set; }
        public string duration { get; set; }

        public virtual IList<ImagesWetland> ImagesWetland { get; set; }
        public virtual IList<SimilarSpeciesWetland> SimilarSpeciesWetland { get; set; }
        public virtual IList<CountyPlantWetland> CountyPlantWetland { get; set; }
        public virtual IList<ReferenceWetland> ReferenceWetland { get; set; }
        public virtual IList<FruitWetland> FruitWetland { get; set; }
        public virtual IList<DivisionWetland> DivisionWetland { get; set; }
        public virtual IList<ShapeWetland> ShapeWetland { get; set; }
        public virtual IList<ArrangementWetland> ArrangementWetland { get; set; }
        public virtual IList<SizeWetland> SizeWetland { get; set; }
        public virtual IList<RegionWetland> RegionWetland { get; set; }
    }

    [Table("images", Schema = "wetland")]
    public class ImagesWetland
    {
        [Key]
        [Column("imageid")]
        public int imageid { get; set; }

        public int id { get; set; }

        public int plantid { get; set; }

        public string filename { get; set; }

        public string credit { get; set; }

        public virtual WetlandPlant WetlandPlant { get; set; }
    }




    [Table("similar_species", Schema = "wetland")]
    public class SimilarSpeciesWetland
    {
        [Key]
        [Column("similarspeciesid")]
        public int similarspeciesid { get; set; }

        public int id { get; set; }

        public int plantid { get; set; }

        public string similarspicon { get; set; }

        public string similarspscinameauthor { get; set; }

        public string reason { get; set; }

        public virtual WetlandPlant WetlandPlant { get; set; }
    }


    [Table("county_plant", Schema = "wetland")]
    public class CountyPlantWetland
    {
        [Key]
        [Column("countyplantid")]
        public int countyplantid { get; set; }

        public int county_id { get; set; }

        public int plantid { get; set; }

        public string name { get; set; }

        public virtual WetlandPlant WetlandPlant { get; set; }
    }



    [Table("references", Schema = "wetland")]
    public class ReferenceWetland
    {
        [Key]
        [Column("referenceid")]
        public int referenceid { get; set; }

        public int id { get; set; }

        public int plantid { get; set; }

        [Column("reference")]
        public string reference { get; set; }

        public string fullcitation { get; set; }

        public virtual WetlandPlant WetlandPlant { get; set; }
    }

    [Table("fruits", Schema = "wetland")]
    public class FruitWetland
    {
        [Key]
        [Column("fruitid")]
        public int fruitid { get; set; }

        public int plantid { get; set; }

        public int valueid { get; set; }

        public virtual WetlandPlant WetlandPlant { get; set; }
    }


    [Table("division", Schema = "wetland")]
    public class DivisionWetland
    {
        [Key]
        [Column("divisionid")]
        public int divisionid { get; set; }

        public int plantid { get; set; }

        public int valueid { get; set; }

        public virtual WetlandPlant WetlandPlant { get; set; }
    }


    [Table("shape", Schema = "wetland")]
    public class ShapeWetland
    {
        [Key]
        [Column("shapeid")]
        public int shapeid { get; set; }

        public int plantid { get; set; }

        public int valueid { get; set; }

        public virtual WetlandPlant WetlandPlant { get; set; }
    }


    [Table("arrangement", Schema = "wetland")]
    public class ArrangementWetland
    {
        [Key]
        [Column("arrangementid")]
        public int arrangementid { get; set; }

        public int plantid { get; set; }

        public int valueid { get; set; }

        public virtual WetlandPlant WetlandPlant { get; set; }
    }


    [Table("size", Schema = "wetland")]
    public class SizeWetland
    {
        [Key]
        [Column("sizeid")]
        public int sizeid { get; set; }

        public int plantid { get; set; }

        public int valueid { get; set; }

        public virtual WetlandPlant WetlandPlant { get; set; }
    }


    [Table("regions", Schema = "wetland")]
    public class RegionWetland
    {
        [Key]
        [Column("regionid")]
        public int regionid { get; set; }

        public int plantid { get; set; }

        public int valueid { get; set; }

        public virtual WetlandPlant WetlandPlant { get; set; }
    }



}