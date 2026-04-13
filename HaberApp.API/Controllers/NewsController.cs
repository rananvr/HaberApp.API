using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HaberApp.API.Controllers
{
    // Burası API'mizin adresidir. Yani Flutter'dan istek atarken "https://localhost:port/api/News" adresine istek atacağız.
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        // Constructor ile Aşçımızı (Servisimizi) garsona (Kontrolcüye) tanıtıyoruz
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // GET: api/News -> Tüm haberleri listelemek için kullanılacak kapı
        [HttpGet]
        public async Task<IActionResult> GetAllNews()
        {
            var news = await _newsService.GetAllNewsAsync();
            return Ok(news); // Ok() metodu, "200 Başarılı" durum koduyla birlikte veriyi gönderir
        }

        // POST: api/News
        [HttpPost]
        [Authorize] // İŞTE KİLİT! Bu satır sayesinde Token'ı olmayan kimse buraya giremez.
        public async Task<IActionResult> AddNews([FromBody] NewsCreateDto dto)
        {
            // 1. Token'ın içinden giriş yapan kullanıcının gerçek ID'sini okuyoruz
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
                return Unauthorized("Güvenlik hatası: Kullanıcı kimliği bulunamadı.");

            int currentUserId = int.Parse(userIdString);

            // 2. Haberi bu sefer gerçek kullanıcının ID'si ile kaydediyoruz
            await _newsService.AddNewsAsync(dto, currentUserId);

            return Ok(new { Message = "Harika! Haber, sizin hesabınız üzerinden başarıyla eklendi." });
        }
    }
}