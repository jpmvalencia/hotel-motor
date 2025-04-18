using HotelMotorApi.Interfaces;
using HotelMotorShared.Models;

namespace HotelMotorApi.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IVehiclesRepository _vehiclesRepository;

        public VehiclesService(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await _vehiclesRepository.GetVehiclesAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return await _vehiclesRepository.GetVehicleByIdAsync(id);
        }
    }
}
