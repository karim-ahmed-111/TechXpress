using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.Data.Entities
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailId { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
