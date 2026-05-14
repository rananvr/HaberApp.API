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
        public async Task UpdateCommentAsync(int commentId, string content, int userId)
        {
            // 1. Veritabanından o yorumu buluyoruz
            var comment = await _context.Comments.FindAsync(commentId);

            if (comment == null)
                throw new Exception("Yorum bulunamadı.");

            // 2. Güvenlik: Yorumu düzenlemek isteyen kişi, yorumun gerçek sahibi mi?
            if (comment.UserId != userId)
                throw new Exception("Bu yorumu düzenleme yetkiniz yok.");

            // 3. İçeriği değiştir ve kaydet
            comment.Content = content;
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int commentId, int userId)
        {
            // 1. Veritabanından o yorumu buluyoruz
            var comment = await _context.Comments.FindAsync(commentId);

            if (comment == null)
                throw new Exception("Yorum bulunamadı.");

            // 2. Güvenlik: Yorumu silmek isteyen kişi, yorumun gerçek sahibi mi?
            if (comment.UserId != userId)
                throw new Exception("Bu yorumu silme yetkiniz yok.");

            // 3. Veritabanından sil ve kaydet
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}