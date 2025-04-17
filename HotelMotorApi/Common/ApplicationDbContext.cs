using Microsoft.EntityFrameworkCore;

namespace HotelMotorApi.Common
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
