using ECommerce.Models.Models;

namespace ECommerce.Services.Interfaces
{
    public interface IAddress
    {
        public Task<List<Address>> GetAddressesByCustomerIdAsync(int customerId);
        public Task<Address?> GetAddressByCustomerIdAndAddressIdAsync(int customerId, int addressId);
        public Task UpdateAddressByCustomerIdAndAddressIdAsync(int customerId, int addressId, Address updatedAddress);
        public Task DeleteAddressesByCustomerIdAsync(int customerId);
        public Task DeleteAddressByCustomerAndAddressIdAsync(int customerId, int addressId);
    }
}
