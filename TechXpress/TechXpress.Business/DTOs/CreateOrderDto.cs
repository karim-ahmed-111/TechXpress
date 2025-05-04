using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Domain.Entities;

namespace TechXpress.Business.DTOs
{
    public class CreateOrderDto
    {
        public string CustomerId { get; set; }
        public List<CartItemDto> Items { get; set; }
        
    }
}
