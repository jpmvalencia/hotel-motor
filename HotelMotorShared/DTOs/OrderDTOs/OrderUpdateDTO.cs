using System.ComponentModel.DataAnnotations;
using HotelMotorShared.Models;

namespace HotelMotorShared.Dtos.OrderDTOs
{
    public class OrderUpdateDTO
    {
        [Required(ErrorMessage = "El resumen de la orden es obligatorio.")]
        public required string Summary { get; set; }
        
        [Required(ErrorMessage = "El estado de la orden es obligatiorio")]
        public OrderStatus Status { get; set; }

        [Required(ErrorMessage = "El ID del vehículo es obligatorio.")]
        public int VehicleId { get; set; }
    }
}
