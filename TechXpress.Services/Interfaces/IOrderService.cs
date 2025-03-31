using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Data.Models;

namespace TechXpress.Services.Interfaces
{
    public interface IOrderService
    {


        Task CreateOrderAsync(string userId, string paymentToken);
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userId);



    }
}
