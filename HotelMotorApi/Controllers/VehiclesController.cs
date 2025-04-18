using HotelMotorApi.Interfaces;
using HotelMotorApi.Services;
using HotelMotorShared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelMotorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehiclesService _vehiclesService;

        public VehiclesController(IVehiclesService vehiclesService)
        {
            _vehiclesService = vehiclesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var vehicles = await _vehiclesService.GetVehiclesAsync();
            return Ok(new
            {
                status=200,
                message="Vehículos encontrados",
                data=vehicles
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicleById(int id)
        {
            var vehicle = await _vehiclesService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound(new { status=404, message = "Vehículo no encontrado" });
            }
            return Ok(new
            {
                status=200,
                message="Vehículo encontrado",
                data=vehicle
            });
        }
    }
}
