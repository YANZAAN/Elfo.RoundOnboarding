using System.ComponentModel.DataAnnotations;

namespace Journey.Domain
{
    public class UpdateCityModel
    {
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
    }
}