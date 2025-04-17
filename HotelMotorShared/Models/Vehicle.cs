using System;
using System.Collections.Generic;
using System.Text;

namespace HotelMotorShared.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public string PlateNumber { get; set; }
        public Customer customer { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
