// TechXpressApi.Web/Controllers/CartController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechXpress.Services.Interfaces;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var cartItems = await _cartService.GetCartItemsAsync(userId);
        return Ok(cartItems);
    }

    [HttpPost("items")]
    public async Task<IActionResult> AddToCart([FromBody] CartItemModel model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _cartService.AddToCartAsync(userId, model.ProductId, model.Quantity);
        return Ok();
    }

    [HttpDelete("items/{productId}")]
    public async Task<IActionResult> RemoveFromCart(int productId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _cartService.RemoveFromCartAsync(userId, productId);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> ClearCart()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _cartService.ClearCartAsync(userId);
        return Ok();
    }
}

public class CartItemModel
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}