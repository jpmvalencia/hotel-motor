using HotelMotorShared.Dtos.CustomerDTOs;

namespace HotelMotorApi.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();
        Task<CustomerDto?> GetByIdAsync(int id);
        Task<CustomerDto> CreateAsync(CustomerCreateDto customerDto);
        Task<CustomerDto?> UpdateAsync(int id, CustomerUpdateDto customerDto);
        Task<bool> DeleteAsync(int id);
        Task<CustomerDto?> SearchOneAsync(string searchTerm);
    }
}
