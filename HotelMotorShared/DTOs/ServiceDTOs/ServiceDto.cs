﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HotelMotorShared.DTOs.ServiceDTOs
{
    public class ServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
