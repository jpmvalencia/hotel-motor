using HotelMotor.Modules.Vehicles;
using HotelMotor.Modules.Vehicles.Models;
using Microsoft.AspNetCore.Components;

namespace HotelMotor.Components.Pages.Vehicles
{
    public partial class VehiclesRegistration
    {
        [Inject]
        private VehicleService VehicleService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private Vehicle newVehicle = new Vehicle();

        private void HandleValidSubmit()
        {
            VehicleService.AddVehicle(newVehicle);
            NavigationManager.NavigateTo("/");
        }
    }
}
