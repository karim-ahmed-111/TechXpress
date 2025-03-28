using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.Data.Entities
{
   public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required,StringLength(50),RegularExpression("/^[a-zA-Z]+$/")]
        public string Name { get; set; }

        public ICollection<Product> products { get; set; }
    }
}
