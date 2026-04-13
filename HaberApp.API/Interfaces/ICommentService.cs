using HaberApp.API.DTOs;

namespace HaberApp.API.Interfaces
{
    public interface ICommentService
    {
        Task AddCommentAsync(CommentCreateDto dto, int userId);
        Task<IEnumerable<CommentListDto>> GetCommentsByNewsIdAsync(int newsId);
    }
}