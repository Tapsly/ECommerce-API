using ECommerce.Models.Models;
using ECommerce.Services.Interfaces;
using ECommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Services
{
    public class AddressesService(ECommerceDbContext dbContext):IAddress
    {
        private readonly  ECommerceDbContext _dbContext = dbContext;
        public async Task DeleteAddressByCustomerAndAddressIdAsync(int customerId, int addressId)
        {
            var existingAddress = _dbContext.Addresses.FirstOrDefault(add =>
               add.CustomerId == customerId && add.Id == addressId);
            try
            {
                _dbContext.Addresses.Remove(existingAddress!);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteAddressesByCustomerIdAsync(int customerId)
        {
            try
            {
                await _dbContext.Addresses.Where(add => add.CustomerId == customerId).ExecuteDeleteAsync();
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Address?> GetAddressByCustomerIdAndAddressIdAsync(int customerId, int addressId)
        {
            try
            {
                return await _dbContext.Addresses.FirstOrDefaultAsync(add =>
                    add.CustomerId == customerId && add.Id == addressId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Address>> GetAddressesByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _dbContext.Addresses.Where(add => add.CustomerId == customerId).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateAddressByCustomerIdAndAddressIdAsync(int customerId, int addressId, Address updatedAddress)
        {
            var existingAddress = _dbContext.Addresses.FirstOrDefault(add =>
                add.CustomerId == customerId && add.Id == addressId);
            try
            {
                existingAddress!.Street = updatedAddress.Street;
                existingAddress!.ZipCode = updatedAddress.ZipCode;
                existingAddress!.City = updatedAddress.City;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
