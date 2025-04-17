using System;
using System.Collections.Generic;
using System.Text;

namespace HotelMotorShared.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Vehicle Vehicle { get; set; }
        public ICollection<OrderService> OrderServices { get; set; }
    }
}
