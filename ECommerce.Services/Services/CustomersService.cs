using ECommerce.Data;
using ECommerce.Models.Models;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Services
{
    public class CustomersService(ECommerceDbContext dbContext) : ICustomer
    {
        private readonly ECommerceDbContext _dbContext = dbContext;

        public async Task<bool> CheckCustomerByEmailAsync(string email)
        {
            try
            {
                return await _dbContext.Customers.AnyAsync(c => c.Email == email);
            }
            catch (ArgumentNullException)
            {

                throw;
            }
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    _dbContext.Customers.Add(customer);
                    await _dbContext.SaveChangesAsync();
                }

            }
            catch (ArgumentNullException)
            {

                throw;
            }
        }

        public async Task DeleteCustomerByEmailAsync(string email)
        {
            var existingCustomer = _dbContext.Customers.FirstOrDefault(c => c.Email == email);
            try
            {
                if (existingCustomer != null)
                {
                    _dbContext.Customers.Remove(existingCustomer);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteCustomerByIdAsync(int id)
        {
            var existingCustomer = _dbContext.Customers.Find(id);
            try
            {
                if (existingCustomer != null)
                {
                    _dbContext.Customers.Remove(existingCustomer);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // include the customers' membership, address, and orders
        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _dbContext.Customers
                    .Include(c => c.MembershipTier)
                        .Include(c => c.Addresses)
                            .Include(c => c.Orders)
                                .AsNoTracking()
                                    .ToListAsync();
            }
            catch (ArgumentNullException)
            {

                throw;
            }
        }

        public async Task<Customer?> GetCustomerByEmailAndPasswordAsync(string email, string password)
        {
            try
            {
                return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
            }
            catch (ArgumentNullException)
            {

                throw;
            }
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            try
            {
                return await _dbContext.Customers.AsNoTracking().
                    Include(c => c.MembershipTier).
                       Include(c => c.Addresses).
                           Include(c => c.Orders).FirstOrDefaultAsync(c => c.Email == email);
            }
            catch (ArgumentNullException)
            {

                throw;
            }
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Customers.AsNoTracking().
                     Include(c => c.MembershipTier).
                        Include(c => c.Addresses).
                            Include(c => c.Orders).FirstOrDefaultAsync(o => o.Id == id);
            }
            catch (ArgumentNullException)
            {

                throw;
            }
        }

        public async Task UpdateCustomerByIdAsync(int id, Customer updatedCustomer)
        {
            var existingCustomer = await _dbContext.Customers.FindAsync(id);
            try
            {
                if (existingCustomer != null)
                {
                    _dbContext.Entry(existingCustomer).CurrentValues.SetValues(updatedCustomer);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (DbUpdateException)
            {

                throw;
            }
        }

        public async Task UpdateCustomerMembershipTier(int customerId, MembershipTier membershipTier)
        {
            var existingCustomer = _dbContext.Customers.Find(customerId);
            try
            {
                if (existingCustomer != null)
                {
                    existingCustomer!.MembershipTierId = membershipTier.Id;
                    existingCustomer!.MembershipTier = membershipTier;
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (DbUpdateException)
            {

                throw;
            }

        }
    }
}

