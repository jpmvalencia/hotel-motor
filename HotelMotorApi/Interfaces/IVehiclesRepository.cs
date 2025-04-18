using HotelMotorShared.Models;

namespace HotelMotorApi.Interfaces
{
    public interface IVehiclesRepository
    {
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(int id);
    }
}
