using System;
using System.Collections.Generic;
using System.Text;

namespace HotelMotorShared.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<OrderService> OrderServices { get; set; }
    }
}
