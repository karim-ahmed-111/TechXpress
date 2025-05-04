using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechXpress.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        // Navigation: if needed, list of orders or products by this user (seller)
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
