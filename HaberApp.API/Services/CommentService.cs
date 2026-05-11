using HaberApp.API.DTOs;
using HaberApp.API.Interfaces;
using HaberApp.API.Models;
using Microsoft.EntityFrameworkCore;
using HaberApp.API.Data; // AppDbContext
using HaberApp.API.Models;

namespace HaberApp.API.Services
{
    public class CommentService : ICommentService
    {
        private readonly IGenericRepository<Comment> _commentRepository;
        private readonly AppDbContext _context; 

        public CommentService(IGenericRepository<Comment> commentRepository, AppDbContext context)
        {
            _commentRepository = commentRepository;
            _context = context;
        }

        public async Task AddCommentAsync(CommentCreateDto dto, int userId)
        {
            var comment = new Comment
            {
                NewsId = dto.NewsId,
                UserId = userId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommentListDto>> GetCommentsByNewsIdAsync(int newsId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.NewsId == newsId && c.IsActive)
                .OrderByDescending(c => c.CreatedAt)
                .Select(c => new CommentListDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    UserName = c.User.FullName,
                    CreatedAt = c.CreatedAt
                }).ToListAsync();
        }
    }
}