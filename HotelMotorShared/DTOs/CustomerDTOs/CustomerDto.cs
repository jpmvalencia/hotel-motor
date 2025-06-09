namespace HotelMotorShared.Dtos.CustomerDTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<int> VehicleIds { get; set; } = new List<int>();
    }
}
