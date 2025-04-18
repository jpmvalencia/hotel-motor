using HotelMotorApi.Common;
using HotelMotorApi.Interfaces;
using HotelMotorShared.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelMotorApi.Repositories
{
    public class ServiceRepository: IServiceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ServiceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            return await _applicationDbContext.Services.ToListAsync();
        }

        public async Task<Service> GetServiceByIdAsync(int id)
        {
            return await _applicationDbContext.Services.FirstOrDefaultAsync(service => service.Id == id);
        }

        public async Task<Service> AddServiceAsync(Service service)
        {
            await _applicationDbContext.Services.AddAsync(service);
            await _applicationDbContext.SaveChangesAsync();
            return service;
        }

        public async Task<Service> UpdateServiceAsync(int id, Service service)
        {
            var existingService = await _applicationDbContext.Services.FirstOrDefaultAsync(s => s.Id == id);
            existingService.Name = service.Name;
            existingService.Description = service.Description;
            existingService.Price = service.Price;
            _applicationDbContext.Services.Update(existingService);
            await _applicationDbContext.SaveChangesAsync();
            return existingService;
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var service = await _applicationDbContext.Services.FirstOrDefaultAsync(s => s.Id == id);
            if (service == null)
            {
                return false;
            }
            _applicationDbContext.Services.Remove(service);
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }
    }
}
