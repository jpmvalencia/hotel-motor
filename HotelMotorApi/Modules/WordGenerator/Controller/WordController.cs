using HotelMotorApi.Modules.WordGenerator.Interfaces;
using HotelMotorShared.DTOs.ServiceDTOs;
using Microsoft.AspNetCore.Mvc;

namespace HotelMotorApi.Modules.WordGenerator.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IWordService _wordService;

        public WordController(IWordService wordService)
        {
            _wordService = wordService;
        }

        [HttpPost("generate-word")]
        public async Task<IActionResult> GenerateServicesWordFile([FromBody] List<ServiceDto> services)
        {
            if (services == null || !services.Any())
                return BadRequest("No se proporcionaron servicios.");

            var fileBytes = await _wordService.GenerateFileWithServices(services);

            if (fileBytes == null || fileBytes.Length == 0)
                return StatusCode(500, "No se pudo generar el archivo Word.");

            return File(
                fileBytes,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                "services.docx"
            );
        }
    }
}
