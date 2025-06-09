using HotelMotorShared.Dtos.UserDTOs;

namespace HotelMotorApi.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        Task<Boolean> AlternateAdminAsync(string email);
    }
}
