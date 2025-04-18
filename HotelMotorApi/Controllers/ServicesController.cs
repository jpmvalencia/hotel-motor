using AutoMapper;
using HotelMotorApi.Interfaces;
using HotelMotorShared.DTOs.ServiceDTOs;
using HotelMotorShared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelMotorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _mapper;
        public ServicesController(IServiceService serviceService, IMapper mapper)
        {
            _serviceService = serviceService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
            var services = await _serviceService.GetServicesAsync();
            return Ok(new
            {
                status = 200,
                message = "Servicios encontrados",
                data = services
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetServiceById(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null)
            {
                return NotFound(new { status = 404, message = "Servicio no encontrado" });
            }
            return Ok(new
            {
                status = 200,
                message = "Servicio encontrado",
                data = service
            });
        }
        [HttpPost]
        public async Task<IActionResult> AddService([FromBody] AddServiceDto addServiceDto)
        {
            try
            {
                var service = await _serviceService.AddServiceAsync(addServiceDto);
                var serviceDto = _mapper.Map<ServiceDto>(service);
                return CreatedAtAction(nameof(GetServiceById), new { id = service.Id }, new
                {
                    status = 201,
                    message = "Servicio creado",
                    data = serviceDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 400, message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateServiceDto updateServiceDto)
        {
            try
            {
                var service = await _serviceService.UpdateServiceAsync(id, updateServiceDto);
                if (service == null)
                {
                    return NotFound(new { status = 404, message = "Servicio no encontrado" });
                }
                var serviceDto = _mapper.Map<ServiceDto>(service);
                return Ok(new
                {
                    status = 200,
                    message = "Servicio actualizado",
                    data = serviceDto
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 400, message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            try
            {
                var result = await _serviceService.DeleteServiceAsync(id);
                if (!result)
                {
                    return NotFound(new { status = 404, message = "Servicio no encontrado" });
                }
                return Ok(new { status = 200, message = "Servicio eliminado" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = 400, message = ex.Message });
            }
        }
    }
}
