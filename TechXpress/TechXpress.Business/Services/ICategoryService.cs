using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechXpress.Business.DTOs;

namespace TechXpress.Business.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task AddAsync(CategoryDto category);
        Task UpdateAsync(CategoryDto category);
        Task DeleteAsync(int id);
    }
}
