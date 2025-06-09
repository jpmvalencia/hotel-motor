using System;
using System.Collections.Generic;
using System.Text;

namespace HotelMotorShared.Dtos.CustomerDTOs
{
    public class CustomerCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
