using System;
using System.ComponentModel.DataAnnotations;

namespace Journey.Domain
{
    public class CityDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Country { get; set; }
    }
}