
using System.ComponentModel.DataAnnotations;

namespace Journey.Domain
{
    public class CreateCityModel
    {
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
    }
}