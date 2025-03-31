// TechXpressApi.Services/Services/OrderService.cs
using Microsoft.EntityFrameworkCore;
using TechXpress.Data.Context;
using TechXpress.Data.Models;
using TechXpress.Services.Interfaces;
using TechXpress.Data;
using TechXpress.Data.Models;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly ICartService _cartService;
    private readonly IPaymentService _paymentService;

    public OrderService(ApplicationDbContext context, ICartService cartService, IPaymentService paymentService)
    {
        _context = context;
        _cartService = cartService;
        _paymentService = paymentService;
    }

    public async Task CreateOrderAsync(string userId, string paymentToken)
    {
        var cartItems = await _cartService.GetCartItemsAsync(userId);
        if (!cartItems.Any()) throw new Exception("Cart is empty");

        var total = cartItems.Sum(c => c.Product!.Price * c.Quantity);
        var charge = await _paymentService.CreateChargeAsync(total, paymentToken);
        if (charge.Status != "succeeded") throw new Exception("Payment failed");

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "Paid",
                OrderDetails = cartItems.Select(c => new OrderDetail
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Product!.Price
                }).ToList()
            };
            _context.Orders.Add(order);

            foreach (var item in cartItems)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product!.Stock < item.Quantity) throw new Exception("Insufficient stock");
                product.Stock -= item.Quantity;
            }

            await _context.SaveChangesAsync();
            await _cartService.ClearCartAsync(userId);
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userId)
    {
        return await _context.Orders
            .Where(o => o.UserId == userId)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToListAsync();
    }
}