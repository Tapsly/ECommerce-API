using ECommerce.Data;
using ECommerce.Models.Models;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ECommerce.Services.Services
{
    public class OrderItemService(ECommerceDbContext dbContext, ILogger<OrderItemService> logger) : IOrderItem
    {
        private readonly ECommerceDbContext _dbContext = dbContext;
        private readonly ILogger<OrderItemService> _logger = logger;
        public async Task CreateOrderItemAsync(OrderItem newOrderItem)
        {
            await _dbContext.OrderItems.AddAsync(newOrderItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderItemByIdAsync(int orderId, int itemId)
        {
            var existingOrder = _dbContext.OrderItems.FirstOrDefault(oi =>
                oi.OrderId == orderId && oi.Id == itemId);
            _dbContext.OrderItems.Remove(existingOrder!);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderItemsByOrderIdAsync(int orderId)
        {
            await _dbContext.OrderItems.Where(oi => oi.OrderId == orderId).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _dbContext.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync();
        }

        public async Task<List<OrderItem>> GetOrderItemsByProductIdAsync(int productId)
        {
            return await _dbContext.OrderItems.Where(oi => oi.ProductId == productId).ToListAsync();
        }

        public async Task<bool> HasOrderItems()
        {
            return await _dbContext.OrderItems.AnyAsync();
        }

        public async Task<bool> OrderHasOrderItems(int orderId)
        {
            return await _dbContext.OrderItems.AnyAsync(oi => oi.OrderId.Equals(orderId));
        }

        public async Task<OrderItem?> UpdateOrderItemByIdAsync(int id, OrderItem updatedOrderItem)
        {
            var existingOrderItem = await _dbContext.OrderItems.FindAsync(id);
            if (existingOrderItem is null)
            {
                _logger.LogInformation($"The orderItem with id:{id} does not exist");
                return null;
            }
            try
            {
                existingOrderItem!.Quantity = updatedOrderItem.Quantity;
                existingOrderItem!.Discount = updatedOrderItem.Discount;
                existingOrderItem!.TotalPrice = updatedOrderItem.TotalPrice;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
            return updatedOrderItem;
        }
    }
}
