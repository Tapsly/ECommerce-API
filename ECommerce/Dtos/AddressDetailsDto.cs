using ECommerce.Models.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class AddressDetailsDto
    {
        [Required, MaxLength(200)]
        public string Street { get; set; } = null!;
        [Required, MaxLength(100)]
        public string City { get; set; } = null!;
        [Required, MaxLength(10)]
        public string ZipCode { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
    }
}
