using HotelMotorApi.Common;
using HotelMotorApi.Interfaces;
using HotelMotorShared.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelMotorApi.Repositories
{
    public class VehiclesRepository : IVehiclesRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public VehiclesRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
        {
            return await _applicationDbContext.Vehicles.OrderByDescending(vehicle => vehicle.Model).ToListAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return await _applicationDbContext.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByCustomerIdAsync(int customerId)
        {
            return await _applicationDbContext.Vehicles
                .Where(v => v.Customer.Id == customerId)
                .OrderByDescending(v => v.Model)
                .ToListAsync();
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            await _applicationDbContext.Vehicles.AddAsync(vehicle);
            await _applicationDbContext.SaveChangesAsync();
            return vehicle;
        }

        public async Task<Vehicle> GetVehicleByPlateNumberAsync(string plateNumber)
        {
            return await _applicationDbContext.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.PlateNumber == plateNumber);
        }

        public async Task<Vehicle> UpdateVehicleAsync(int id, Vehicle vehicle)
        {
            var existingVehicle = await _applicationDbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
            existingVehicle.Brand = vehicle.Brand;
            existingVehicle.Model = vehicle.Model;
            existingVehicle.Type = vehicle.Type;
            existingVehicle.Year = vehicle.Year;
            existingVehicle.PlateNumber = vehicle.PlateNumber;
            
            await _applicationDbContext.SaveChangesAsync();
            return existingVehicle;
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _applicationDbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == id);
            if (vehicle == null)
            {
                return false;
            }
            _applicationDbContext.Vehicles.Remove(vehicle);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
