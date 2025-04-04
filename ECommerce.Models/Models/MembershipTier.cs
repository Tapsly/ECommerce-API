using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.Models
{
    public class MembershipTier
    {
        [Key]
        public int Id { get; set; } //Primary Key
        [Required, MaxLength(50)]
        public string TierName { get; set; } = null!; // e.g., Gold, Silver, Bronze
        [Column(TypeName = "decimal(5,2)")]
        public decimal DiscountPercentage { get; set; } // Discount percentage for this tier
    }
}
