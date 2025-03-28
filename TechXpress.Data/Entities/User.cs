using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.Data.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required(ErrorMessage = "You Must Provide A Name"), RegularExpression(@"^[A-Za-z][A-Za-z\s]*$", ErrorMessage = "Numbers and Special Characters Are Not Allowed")]
        [StringLength(50)]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Please Enter A Valid Email Address")]
        public string Email { get; set; }
    }
}
