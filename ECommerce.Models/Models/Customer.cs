using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; } //Primary Key
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = null!;
        [Required, MaxLength(100)]
        public string LastName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public int? MembershipTierId { get; set; } // Foreign key to MembershipTier
        [ForeignKey("MembershipTierId")]
        [Ignore]
        public MembershipTier? MembershipTier { get; set; }
        public List<Address>? Addresses { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
