using HaberApp.API.DTOs;

namespace HaberApp.API.Interfaces
{
    public interface ITagService
    {
        Task<IEnumerable<TagDto>> GetAllTagsAsync();
        Task<IEnumerable<NewsListDto>> GetNewsByTagIdAsync(int tagId);
    }
}