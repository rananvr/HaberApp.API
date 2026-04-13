using HaberApp.API.Data;
using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HaberApp.API.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;

        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
        {
            return await _context.Tags
                .Select(t => new TagDto { Id = t.Id, Name = t.TagName })
                .ToListAsync();
        }

        public async Task<IEnumerable<NewsListDto>> GetNewsByTagIdAsync(int tagId)
        {
            // NewsTags üzerinden o etikete sahip haberleri bulup getiriyoruz
            return await _context.NewsTags
                .Include(nt => nt.News).ThenInclude(n => n.Category)
                .Include(nt => nt.News).ThenInclude(n => n.Author)
                .Where(nt => nt.TagId == tagId)
                .Select(nt => new NewsListDto
                {
                    Id = nt.News.Id,
                    Title = nt.News.Title,
                    ImageUrl = nt.News.ImageUrl,
                    CategoryName = nt.News.Category.Name,
                    AuthorName = nt.News.Author.FullName,
                    PublishedAt = nt.News.PublishedAt
                }).ToListAsync();
        }
    }
}