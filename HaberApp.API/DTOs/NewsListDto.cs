namespace HaberApp.API.DTOs
{
    public class NewsListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string CategoryName { get; set; } = string.Empty; // Sadece kategori adını göndereceğiz, tüm kategoriyi değil
        public string AuthorName { get; set; } = string.Empty;   // Yazarın sadece adı
        public DateTime? PublishedAt { get; set; }
    }
}