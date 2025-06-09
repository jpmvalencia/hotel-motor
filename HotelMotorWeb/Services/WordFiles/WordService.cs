using System.Net.Http.Json;
using HotelMotorShared.DTOs.ServiceDTOs;

namespace HotelMotorWeb.Services.WordFiles
{
    public class WordService
    {
        private readonly HttpClient _httpClient;

        public WordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<byte[]> GetWordWithServicesAsync(IEnumerable<ServiceDto> services)
        {
            var response = await _httpClient.PostAsJsonAsync("word/generate-word", services);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
