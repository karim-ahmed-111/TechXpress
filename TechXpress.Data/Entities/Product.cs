using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.Data.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0.50, double.MaxValue)]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
