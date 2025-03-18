using HotelMotor.Modules.Services.Models;
using Microsoft.AspNetCore.Components;

namespace HotelMotor.Modules.Services.Components
{
    public partial class ServiceDropdown
    {
        [Inject]
        private ServiceService ServiceService { get; set; }

        private List<Service> _services = new List<Service>();

        protected override void OnInitialized()
        {
            _services = ServiceService.GetServices();
        }
    }
}
