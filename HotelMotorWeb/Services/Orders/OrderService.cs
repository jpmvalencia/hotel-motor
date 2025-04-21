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
    }
}
