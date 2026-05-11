using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using HaberApp.API.Models;

namespace HaberApp.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryListDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return categories.Select(c => new CategoryListDto
            {
                Id = c.Id,
                Name = c.Name,
                IconUrl = c.IconUrl
            }).ToList();
        }
        public async Task AddCategoryAsync(CategoryCreateDto dto)
        {
            var newCategory = new Category
            {
                Name = dto.Name,
                IconUrl = dto.IconUrl
            };

            await _categoryRepository.AddAsync(newCategory);
            await _categoryRepository.SaveChangesAsync();
        }
    }
}