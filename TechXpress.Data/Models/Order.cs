using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending";
        public ApplicationUser? User { get; set; }
        [Required(ErrorMessage = "Order Must Have A Date")]
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "Order Must Have A Total Price")]
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
