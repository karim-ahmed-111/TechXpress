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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _uow.Categories.GetAllAsync();
            return categories.Select(c => new CategoryDto { CategoryId = c.CategoryId, Name = c.Name });
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var c = await _uow.Categories.GetByIdAsync(id);
            if (c == null) return null;
            return new CategoryDto { CategoryId = c.CategoryId, Name = c.Name };
        }

        public async Task AddAsync(CategoryDto categoryDto)
        {
            var category = new Category { Name = categoryDto.Name };
            await _uow.Categories.AddAsync(category);
            await _uow.SaveAsync();
        }

        public async Task UpdateAsync(CategoryDto categoryDto)
        {
            var category = await _uow.Categories.GetByIdAsync(categoryDto.CategoryId);
            if (category != null)
            {
                category.Name = categoryDto.Name;
                _uow.Categories.Update(category);
                await _uow.SaveAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _uow.Categories.GetByIdAsync(id);
            if (category != null)
            {
                _uow.Categories.Delete(category);
                await _uow.SaveAsync();
            }
        }
    }
}
