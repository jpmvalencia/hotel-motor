using HotelMotor.Modules.Vehicles;
using HotelMotor.Modules.Vehicles.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace HotelMotor.Components.Pages.Vehicles
{
    public partial class VehiclesRegistration
    {
        [Inject]
        private VehicleService VehicleService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        private Vehicle newVehicle = new Vehicle();

        private string errorMessage;

        private void HandleValidSubmit()
        {
            VehicleService.AddVehicle(newVehicle);
            NavigationManager.NavigateTo("/");
        }

        /// <summary>
        /// Handles the file input change event to upload and validate the vehicle image.
        /// Validates the file size and type, and sets an error message if the validation fails.
        /// </summary>
        /// <param name="e">The file change event arguments.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task HandleImageFile(InputFileChangeEventArgs e)
        {
            byte[] imageBytes;

            if (e.File != null)
            {
                if (e.File.Size < 150000)
                {
                    var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                    var fileExtension = Path.GetExtension(e.File.Name).ToLower();

                    if (!supportedTypes.Contains(fileExtension))
                    {
                        errorMessage = "Solo se permiten archivos .jpg, .jpeg, o .png.";
                        return;
                    }
                    using (var stream = new MemoryStream())
                    {
                        await e.File.OpenReadStream().CopyToAsync(stream);
                        imageBytes = stream.ToArray();

                        newVehicle.VehicleImage = imageBytes;
                        errorMessage = null; // Clear any previous error message
                    }
                }
                else
                {
                    errorMessage = "El archivo es demasiado pesado";
                }
            }
        }
    }
}
