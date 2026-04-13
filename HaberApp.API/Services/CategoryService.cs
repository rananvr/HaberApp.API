using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using HaberApp.API.Models;

namespace HaberApp.API.Services
{
    public class CategoryService : ICategoryService
    {
        // Kategorileri veritabanından çekmek için Joker Depocumuzu (GenericRepository) çağırıyoruz
        private readonly IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryListDto>> GetAllCategoriesAsync()
        {
            // 1. Veritabanındaki tüm kategorileri getir
            var categories = await _categoryRepository.GetAllAsync();

            // 2. Ham kategorileri DTO paketlerine çevir
            return categories.Select(c => new CategoryListDto
            {
                Id = c.Id,
                Name = c.Name,
                IconUrl = c.IconUrl
            }).ToList();
        }
        // CategoryService.cs içine eklenecek yeni metot:
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