// TechXpress.API/Controllers/Customer/ProductsController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechXpress.Business.Services;

namespace TechXpress.API.Controllers.Customer
{
    [ApiController]
    [Route("api/customer/[controller]")]
    [Authorize(Roles = "Customer")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _service.GetByIdAsync(id));
    }
}
