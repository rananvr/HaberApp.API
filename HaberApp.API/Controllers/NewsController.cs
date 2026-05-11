using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HaberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var news = await _newsService.GetAllNewsAsync();
            return Ok(news); 
        }

        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> AddNews([FromBody] NewsCreateDto dto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized("Güvenlik hatası: Kullanıcı kimliği bulunamadı.");

            int currentUserId = int.Parse(userIdString);

            await _newsService.AddNewsAsync(dto, currentUserId);

            return Ok(new { Message = "Harika! Haber, sizin hesabınız üzerinden başarıyla eklendi." });
        }
    }
}