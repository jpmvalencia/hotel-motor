using System.Net.Http.Json;
using HotelMotorShared.Dtos;
using HotelMotorShared.Dtos.OrderDTOs;
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

        public async Task<bool> UpdateVehicleAsync(int id, UpdateVehicleDTO updateVehicleDto)
        {
            var response = await _httpClient.PatchAsJsonAsync($"vehicles/{id}", updateVehicleDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"vehicles/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByVehicleIdAsync(int vehicleId)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<IEnumerable<OrderDTO>>>($"vehicles/{vehicleId}/orders");
            return response?.Data ?? new List<OrderDTO>();
        }
    }
}
