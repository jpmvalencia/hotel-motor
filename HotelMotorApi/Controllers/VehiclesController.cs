using AutoMapper;
using HotelMotorApi.Interfaces;
using HotelMotorApi.Services;
using HotelMotorShared.Dtos;
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
        private readonly IMapper _mapper;
        public VehiclesController(IVehiclesService vehiclesService, IMapper mapper)
        {
            _vehiclesService = vehiclesService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
        {
            var vehicles = await _vehiclesService.GetVehiclesAsync();
            return Ok(new
            {
                status = 200,
                message = "Vehículos encontrados",
                data = vehicles
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicleById(int id)
        {
            var vehicle = await _vehiclesService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound(new { status = 404, message = "Vehículo no encontrado" });
            }
            return Ok(new
            {
                status = 200,
                message = "Vehículo encontrado",
                data = vehicle
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] AddVehicleDTO addVehicleDto)
        {
            try
            {
                var vehicle = await _vehiclesService.AddVehicleAsync(addVehicleDto);
                var vehicleDto = _mapper.Map<VehicleDTO>(vehicle);
                return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.Id }, new
                {
                    status = 201,
                    message = "Vehículo creado",
                    data = vehicleDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 400, message = "Error al crear el vehículo", error = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] UpdateVehicleDTO updateVehicleDto)
        {
            try
            {
                var updatedVehicle = await _vehiclesService.UpdateVehicleAsync(id, updateVehicleDto);
                var vehicleDto = _mapper.Map<VehicleDTO>(updatedVehicle);

                return Ok(new
                {
                    status = 200,
                    message = "Vehículo actualizado",
                    data = vehicleDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Error al actualizar el vehículo",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            try
            {
                var deletedVehicle = await _vehiclesService.DeleteVehicleAsync(id);
                if (!deletedVehicle)
                {
                    return NotFound(new { status = 404, message = "Vehículo no encontrado" });
                }
                return Ok(new
                {
                    status = 200,
                    message = "Vehículo eliminado",
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    status = 400,
                    message = "Error al eliminar el vehículo",
                    error = ex.Message
                });
            }
        }
    }
}
