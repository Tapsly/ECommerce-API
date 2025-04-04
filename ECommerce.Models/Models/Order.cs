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
    public class Order
    {
        [Key]
        public int Id { get; set; } //Primary Key
        [Required]
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; } //Base Amount
        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderDiscount { get; set; } //Order Level Discount
        [Column(TypeName = "decimal(18,2)")]
        public decimal DeliveryCharge { get; set; } //Delivery Charge
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } //Total Amount After Apply Discount and Delivery Charge
        [MaxLength(20)]
        public string Status { get; set; } = null!;
        public DateTime? ShippedDate { get; set; }
        [Required]
        public int? CustomerId { get; set; } //Foreign Key
        [ForeignKey("CustomerId")]
        [Ignore]
        public Customer Customer { get; set; } = null!;
        public int? ShippingAddressId { get; set; } //Foreign Key
        [ForeignKey("ShippingAddressId")]
        [Ignore]
        public Address? ShippingAddress { get; set; }
        [Ignore]
        public List<OrderItem>? OrderItems { get; set; }
        [Ignore]
        public TrackingDetail? TrackingDetail { get; set; }
    }
}
