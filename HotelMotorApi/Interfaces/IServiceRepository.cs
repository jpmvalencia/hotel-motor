using HotelMotorShared.Models;

namespace HotelMotorApi.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetServicesAsync();
        Task<Service> GetServiceByIdAsync(int id);
        Task<Service> AddServiceAsync(Service service);
        Task<Service> UpdateServiceAsync(int id, Service service);
        Task<bool> DeleteServiceAsync(int id);
    }
}
