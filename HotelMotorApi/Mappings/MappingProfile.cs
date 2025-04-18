using AutoMapper;
using HotelMotorShared.DTOs;
using HotelMotorShared.Models;

namespace HotelMotorApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.VehicleIds, opt => opt.MapFrom(src =>
                    src.Vehicles != null ? src.Vehicles.Select(v => v.Id).ToList() : new List<int>()));

        }
    }
}
