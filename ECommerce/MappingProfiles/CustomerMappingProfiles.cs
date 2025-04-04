using ECommerce.Dtos;
using ECommerce.Models;
using ECommerce.Models.Models;
namespace ECommerce.MappingProfiles
{
    public class CustomerMappingProfiles
    {
        public static Customer MapToCustomer(CustomerCreateDto customerRegistrationDTO)
        {
            var newCustomer = new Customer
            {
                FirstName = customerRegistrationDTO.Name,
                LastName = customerRegistrationDTO.LastName,
                Email = customerRegistrationDTO.Email,
                PhoneNumber = customerRegistrationDTO.PhoneNumber,
            };

            return newCustomer;
        }
        public static CustomerDto MapToCustomerDTO(Customer customer)
        {
            var customerDTO = new CustomerDto
            {
                CustomerName = $"{customer.FirstName} + {customer.LastName}",
                Email = customer.Email,
            };

            return customerDTO;
        }
        public static CustomerDetailsDto MapToCustomerDetailsDTO(Customer customer)
        {
            var customerDetailsDTO = new CustomerDetailsDto
            {
                CustomerName = $"{customer.FirstName} + {customer.LastName}",
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                IsActive = customer.IsActive,
                MembershipTier = customer.MembershipTier,
                Addresses = customer.Addresses,
                Orders = customer.Orders
            };
            return customerDetailsDTO;
        }
        public static CustomerLoginDto MapToLoginDTO(Customer customer)
        {
            var loginDTO = new CustomerLoginDto
            {
                Email = customer.Email,
                Password = customer.Password,
            };
            return loginDTO;
        }
        public static Customer MapToCustomer(CustomerLoginDto customerLoginDTO)
        {
            var loginCustomer = new Customer
            {
                Email = customerLoginDTO.Email,
                Password = customerLoginDTO.Password,
            };

            return loginCustomer;
        }
    }
}
