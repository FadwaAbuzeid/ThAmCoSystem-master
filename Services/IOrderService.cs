using ThreeAmigosWebApp.Models;

public interface IOrderService
{
    Task<List<Order>> GetOrdersToDispatchAsync();
    Task<Order> GetOrderByIdAsync(int orderId);
    Task UpdateOrderAsync(Order order);
    Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    Task DeleteOrdersByUserIdAsync(string userId);
}

