using ECommerce.Data;
using ECommerce.Models.Models;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Services
{
    public class OrderItemService(ECommerceDbContext dbContext):IOrderItem
    {
        private readonly ECommerceDbContext _dbContext = dbContext;

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

        public async Task UpdateOrderItemByIdAsync(int id, OrderItem updatedOrderItem)
        {
            var existingOrderItem = _dbContext.OrderItems.Find(id);
            existingOrderItem!.Quantity = updatedOrderItem.Quantity;
            existingOrderItem!.Discount = updatedOrderItem.Discount;
            existingOrderItem!.TotalPrice = updatedOrderItem.TotalPrice;
            await _dbContext.SaveChangesAsync();
        }
    }
}
