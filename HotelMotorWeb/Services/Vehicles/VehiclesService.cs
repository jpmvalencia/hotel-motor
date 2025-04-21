using System.Net.Http.Json;
using HotelMotorShared.Dtos;
using HotelMotorShared.Dtos.OrderDTOs;
using HotelMotorShared.Dtos.CustomerDTOs;
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

        public async Task<List<VehicleDTO>> GetVehiclesByCustomerSearchTermAsync(string searchTerm)
        {
            try
            {
                var response = await _httpClient.GetAsync($"customers/search?term={searchTerm}");

                if (!response.IsSuccessStatusCode)
                    return new List<VehicleDTO>();

                var customerResponse = await response.Content.ReadFromJsonAsync<ApiResponse<CustomerDto>>();
                if (customerResponse?.Data == null)
                    return new List<VehicleDTO>();

                int customerId = customerResponse.Data.Id;

                var vehicleResponse = await _httpClient.GetFromJsonAsync<ApiResponse<List<VehicleDTO>>>($"vehicles/by-customer/{customerId}");
                return vehicleResponse?.Data ?? new List<VehicleDTO>();
            }
            catch
            {
                return new List<VehicleDTO>();
            }
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
