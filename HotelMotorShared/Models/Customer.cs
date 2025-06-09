using System;
using System.Collections.Generic;
using System.Text;

namespace HotelMotorShared.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
