using HotelMotorApi.Common;
using HotelMotorApi.Interfaces;
using HotelMotorShared.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelMotorApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Vehicle)
                .Include(os => os.OrderDetails)
                .ThenInclude(os => os.Service)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Vehicle)
                .Include(os => os.OrderDetails)
                .ThenInclude(os => os.Service)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }
        public async Task<Order?> UpdateAsync(Order order)
        {
            var existingOrder = await GetByIdAsync(order.Id);
            if (existingOrder == null)
                return null;

            existingOrder.Summary = order.Summary;
            existingOrder.Status = order.Status;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.Vehicle = order.Vehicle;

            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync();
            return existingOrder;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await GetByIdAsync(id);
            if (order == null)
            {
                return false;
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(c => c.Id == id);

        }

    }
}
