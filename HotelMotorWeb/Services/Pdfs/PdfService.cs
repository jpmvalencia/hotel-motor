using System.Net.Http.Json;
using HotelMotorShared.Dtos.OrderDTOs;

namespace HotelMotorWeb.Services.Pdfs
{
    public class PdfService
    {
        private readonly HttpClient _httpClient;

        public PdfService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> GetPdfAsync(IEnumerable<OrderDTO> orders)
        {
            var response = await _httpClient.PostAsJsonAsync("pdf/generate-pdf", orders);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
