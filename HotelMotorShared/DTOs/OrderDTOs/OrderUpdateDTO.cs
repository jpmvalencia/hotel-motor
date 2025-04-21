using System.ComponentModel.DataAnnotations;
using HotelMotorShared.Models;

namespace HotelMotorShared.Dtos.OrderDTOs
{
    public class OrderUpdateDTO
    {
        [Required(ErrorMessage = "El resumen de la orden es obligatorio.")]
        public string? Summary { get; set; }

        [Required(ErrorMessage = "El estado de la orden es obligatorio")]
        public OrderStatus? Status { get; set; }

        [Required(ErrorMessage = "La fecha de vencimiento de la orden es obligatoria")]
        public DateTime DueDate { get; set; }
    }
}
