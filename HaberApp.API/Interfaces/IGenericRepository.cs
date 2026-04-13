namespace HaberApp.API.Interfaces
{
    // <T> harfi jokerdir. Buraya User, News, Category ne gönderirsek onun için çalışır.
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(); // Hepsini getir
        Task<T?> GetByIdAsync(int id);      // ID'ye göre 1 tane getir
        Task AddAsync(T entity);            // Yeni ekle
        void Update(T entity);              // Güncelle
        void Delete(T entity);              // Sil
        Task SaveChangesAsync();            // Değişiklikleri veritabanına kaydet
    }
}