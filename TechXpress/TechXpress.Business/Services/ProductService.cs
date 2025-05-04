using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Business.DTOs;
using TechXpress.Data.Repositories;
using TechXpress.Domain.Entities;

namespace TechXpress.Business.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _uow.Products.GetAllAsync();
            // Eager-load categories for name
            var list = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name
            });
            return list;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var p = await _uow.Products.GetByIdAsync(id);
            if (p == null) return null;
            return new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name
            };
        }

        public async Task AddAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                CategoryId = productDto.CategoryId,
                // SellerId should be set by caller (e.g. controller) if needed
            };
            await _uow.Products.AddAsync(product);
            await _uow.SaveAsync();
        }

        public async Task UpdateAsync(ProductDto productDto)
        {
            var product = await _uow.Products.GetByIdAsync(productDto.ProductId);
            if (product != null)
            {
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Price = productDto.Price;
                product.CategoryId = productDto.CategoryId;
                _uow.Products.Update(product);
                await _uow.SaveAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _uow.Products.GetByIdAsync(id);
            if (product != null)
            {
                _uow.Products.Delete(product);
                await _uow.SaveAsync();
            }
        }

        public async Task<IEnumerable<ProductDto>> GetBySellerAsync(string sellerId)
        {
            var products = await _uow.Products.FindAsync(p => p.SellerId == sellerId);
            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name
            });
        }
    }
}
