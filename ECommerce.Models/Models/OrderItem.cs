using AutoMapper.Configuration.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; } //Primary Key
        [Required]
        public int OrderId { get; set; } //Foreign Key
        [ForeignKey("OrderId")]
        [Ignore]
        public Order Order { get; set; } = null!;
        [Required]
        public int ProductId { get; set; } //Foreign Key
        [ForeignKey("ProductId")]
        [Ignore]
        public Product Product { get; set; } = null!;
        [Required]
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal ProductPrice { get; set; } //Product Price
        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; } // Product Level Discount
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; } //Product Level Total Price after Applying Discount
    }
}
