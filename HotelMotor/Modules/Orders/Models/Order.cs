using HotelMotor.Modules.OrderDetails.Models;
using HotelMotor.Modules.Vehicles.Models;
using System.ComponentModel.DataAnnotations;

namespace HotelMotor.Modules.Orders.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El resumen de los servicios realizados es obligatorio.")]
        public string Summary { get; set; }
        [Required(ErrorMessage = "El estado de la orden es obligatorio.")]
        public OrderStatus OrderStatus { get; set; }
        [Required(ErrorMessage = "La fecha de creación es obligatoria.")]
        public DateTime CreatedTime { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Vehicle Vehicle { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
