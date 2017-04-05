using System.ComponentModel.DataAnnotations;

namespace MobileApi.Models
{

    public class PumaType : Repository<PumaType>
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}