using HaberApp.API.DTOs;

namespace HaberApp.API.Interfaces
{
    public interface IBookmarkService
    {
        Task AddBookmarkAsync(int newsId, int userId);
        Task RemoveBookmarkAsync(int newsId, int userId);
        Task<IEnumerable<NewsListDto>> GetUserBookmarksAsync(int userId);
    }
}