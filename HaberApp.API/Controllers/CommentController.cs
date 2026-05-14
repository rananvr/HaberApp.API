using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using HaberApp.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HaberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // Yorumları Listele 
        [HttpGet("news/{newsId}")]
        public async Task<IActionResult> GetComments(int newsId)
        {
            var comments = await _commentService.GetCommentsByNewsIdAsync(newsId);
            return Ok(comments);
        }

        // Yorum Ekle (Sadece giriş yapanlar)
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CommentCreateDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _commentService.AddCommentAsync(dto, userId);
            return Ok(new { Message = "Yorumunuz başarıyla eklendi." });
        }

        // Yorum Düzenle (Sadece giriş yapanlar ve kendi yorumu olanlar)
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentUpdateDto dto)
        {
            try
            {
                // Token'dan o anki kullanıcının ID'sini alıyoruz (Güvenlik için)
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                // Servis katmanına ID, yeni metin ve kullanıcı ID'sini yolluyoruz
                await _commentService.UpdateCommentAsync(id, dto.Content, userId);

                return Ok(new { Message = "Yorumunuz başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Yorum Sil (Sadece giriş yapanlar ve kendi yorumu olanlar)
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                await _commentService.DeleteCommentAsync(id, userId);

                return Ok(new { Message = "Yorum başarıyla silindi." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        public class CommentUpdateDto
        {
            public string Content { get; set; }
        }

    }
}