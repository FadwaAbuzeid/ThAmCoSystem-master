using Microsoft.EntityFrameworkCore;
using ThreeAmigosWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreeAmigosWebApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetOrdersToDispatchAsync()
        {
            return await _context.Orders
                .Where(o => !o.IsDispatched)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task DeleteOrdersByUserIdAsync(string userId)
        {
            var ordersToDelete = await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();

            if (ordersToDelete.Any())
            {
                _context.Orders.RemoveRange(ordersToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
