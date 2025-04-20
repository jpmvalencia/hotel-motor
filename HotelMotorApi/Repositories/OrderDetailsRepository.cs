using HotelMotorApi.Common;
using HotelMotorApi.Interfaces;
using HotelMotorShared.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelMotorApi.Repositories
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderDetails>> GetServicesByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails
                .Where(x => x.OrderId == orderId)
                .Include(s => s.Service)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderDetails>> AssignServicesToOrderAsync(int orderId, List<int> servicesIds)
        {
            var existingServiceIds = await _context.OrderDetails
                .Where(od => od.OrderId == orderId && servicesIds.Contains(od.ServiceId))
                .Select(od => od.ServiceId)
                .ToListAsync();

            var newServiceIds = servicesIds.Except(existingServiceIds);

            var newOrderDetails = newServiceIds.Select(serviceId => new OrderDetails
            {
                OrderId = orderId,
                ServiceId = serviceId
            }).ToList();

            if (newOrderDetails.Count > 0)
            {
                await _context.OrderDetails.AddRangeAsync(newOrderDetails);
                await _context.SaveChangesAsync();
            }

            return await _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .Include(od => od.Service)
                .ToListAsync();
        }


        public async Task<bool> RemoveServiceFromOrderAsync(int orderId, int serviceId)
        {
            var orderDetail = await _context.OrderDetails
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.ServiceId == serviceId);

            if (orderDetail == null)
                return false;

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> IsServiceAssignedToOrderAsync(int orderId, int serviceId)
        {
            return await _context.OrderDetails.AnyAsync(od => od.OrderId == orderId && od.ServiceId == serviceId);
        }
    }
}
