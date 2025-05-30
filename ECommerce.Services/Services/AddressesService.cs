﻿using ECommerce.Data;
using ECommerce.Models.Models;
using ECommerce.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Services.Services
{
    public class AddressesService(ECommerceDbContext dbContext) : IAddress
    {
        private readonly ECommerceDbContext _dbContext = dbContext;
        public async Task DeleteAddressByCustomerAndAddressIdAsync(int customerId, int addressId)
        {
            var existingAddress = _dbContext.Addresses.FirstOrDefault(add =>
               add.CustomerId == customerId && add.Id == addressId);
            try
            {
                _dbContext.Addresses.Remove(existingAddress!);
                // update the customer address and address id
                /* _dbContext.Customers.Where(c => c.Id == customerId).ExecuteUpdateAsync(
                     setters => setters.SetProperty(c => c.Addresses!.Where(add => add.Id == addressId), null));*/
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

        public async Task<Address?> UpdateAddressByCustomerIdAndAddressIdAsync(int customerId, int addressId, Address updatedAddress)
        {
            var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(add =>
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
            return updatedAddress;
        }
    }
}
