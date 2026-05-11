using HaberApp.API.Data;
using HaberApp.API.Interfaces;
using HaberApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HaberApp.API.Repositories
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        public NewsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<News>> GetAllNewsWithDetailsAsync()
        {
            return await _context.News
                .Include(n => n.Category)
                .Include(n => n.Author)
                .ToListAsync();
        }

        public async Task<News?> GetNewsByIdWithDetailsAsync(int id)
        {
            return await _context.News
                .Include(n => n.Category)
                .Include(n => n.Author)
                .FirstOrDefaultAsync(n => n.Id == id);
        }
        public async Task<IEnumerable<News>> GetNewsByCategoryIdWithDetailsAsync(int categoryId)
        {
            return await _context.News
                .Include(n => n.Category)
                .Include(n => n.Author)
                .Where(n => n.CategoryId == categoryId)
                .OrderByDescending(n => n.PublishedAt)
                .ToListAsync();
        }

    
    }
}