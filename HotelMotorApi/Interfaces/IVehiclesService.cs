using HotelMotorShared.Models;

namespace HotelMotorApi.Interfaces
{
    public interface IVehiclesService
    {
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(int id);
    }
}
