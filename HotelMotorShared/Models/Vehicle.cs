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
        public Customer Customer { get; set; }
        public ICollection<Order> Orders { get; set; }

        public static implicit operator Vehicle(Task<Vehicle> v)
        {
            throw new NotImplementedException();
        }
    }
}
