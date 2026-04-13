using HaberApp.API.DTOs;

namespace HaberApp.API.Interfaces
{
    public interface INewsService
    {
        // Flutter bizden haberleri istediğinde ona ham News sınıfını değil, hazırladığımız DTO'yu döneceğiz.
        Task<IEnumerable<NewsListDto>> GetAllNewsAsync();

        // Flutter bize yeni haber gönderdiğinde DTO olarak alıp, işlemi yapacağız.
        // Yazar ID'sini şimdilik dışarıdan parametre alıyoruz, ileride bunu JWT (Token) içinden otomatik çekeceğiz.
        Task AddNewsAsync(NewsCreateDto dto, int authorId);
        Task<NewsDetailDto> GetNewsByIdAsync(int id);
        Task<IEnumerable<NewsListDto>> GetNewsByCategoryIdAsync(int categoryId);
    }
}