using AutoMapper;
using Journey.Domain;
using Journey.Persistence.Entities;

namespace Journey.Utility.MapperProfiles
{
    public class DTOProfile : Profile
    {
        public DTOProfile()
        {

        }

        private void MapDTO()
        {
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<PlaceOfInterest, PlaceOfInterestDTO>().ReverseMap();
        }
    }
}