using AutoMapper;
using HotelMotorShared.Dtos.CustomerDTOs;
using HotelMotorShared.Dtos;
using HotelMotorShared.DTOs;
using HotelMotorShared.Models;
using HotelMotorShared.DTOs.ServiceDTOs;
using HotelMotorShared.Dtos.OrderDTOs;

namespace HotelMotorApi.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.VehicleIds, opt => opt.MapFrom(src =>
                    src.Vehicles != null ? src.Vehicles.Select(v => v.Id).ToList() : new List<int>()));

            CreateMap<CustomerCreateDto, Customer>();
            CreateMap<CustomerUpdateDto, Customer>();

            CreateMap<Vehicle, VehicleDTO>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.Id));

            CreateMap<Service, ServiceDto>();

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Vehicle, opt => opt.MapFrom(src => src.Vehicle))
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.OrderDetails));

            CreateMap<OrderCreateDTO, Order>();
            CreateMap<OrderUpdateDTO, Order>();

            CreateMap<OrderDetails, ServiceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Service.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Service.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Service.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Service.Price));

        }
    }
}
