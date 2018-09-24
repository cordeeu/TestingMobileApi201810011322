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
    [Table("plants", Schema = "flower")]
    public class Flower
    {
        [Key]
        [Column("plant_id")]
        [DataMember]
        public int plant_id { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string familyScientific { get; set; }
        [DataMember]
        public string familyScientific2 { get; set; }
        [DataMember]
        public string familyCommon { get; set; }
        [DataMember]
        public string genusSpeciesWeber { get; set; }
        [DataMember]
        public string genusSpeciesAckerfield { get; set; }
        [DataMember]
        public string genusWeber { get; set; }
        [DataMember]
        public string speciesWeber { get; set; }
        [DataMember]
        public string genusAckerfield { get; set; }
        [DataMember]
        public string commonName1 { get; set; }
        [DataMember]
        public string commonName2 { get; set; }
        [DataMember]
        public string color { get; set; }
        [DataMember]
        public string month { get; set; }
        [DataMember]
        public string zone { get; set; }
        [DataMember]
        public string origin { get; set; }
        [DataMember]
        public string noxious { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string similar { get; set; }
        [DataMember]
        public string photos { get; set; }
        [DataMember]
        public string thumbnail { get; set; }
    }


    [Table("settings", Schema = "flower")]
    public class FlowerSetting
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