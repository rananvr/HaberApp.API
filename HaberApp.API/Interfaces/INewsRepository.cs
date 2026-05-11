using HaberApp.API.Models;

namespace HaberApp.API.Interfaces
{
    public interface INewsRepository : IGenericRepository<News>
    {
        Task<IEnumerable<News>> GetAllNewsWithDetailsAsync(); 
        Task<News?> GetNewsByIdWithDetailsAsync(int id);      
        Task<IEnumerable<News>> GetNewsByCategoryIdWithDetailsAsync(int categoryId);
    }
}