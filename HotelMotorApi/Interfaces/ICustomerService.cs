using HotelMotorShared.DTOs;

namespace HotelMotorApi.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllAsync();
        Task<CustomerDTO> GetByIdAsync(int id);
        Task<CustomerDTO> CreateAsync(CustomerDTO customer);
        Task<CustomerDTO> UpdateAsync(CustomerDTO customer);
        Task<bool> DeleteAsync(int id);
    }
}
