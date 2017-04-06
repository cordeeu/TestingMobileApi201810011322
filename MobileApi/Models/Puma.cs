using System.ComponentModel.DataAnnotations;

namespace MobileApi.Models
{

    public class Puma : Repository<Puma>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}