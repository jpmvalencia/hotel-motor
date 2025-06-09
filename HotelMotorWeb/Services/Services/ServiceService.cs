using HotelMotorShared.DTOs.ServiceDTOs;
using HotelMotorWeb.Shared;
using System.Net.Http.Json;

namespace HotelMotorWeb.Services.Services
{
    public class ServiceService
    {
        private readonly HttpClient _httpClient;

        public ServiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ServiceDto>> GetServicesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<ServiceDto>>>("services");
            return response?.Data ?? new List<ServiceDto>();
        }

        public async Task<bool> CreateServiceAsync(AddServiceDto addServiceDto)
        {
            var response = await _httpClient.PostAsJsonAsync("services", addServiceDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateServiceAsync(int id, UpdateServiceDto updateServiceDto)
        {
            var response = await _httpClient.PatchAsJsonAsync($"services/{id}", updateServiceDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"services/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
