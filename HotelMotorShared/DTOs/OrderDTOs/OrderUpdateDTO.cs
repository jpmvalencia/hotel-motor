using System.ComponentModel.DataAnnotations;
using HotelMotorShared.Models;

namespace HotelMotorShared.Dtos.OrderDTOs
{
    public class OrderUpdateDTO
    {
        public string? Summary { get; set; }

        public OrderStatus? Status { get; set; }

        [Range(1, 365, ErrorMessage = "El número de días debe estar entre 1 y 365.")]
        public int? DueDateDays { get; set; }
    }
}
