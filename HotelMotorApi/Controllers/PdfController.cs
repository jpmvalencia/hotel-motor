using HotelMotorApi.Interfaces;
using HotelMotorShared.Dtos.OrderDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelMotorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IPdfService _pdfService;

        public PdfController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpPost("generate-pdf")]
        public IActionResult GeneratePdf([FromBody] List<OrderDTO> orders)
        {
            var pdfBytes = _pdfService.GeneratePdf(orders);
            if (pdfBytes == null || pdfBytes.Length == 0)
            {
                return BadRequest("No se pudo generar el PDF.");
            }
            return File(pdfBytes, "application/pdf", "generated.pdf");
        }
    }
}
