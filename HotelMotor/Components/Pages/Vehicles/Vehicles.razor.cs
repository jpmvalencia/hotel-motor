using HotelMotor.Modules.Vehicles;
using HotelMotor.Modules.Vehicles.Models;
using Microsoft.AspNetCore.Components;

namespace HotelMotor.Components.Pages.Vehicles
{
    public partial class Vehicles
    {
        [Inject]
        private VehicleService VehicleService { get; set; }

        private List<Vehicle> vehicles = new List<Vehicle>();

        protected override void OnInitialized()
        {
            GetAllVehicles();
        }

        private List<Vehicle> GetAllVehicles()
        {
            return vehicles = VehicleService.GetVehicles();
        }

        private string GetVehicleImage(VehicleType vehicleType)
        {
            return vehicleType switch
            {
                VehicleType.Motorcycle => "images/vehicles/motos.jpg",
                VehicleType.Car => "images/vehicles/carros.jpg", 
                _ => "images/vehicles/motoycarro.jpg"
            };
        }
    }
}
