using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Business.DTOs;

namespace TechXpress.Business.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task AddAsync(ProductDto product);
        Task UpdateAsync(ProductDto product);
        Task DeleteAsync(int id);
        Task<IEnumerable<ProductDto>> GetBySellerAsync(string sellerId);
    }
}
