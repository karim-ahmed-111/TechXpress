using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.Domain.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        // The customer who made the order
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public ApplicationUser Customer { get; set; }

        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = "Pending"; // e.g. Pending, Paid, Shipped

        // Navigation: order items
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
