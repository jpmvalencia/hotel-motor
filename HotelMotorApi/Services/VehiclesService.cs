using HotelMotorApi.Interfaces;
using HotelMotorShared.Dtos;
using HotelMotorShared.Models;

namespace HotelMotorApi.Services
{
    public class VehiclesService : IVehiclesService
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly ICustomerRepository _customerRepository;
        public VehiclesService(IVehiclesRepository vehiclesRepository, ICustomerRepository customerRepository)
        {
            _vehiclesRepository = vehiclesRepository;
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await _vehiclesRepository.GetVehiclesAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return await _vehiclesRepository.GetVehicleByIdAsync(id);
        }

        public async Task<Vehicle> AddVehicleAsync(AddVehicleDTO addVehicleDto)
        {
            var customer = await _customerRepository.GetByIdAsync(addVehicleDto.CustomerId);

            if (customer == null)
            {
                throw new Exception("Cliente no encontrado");
            }
            if (await _vehiclesRepository.GetVehicleByPlateNumberAsync(addVehicleDto.PlateNumber) != null)
            {
                throw new Exception("Ya hay un vehículo registrado con ese número de placa");
            }

            var vehicle = new Vehicle
            {
                Brand = addVehicleDto.Brand,
                Model = addVehicleDto.Model,
                Type = addVehicleDto.Type,
                Year = addVehicleDto.Year,
                PlateNumber = addVehicleDto.PlateNumber,
                Customer = customer
            };
            return await _vehiclesRepository.AddVehicleAsync(vehicle);
        }

        public async Task<Vehicle> UpdateVehicleAsync(int id, UpdateVehicleDTO updateVehicleDto)
        {
            var vehicle = await _vehiclesRepository.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                throw new Exception("Vehículo no encontrado");
            }
            vehicle.Brand = updateVehicleDto.Brand;
            vehicle.Model = updateVehicleDto.Model;
            vehicle.Type = updateVehicleDto.Type;
            vehicle.Year = updateVehicleDto.Year;
            return await _vehiclesRepository.UpdateVehicleAsync(id, vehicle);
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _vehiclesRepository.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                throw new Exception("Vehículo no encontrado");
            }
            return await _vehiclesRepository.DeleteVehicleAsync(id);
        }
    }
}
