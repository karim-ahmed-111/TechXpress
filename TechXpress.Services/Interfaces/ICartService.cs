using TechXpress.Data.Models;

public interface ICartService
{
    Task<IEnumerable<CartItem>> GetCartItemsAsync(string userId);
    Task AddToCartAsync(string userId, int productId, int quantity);
    Task RemoveFromCartAsync(string userId, int productId);
    Task ClearCartAsync(string userId);
}