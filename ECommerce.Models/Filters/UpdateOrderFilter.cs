using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.Filters
{
    public class UpdateOrderFilter
    {
        public int Id { get; set; }
        public decimal? Amount { get; set; }
        public decimal? OrderDiscount { get; set; }
        public decimal? DeliveryCharge { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Status { get; set; }
        public DateTime? ShippedDate { get; set; }
    }
}

