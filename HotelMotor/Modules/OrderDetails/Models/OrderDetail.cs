using HotelMotor.Modules.Orders.Models;
using HotelMotor.Modules.Services.Models;

namespace HotelMotor.Modules.OrderDetails.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public Order Order { get; set; }
        public Service Service { get; set; }
    }
}
