using System.ComponentModel.DataAnnotations;

namespace HotelMotorShared.Dtos.OrderDTOs

{
    public class OrderCreateDTO
    {
        [Required(ErrorMessage = "El resumen de la orden es obligatorio.")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "El ID del vehículo es obligatorio.")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento de la orden es obligatoria")]
        public DateTime DueDate { get; set; }
    }
}
