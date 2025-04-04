using ECommerce.Data;
using ECommerce.Models.Models;
using ECommerce.Services.Interfaces;
using ECommerce.Utils.Filters;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Services
{
    public class OrdersService(ECommerceDbContext dbContext):IOrder
    {
        private readonly ECommerceDbContext _dbContext = dbContext;
        public async Task CreateOrderAsync(Order order)
        {
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrdersByCustomerIdASync(int customerId)
        {
            var existingOrder = _dbContext.Orders.FirstOrDefault(o => o.CustomerId == customerId);
            _dbContext.Orders.Remove(existingOrder!);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderByOrderIdASync(int id)
        {
            var existingOrder = _dbContext.Orders.Find(id);
            try
            {
                _dbContext.Orders.Remove(existingOrder!);
                // update this order's tracking details
                existingOrder!.TrackingDetail = null;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Order?> GetOrderByCustomerIdAsync(int customerId)
        {
            return await _dbContext.Orders.
                Include(o => o.OrderItems!).
                    ThenInclude(oi => oi.Product).
                        Include(o => o.Customer).
                            FirstOrDefaultAsync(o => o.CustomerId == customerId);
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _dbContext.Orders.
                Include(o => o.OrderItems!).
                    ThenInclude(oi => oi.Product).
                        Include(o => o.Customer).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateOrderByCustomerIdAsync(int customerId, Order order)
        {
            var existingOrder = _dbContext.Orders.FirstOrDefault(o => o.CustomerId == customerId);
            if (existingOrder != null)
            {
                _dbContext.Orders.Entry(existingOrder).CurrentValues.SetValues(order);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateOrderShippingDateByOrderIdAsync(int id, DateTime shippedDate)
        {
            var existingOrder = await _dbContext.Orders.FindAsync(id);
            existingOrder!.ShippedDate = shippedDate;
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateOrderStatusByOrderIdAsync(int id, string status)
        {
            var existingOrder = await _dbContext.Orders.FindAsync(id);
            existingOrder!.Status = status;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateOrderByOrderIdASync(UpdateOrderFilter filter)
        {
            var existingOrder = await _dbContext.Orders.FindAsync(filter.Id);
            if (filter != null)
            {
                if (filter.Amount != null) existingOrder!.Amount = (decimal)filter.Amount;
                if (filter.OrderDiscount != null) existingOrder!.OrderDiscount = (decimal)filter.OrderDiscount;
                if (filter.DeliveryCharge != null) existingOrder!.DeliveryCharge = (decimal)filter.DeliveryCharge;
                if (filter.TotalAmount != null) existingOrder!.TotalAmount = (decimal)filter.TotalAmount;
                if (!string.IsNullOrEmpty(filter.Status)) existingOrder!.Status = filter.Status!;
                if (filter.ShippedDate != null) existingOrder!.ShippedDate = filter.ShippedDate;
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}

