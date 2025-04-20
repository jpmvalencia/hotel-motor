using HotelMotorShared.Dtos.OrderDTOs;

namespace HotelMotorApi.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(int id);
        Task<OrderDTO> CreateOrderAsync(OrderCreateDTO orderDto);
        Task<OrderDTO?> UpdateOrderAsync(int id, OrderUpdateDTO orderDto);
        Task<bool> DeleteOrderAsync(int id);
        Task<OrderDTO> AddServicesToOrder(int orderId, List<int> servicesIds);
    }
}
