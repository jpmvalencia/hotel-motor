using HotelMotorShared.Models;

namespace HotelMotorApi.Interfaces
{
    public interface IOrderDetailsRepository
    {
        Task<IEnumerable<OrderDetails>> GetServicesByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderDetails>> AssignServicesToOrderAsync(int orderId, List<int> servicesIds);
        Task<bool> RemoveServiceFromOrderAsync(int orderId, int serviceId);
        Task<bool> IsServiceAssignedToOrderAsync(int orderId, int serviceId);
    }
}
