using System;

namespace Journey.Persistence.Entities
{
    public class PlaceOfInterest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid CityOwnerId { get; set; }
        public City CityOwner { get; set; }
    }
}