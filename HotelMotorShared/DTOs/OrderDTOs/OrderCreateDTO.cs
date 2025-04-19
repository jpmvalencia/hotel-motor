using System.ComponentModel.DataAnnotations;

namespace HotelMotorShared.Dtos.OrderDTOs

{
    public class OrderCreateDTO
    {
        [Required]
        public string Summary { get; set; }
        [Required]
        public int VehicleId { get; set; }
    }
}
