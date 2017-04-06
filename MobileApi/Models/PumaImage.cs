using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MobileApi.Models
{
    public class PumaImage
    {
        [Key]
        public int Id { get; set; }
        public int PumaId { get; set; }
        public string ImageFilename { get; set; }
        public string Credit { get; set; }
    }
}