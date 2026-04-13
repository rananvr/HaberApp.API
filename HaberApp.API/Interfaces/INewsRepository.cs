using HaberApp.API.Models;

namespace HaberApp.API.Interfaces
{
    // IGenericRepository'deki tüm özellikleri miras alır, üstüne kendi özel metotlarını ekler
    public interface INewsRepository : IGenericRepository<News>
    {
        Task<IEnumerable<News>> GetAllNewsWithDetailsAsync(); // Detaylarıyla tüm haberler
        Task<News?> GetNewsByIdWithDetailsAsync(int id);      // Detaylarıyla tek haber
        Task<IEnumerable<News>> GetNewsByCategoryIdWithDetailsAsync(int categoryId);
    }
}