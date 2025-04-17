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
    }
}
