using HotelMotorShared.Dtos.OrderDTOs;

namespace HotelMotorApi.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(int id);
        Task<OrderDTO> CreateOrderAsync(OrderCreateDTO order);
    }
}
