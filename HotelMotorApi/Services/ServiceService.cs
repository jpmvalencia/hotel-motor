using HotelMotorApi.Interfaces;
using HotelMotorShared.DTOs.ServiceDTOs;
using HotelMotorShared.Models;

namespace HotelMotorApi.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            return await _serviceRepository.GetServicesAsync();
        }
        public async Task<Service> GetServiceByIdAsync(int id)
        {
            return await _serviceRepository.GetServiceByIdAsync(id);
        }
        public async Task<Service> AddServiceAsync(AddServiceDto addServiceDto)
        { 
            var service = new Service
            {
                Name = addServiceDto.Name,
                Description = addServiceDto.Description,
                Price = addServiceDto.Price
            };
            return await _serviceRepository.AddServiceAsync(service);
        }

        public async Task<Service> UpdateServiceAsync(int id, UpdateServiceDto serviceDto)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);
            if (service == null)
            {
                throw new Exception("Service not found");
            }
            service.Name = serviceDto.Name;
            service.Description = serviceDto.Description;
            service.Price = serviceDto.Price;
            return await _serviceRepository.UpdateServiceAsync(id, service);
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var service = await _serviceRepository.GetServiceByIdAsync(id);
            if (service == null)
            {
                throw new Exception("Service not found");
            }
            return await _serviceRepository.DeleteServiceAsync(id);
        }
    }
}
