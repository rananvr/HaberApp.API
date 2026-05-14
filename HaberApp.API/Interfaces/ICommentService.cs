using HaberApp.API.DTOs;

namespace HaberApp.API.Interfaces
{
    public interface ICommentService
    {
        Task AddCommentAsync(CommentCreateDto dto, int userId);
        Task<IEnumerable<CommentListDto>> GetCommentsByNewsIdAsync(int newsId);
        Task UpdateCommentAsync(int commentId, string content, int userId);
        Task DeleteCommentAsync(int commentId, int userId);

    }
}