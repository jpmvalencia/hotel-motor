using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelMotorShared.Dtos
{
    public class UpdateVehicleDTO
    {
        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public int Year { get; set; }
    }
}
