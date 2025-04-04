using ECommerce.Models.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Dtos
{
    public class CustomerDetailsDto
    {
        public int Id { get; set; } //Primary Key
        [Required, MaxLength(100)]
        public string CustomerName { get; set; } = null!;
        [Required, MaxLength(100)]
        public string Email { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public int MembershipTierId { get; set; } // Foreign key to MembershipTier
        [ForeignKey("MembershipTierId")]
        public MembershipTier? MembershipTier { get; set; }
        public List<Address>? Addresses { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
