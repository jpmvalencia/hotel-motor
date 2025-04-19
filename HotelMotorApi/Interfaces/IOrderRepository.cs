using HotelMotorShared.Models;

namespace HotelMotorApi.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<Order?> UpdateAsync(Order order);
        Task<bool> ExistsAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
