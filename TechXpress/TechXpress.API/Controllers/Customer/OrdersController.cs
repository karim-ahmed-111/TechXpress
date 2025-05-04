// TechXpress.API/Controllers/Customer/OrdersController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechXpress.Business.Services;
using System.Security.Claims;

namespace TechXpress.API.Controllers.Customer
{
    [ApiController]
    [Route("api/customer/[controller]")]
    [Authorize(Roles = "Customer")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrdersController(IOrderService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _service.GetOrdersForUserAsync(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _service.GetByIdAsync(id);
            if (order == null) return NotFound();
            // Ensure user owns the order
            if (order.CustomerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                return Forbid();
            return Ok(order);
        }
    }
}
