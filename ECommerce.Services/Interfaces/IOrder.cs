using ECommerce.Models.Filters;
using ECommerce.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Interfaces
{
    public interface IOrder
    {
        public Task CreateOrderAsync(Order order);
        public Task<Order?> GetOrderByIdAsync(int id);
        public Task<Order?> GetOrderByCustomerIdAsync(int customerId);
        public Task UpdateOrderByOrderIdASync(UpdateOrderFilter filter);
        public Task UpdateOrderByCustomerIdAsync(int customerId, Order order);
        public Task UpdateOrderStatusByOrderIdAsync(int id, string status);
        public Task UpdateOrderShippingDateByOrderIdAsync(int id, DateTime ShippedDate);
        public Task DeleteOrderByOrderIdASync(int id);
        public Task DeleteOrdersByCustomerIdASync(int customerId);
    }
}
