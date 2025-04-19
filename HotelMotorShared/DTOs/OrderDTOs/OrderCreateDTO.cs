using System.ComponentModel.DataAnnotations;

namespace HotelMotorShared.Dtos.OrderDTOs

{
    public class OrderCreateDTO
    {
        [Required(ErrorMessage = "El resumen de la orden es obligatorio.")]
        public required string Summary { get; set; }
        [Required(ErrorMessage = "El ID del vehículo es obligatorio.")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "El número de días para la fecha de vencimiento es obligatorio.")]
        [Range(1, 365, ErrorMessage = "El número de días debe estar entre 1 y 365.")]
        public int DueDateDays { get; set; }
    }
}
