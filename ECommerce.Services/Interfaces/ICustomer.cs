using ECommerce.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Services.Interfaces
{
    public interface ICustomer
    {
        public Task<Customer?> GetCustomerByIdAsync(int id);
        public Task<bool> CheckCustomerByEmailAsync(string email);
        public Task<Customer?> GetCustomerByEmailAsync(string email);
        public Task<Customer?> GetCustomerByEmailAndPasswordAsync(string email, string password);
        public Task<List<Customer>> GetAllCustomersAsync();
        public Task CreateCustomerAsync(Customer customer);
        public Task UpdateCustomerByIdAsync(int id, Customer updatedCustomer);
        public Task UpdateCustomerMembershipTier(int customerId, MembershipTier membershipTier);
        public Task DeleteCustomerByIdAsync(int id);
        public Task DeleteCustomerByEmailAsync(string email);
    }
}
