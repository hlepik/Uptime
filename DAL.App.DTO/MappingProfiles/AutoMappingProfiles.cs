using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<DAL.App.DTO.Reservation, Domain.App.Reservation>().ReverseMap();

        }
    }
}