using ECommerce.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Interfaces
{
    public interface IOrderItem
    {
        public Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId);
        public Task<List<OrderItem>> GetOrderItemsByProductIdAsync(int productId);
        public Task CreateOrderItemAsync(OrderItem newOrderItem);
        public Task UpdateOrderItemByIdAsync(int id, OrderItem OrderItem);
        public Task DeleteOrderItemsByOrderIdAsync(int orderId);
        public Task DeleteOrderItemByIdAsync(int orderId, int itemId);
    }
}
