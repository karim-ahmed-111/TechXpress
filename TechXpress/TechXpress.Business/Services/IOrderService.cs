using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Business.DTOs;

namespace TechXpress.Business.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<IEnumerable<OrderDto>> GetOrdersForUserAsync(string userId);
        Task<OrderDto> GetByIdAsync(int id);
        Task CreateOrderAsync(CreateOrderDto orderDto);
        Task UpdateOrderStatusAsync(int orderId, string status);
    }
}
