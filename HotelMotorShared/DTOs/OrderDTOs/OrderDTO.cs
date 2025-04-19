using HotelMotorShared.Models;

namespace HotelMotorShared.Dtos.OrderDTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public VehicleDTO Vehicle { get; set; }
        public ICollection<OrderService> OrderServices { get; set; } = new List<OrderService>();
    }
}
