using ECommerce.Models.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IOrderItem
    {
        public Task<bool> HasOrderItems();
        public Task<bool> OrderHasOrderItems(int orderId);
        public Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        public Task<List<OrderItem>> GetOrderItemsByProductIdAsync(int productId);
        public Task CreateOrderItemAsync(OrderItem newOrderItem);
        public Task<OrderItem?> UpdateOrderItemByIdAsync(int id, OrderItem OrderItem);
        public Task DeleteOrderItemsByOrderIdAsync(int orderId);
        public Task DeleteOrderItemByIdAsync(int orderId, int itemId);
    }
}
