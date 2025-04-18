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

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            return await _context.Customers
                .Include(c => c.Vehicles)
                .FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());
        }

        public async Task<Customer> CreateAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }
        public async Task<Customer?> UpdateAsync(Customer customer)
        {
            var existingCustomer = await GetByIdAsync(customer.Id);
            if (existingCustomer == null)
                return null;

            existingCustomer.Name = customer.Name;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Address = customer.Address;

            _context.Customers.Update(existingCustomer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await GetByIdAsync(id);
            if (customer == null)
                return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Customers.AnyAsync(c => c.Id == id);

        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Customers.AnyAsync(c => c.Email.ToLower() == email.ToLower());
        }
    }
}
