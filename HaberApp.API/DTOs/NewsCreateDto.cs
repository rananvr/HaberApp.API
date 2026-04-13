namespace HaberApp.API.DTOs
{
    public class NewsCreateDto
    {
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

        // Haberi eklerken etiket id'lerini de bir liste olarak alabiliriz
        public List<int> TagIds { get; set; } = new List<int>();
    }
}