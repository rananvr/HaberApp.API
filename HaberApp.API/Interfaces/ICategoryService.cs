using HaberApp.API.DTOs;

namespace HaberApp.API.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryListDto>> GetAllCategoriesAsync();
        Task AddCategoryAsync(CategoryCreateDto dto);
    }
}