// TechXpress.API/Controllers/Customer/CartController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using System.Text.Json;
using TechXpress.Business.DTOs;
using TechXpress.Business.Services;
using TechXpress.Data.Repositories;
using TechXpress.Domain.Entities;

namespace TechXpress.API.Controllers.Customer
{
    [ApiController]
    [Route("api/customer/[controller]")]
    [Authorize(Roles = "Customer")]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IOrderService _orderService;

        public CartController(IUnitOfWork uow, IOrderService orderService)
        {
            _uow = uow;
            _orderService = orderService;
        }

        private const string CartSessionKey = "Cart";

        [HttpGet]
        public IActionResult GetCart()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            var cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItemDto>()
                : JsonSerializer.Deserialize<List<CartItemDto>>(cartJson);
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDto item)
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            var cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItemDto>()
                : JsonSerializer.Deserialize<List<CartItemDto>>(cartJson);

            var existing = cart.FirstOrDefault(c => c.ProductId == item.ProductId);
            if (existing != null)
                existing.Quantity += item.Quantity;
            else
                cart.Add(item);

            HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
            return Ok(cart);
        }

        [HttpPost("remove")]
        public IActionResult RemoveFromCart([FromBody] CartItemDto item)
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            var cart = string.IsNullOrEmpty(cartJson)
                ? new List<CartItemDto>()
                : JsonSerializer.Deserialize<List<CartItemDto>>(cartJson);

            cart.RemoveAll(c => c.ProductId == item.ProductId);
            HttpContext.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
            return Ok(cart);
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromServices] IStripeService stripeService)
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson)) return BadRequest("Cart is empty.");

            var cart = JsonSerializer.Deserialize<List<CartItemDto>>(cartJson);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Calculate total and prepare order items
            var orderItems = new List<OrderItem>();
            foreach (var item in cart)
            {
                var product = await _uow.Products.GetByIdAsync(item.ProductId);
                if (product == null) continue;
                orderItems.Add(new OrderItem
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            // Create order (status Pending)
            var order = new Order
            {
                CustomerId = userId,
                OrderDate = DateTime.UtcNow,
                Status = "Pending",
                OrderItems = orderItems
            };
            await _uow.Orders.AddAsync(order);
            await _uow.SaveAsync();

            // Create Stripe Checkout Session
            var lineItems = new List<SessionLineItemOptions>();
            foreach (var item in cart)
            {
                var product = await _uow.Products.GetByIdAsync(item.ProductId);
                if (product == null) continue;
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = product.Name
                        },
                        UnitAmount = (long)(product.Price * 100), // in cents
                        Currency = "usd"
                    },
                    Quantity = item.Quantity
                });
            }
            var successUrl = $"{Request.Scheme}://{Request.Host}/success"; // placeholder
            var cancelUrl = $"{Request.Scheme}://{Request.Host}/cancel";   // placeholder
            var session = stripeService.CreateCheckoutSession(successUrl, cancelUrl, lineItems);

            // Clear cart after creating order
            HttpContext.Session.Remove(CartSessionKey);

            return Ok(new { SessionId = session.Id });
        }
    }
}
