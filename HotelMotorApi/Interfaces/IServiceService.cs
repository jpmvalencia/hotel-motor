using HotelMotorShared.DTOs.ServiceDTOs;
using HotelMotorShared.Models;

namespace HotelMotorApi.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetServicesAsync();
        Task<Service> GetServiceByIdAsync(int id);
        Task<Service> AddServiceAsync(AddServiceDto addServiceDto);
        Task<Service> UpdateServiceAsync(int id, UpdateServiceDto serviceDto);
        Task<bool> DeleteServiceAsync(int id);
    }
}
