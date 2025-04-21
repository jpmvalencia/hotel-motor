using HotelMotorShared.Models;

namespace HotelMotorApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
    }
}
