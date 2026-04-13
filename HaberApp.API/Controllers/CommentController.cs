using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
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

        // Yorumları Listele (Herkes görebilir)
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

    }
}