using System.ComponentModel.DataAnnotations;

namespace HotelMotor.Modules.Vehicles.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El tipo de vehículo es obligatorio.")]
        public VehicleType VehicleType { get; set; }
        [Required(ErrorMessage = "La marca es obligatoria.")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "El modelo es obligatorio.")]
        public string Model { get; set; }
        [Required(ErrorMessage = "El año es obligatorio.")]
        [Range(1885, 2026, ErrorMessage = "El año debe estar entre 1885 y 2026.")]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "El año debe ser un número de cuatro dígitos.")]
        public int Year { get; set; }
        [Required(ErrorMessage = "El número de matrícula es obligatorio.")]
        [RegularExpression(@"^(?:[A-Z]{3}[0-9]{3}|[A-Z]{3}[0-9]{2}[A-Z])$", ErrorMessage = "El número de matrícula es inválido.")]
        public string PlateNumber { get; set; }
    }
}
