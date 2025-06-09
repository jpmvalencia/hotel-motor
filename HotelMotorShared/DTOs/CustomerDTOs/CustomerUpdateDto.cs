using System;
using System.Collections.Generic;
using System.Text;

namespace HotelMotorShared.Dtos.CustomerDTOs
{
    public class CustomerUpdateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
