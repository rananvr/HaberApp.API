using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace HaberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/Category
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        // POST: api/Category
        [HttpPost]
        [Authorize] // Sadece giriş yapmış olanlar (Token'ı olanlar) kategori ekleyebilir
        public async Task<IActionResult> AddCategory([FromBody] CategoryCreateDto dto)
        {
            await _categoryService.AddCategoryAsync(dto);
            return Ok(new { Message = $"{dto.Name} kategorisi başarıyla eklendi!" });
        }
    }

}