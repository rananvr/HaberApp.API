using HaberApp.API.Data;
using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using HaberApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HaberApp.API.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IGenericRepository<Bookmark> _bookmarkRepository;
        private readonly AppDbContext _context;

        public BookmarkService(IGenericRepository<Bookmark> bookmarkRepository, AppDbContext context)
        {
            _bookmarkRepository = bookmarkRepository;
            _context = context;
        }

        public async Task AddBookmarkAsync(int newsId, int userId)
        {
            // Zaten kaydedilmiş mi kontrolü
            var exists = await _context.Bookmarks.AnyAsync(b => b.NewsId == newsId && b.UserId == userId);
            if (!exists)
            {
                await _bookmarkRepository.AddAsync(new Bookmark { NewsId = newsId, UserId = userId });
                await _bookmarkRepository.SaveChangesAsync();
            }
        }

        public async Task RemoveBookmarkAsync(int newsId, int userId)
        {
            var bookmark = await _context.Bookmarks.FirstOrDefaultAsync(b => b.NewsId == newsId && b.UserId == userId);
            if (bookmark != null)
            {
                _context.Bookmarks.Remove(bookmark);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<NewsListDto>> GetUserBookmarksAsync(int userId)
        {
            return await _context.Bookmarks
                .Include(b => b.News).ThenInclude(n => n.Category)
                .Include(b => b.News).ThenInclude(n => n.Author)
                .Where(b => b.UserId == userId)
                .Select(b => new NewsListDto
                {
                    Id = b.News.Id,
                    Title = b.News.Title,
                    ImageUrl = b.News.ImageUrl,
                    CategoryName = b.News.Category.Name,
                    AuthorName = b.News.Author.FullName,
                    PublishedAt = b.News.PublishedAt
                }).ToListAsync();
        }
    }
}