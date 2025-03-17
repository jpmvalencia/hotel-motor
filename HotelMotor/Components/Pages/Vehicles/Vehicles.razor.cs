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

        /// <summary>
        /// Initializes the component and retrieves the list of vehicles.
        /// </summary>
        protected override void OnInitialized()
        {

            GetAllVehicles();
        }

        /// <summary>
        /// Retrieves all vehicles from the VehicleService and assigns them to the vehicles list.
        /// </summary>
        /// <returns>A list of vehicles.</returns>
        private List<Vehicle> GetAllVehicles()
        {
            return vehicles = VehicleService.GetVehicles();
        }

        /// <summary>
        /// Converts a byte array representing an image to a base64 string and returns it as a data URL.
        /// If the byte array is null or empty, returns a default image URL based on the vehicle type.
        /// </summary>
        /// <param name="imageBytes">The byte array representing the image.</param>
        /// <param name="vehicleType">The type of the vehicle.</param>
        /// <returns>A string representing the image as a data URL or a default image URL.</returns>
        private string GetVehicleImage(byte[] imageBytes, VehicleType vehicleType)
        {
            if (imageBytes != null && imageBytes.Length > 0)
            {
                var base64Image = Convert.ToBase64String(imageBytes);
                return $"data:image/jpeg;base64,{base64Image}";
            }
            else
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
}
