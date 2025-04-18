﻿namespace HotelMotorShared.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<int> VehicleIds { get; set; } = new List<int>();
    }
}
