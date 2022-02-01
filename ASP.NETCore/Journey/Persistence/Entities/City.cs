using System;
using System.Collections.Generic;

namespace Journey.Persistence.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        
        public ICollection<PlaceOfInterest> IntrestingPlaces { get; set; }
    }
}