using System.Net.Http.Json;
using HotelMotorShared.Dtos.UserDTOs;

namespace HotelMotorWeb.Services.Auth
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AuthResponseDto?> Register(RegisterDto registerDto)
        {
            var response = await _http.PostAsJsonAsync("auth/register", registerDto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ResponseWrapper<AuthResponseDto>>();
                return result?.Data;
            }

            return null;
        }

        public async Task<AuthResponseDto?> Login(LoginDto loginDto)
        {
            var response = await _http.PostAsJsonAsync("auth/login", loginDto);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ResponseWrapper<AuthResponseDto>>();
                return result?.Data;
            }

            return null;
        }
    }

    // Clase para envolver la respuesta del backend
    public class ResponseWrapper<T>
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }
    }
}
