using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HaberApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Kaydedilenler sadece üyeler içindir
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyBookmarks()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            return Ok(await _bookmarkService.GetUserBookmarksAsync(userId));
        }

        [HttpPost("{newsId}")]
        public async Task<IActionResult> Add(int newsId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _bookmarkService.AddBookmarkAsync(newsId, userId);
            return Ok(new { Message = "Haber kaydedildi." });
        }

        [HttpDelete("{newsId}")]
        public async Task<IActionResult> Remove(int newsId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _bookmarkService.RemoveBookmarkAsync(newsId, userId);
            return Ok(new { Message = "Haber kaydı silindi." });
        }
    }
}