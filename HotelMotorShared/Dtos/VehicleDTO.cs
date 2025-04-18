using System;
using System.Collections.Generic;
using System.Text;

namespace HotelMotorShared.Dtos
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
        public string PlateNumber { get; set; }
        public int CustomerId { get; set; }
    }
}
