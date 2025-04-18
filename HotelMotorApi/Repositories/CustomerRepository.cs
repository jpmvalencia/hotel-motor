using HotelMotorApi.Common;
using HotelMotorApi.Interfaces;
using HotelMotorShared.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelMotorApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.Vehicles)
                .ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Vehicles)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<Customer> CreateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
