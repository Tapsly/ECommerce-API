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
    public class TrackingDetail
    {
        [Key]
        public int Id { get; set; } //Primary Key
        public int OrderId { get; set; } //Foreign Key
        [ForeignKey("OrderId")]
        [Ignore]
        public Order Order { get; set; } = null!;
        [Required]
        public string Carrier { get; set; } = null!;
        public DateTime EstimatedDeliveryDate { get; set; }
        [MaxLength(500)]
        public string TrackingNumber { get; set; } = null!;
    }
}
