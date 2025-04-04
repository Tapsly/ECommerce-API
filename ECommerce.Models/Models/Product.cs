using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Configuration.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } // Primary Key
        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountPercentage { get; set; } //Product Level Discount
        [MaxLength(500)]
        public string? Description { get; set; }
        [MaxLength(50)]
        public string SKU { get; set; } = null!; // Stock Keeping Unit
        [Required]
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; } // Foreign Key
        [ForeignKey("CategoryId")]
        [Ignore]
        public Category Category { get; set; } = null!;
    }
}
