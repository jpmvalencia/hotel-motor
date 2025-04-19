using System.Net.Http.Json;
using HotelMotorShared.Dtos;
using HotelMotorWeb.Shared;

namespace HotelMotorWeb.Services.Vehicles
{
    public class VehiclesService
    {
        private readonly HttpClient _httpClient;

        public VehiclesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<VehicleDTO>> GetVehiclesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<VehicleDTO>>>("vehicles");
            return response?.Data ?? new List<VehicleDTO>();
        }

        public async Task<bool> CreateVehicleAsync(AddVehicleDTO addVehicleDto)
        {
            var response = await _httpClient.PostAsJsonAsync("vehicles", addVehicleDto);
            return response.IsSuccessStatusCode;
        }
    }
}
