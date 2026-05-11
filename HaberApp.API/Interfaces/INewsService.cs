using HaberApp.API.DTOs;

namespace HaberApp.API.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsListDto>> GetAllNewsAsync();

        Task AddNewsAsync(NewsCreateDto dto, int authorId);
        Task<NewsDetailDto> GetNewsByIdAsync(int id);
        Task<IEnumerable<NewsListDto>> GetNewsByCategoryIdAsync(int categoryId);
    }
}