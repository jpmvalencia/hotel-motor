using HotelMotor.Modules.Orders.Models;

namespace HotelMotor.Modules.Orders
{
    public class OrderService
    {
        private List<Order> _orders = new List<Order>();

        public List<Order> GetOrders()
        {
            return _orders;
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }
    }
}
