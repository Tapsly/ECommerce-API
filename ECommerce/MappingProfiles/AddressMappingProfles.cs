using ECommerce.Dtos;
using ECommerce.Models.Models;

namespace ECommerce.MappingProfiles
{
    public class AddressMappingProfles
    {
        public static Address MapToAddress(AddressCreateDto addressCreateDTO)
        {
            return new Address
            {
                Street = addressCreateDTO.Street,
                City = addressCreateDTO.City,
                ZipCode = addressCreateDTO.ZipCode,
            };
        }

        public static AddressDto MapToAddressDTO(Address address)
        {
            return new AddressDto
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                CustomerId = address.CustomerId,
            };
        }

        public static AddressDetailsDto MapToAddressDetailsDTO(Address address)
        {
            return new AddressDetailsDto
            {
                Street = address.Street!,
                City = address.City!,
                ZipCode = address.ZipCode!,
                Customer = address.Customer,
            };
        }
    }
}
