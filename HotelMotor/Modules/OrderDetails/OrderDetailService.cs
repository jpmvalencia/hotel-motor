using HotelMotor.Modules.OrderDetails.Models;

namespace HotelMotor.Modules.OrderDetails
{
    public class OrderDetailService
    {
        private List<OrderDetail> _orderDetails = new List<OrderDetail>();

        public List<OrderDetail> GetOrderDetails()
        {
            return _orderDetails;
        }

        public void AddOrderDetail(OrderDetail orderDetail)
        {
            _orderDetails.Add(orderDetail);
        }
    }
}
