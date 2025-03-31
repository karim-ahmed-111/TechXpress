using TechXpress.Data.Models;
using TechXpress.Data.Repositories.ProductRepository;
using TechXpress.Services.Services;
using TechXpress.Data.Models;
using TechXpress.Data.Repositories;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(string? search, int? categoryId)
    {
        var products = await _productRepository.GetAllAsync();
        if (!string.IsNullOrEmpty(search))
            products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        if (categoryId.HasValue)
            products = products.Where(p => p.CategoryId == categoryId.Value);
        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }

    public async Task AddProductAsync(Product product)
    {
        await _productRepository.AddAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }
}