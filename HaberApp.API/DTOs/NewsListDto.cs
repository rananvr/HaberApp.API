namespace HaberApp.API.DTOs
{
    public class NewsListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string Content { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty; 
        public string AuthorName { get; set; } = string.Empty;   
        public DateTime? PublishedAt { get; set; }
    }
}