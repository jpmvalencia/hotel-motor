using HotelMotorShared.Models;

namespace HotelMotorApi.Interfaces
{
    public interface IVehiclesRepository
    {
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
        Task<Vehicle> GetVehicleByPlateNumberAsync(string plateNumber);
        Task<Vehicle> UpdateVehicleAsync(int id, Vehicle vehicle);
        Task<bool> DeleteVehicleAsync(int id);
    }
}
