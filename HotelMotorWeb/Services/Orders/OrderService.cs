using System.Net.Http.Json;
using HotelMotorShared.Dtos.OrderDTOs;

namespace HotelMotorWeb.Services.Orders
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> CreateOrderAsync(OrderCreateDTO orderDto)
        {
            var response = await _httpClient.PostAsJsonAsync("orders", orderDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateOrderAsync(int id, OrderUpdateDTO orderDto)
        {
            var response = await _httpClient.PatchAsJsonAsync($"orders/{id}", orderDto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"orders/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> AssignServicesToOrderAsync(int orderId, List<int> serviceIds)
        {
            var response = await _httpClient.PostAsJsonAsync($"orders/{orderId}/assign-services", serviceIds);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveServiceFromOrderAsync(int orderId, int serviceId)
        {
            // Fix: Replace DeleteFromJsonAsync with DeleteAsync and pass serviceId in the query string
            var response = await _httpClient.DeleteAsync($"orders/{orderId}/delete-service?serviceId={serviceId}");
            return response.IsSuccessStatusCode;
        }
    }
}
