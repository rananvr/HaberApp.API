namespace HaberApp.API.Models
{
    public class News
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int ViewCount { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public DateTime? PublishedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public Category Category { get; set; } = null!;
        public User Author { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<NewsTag> NewsTags { get; set; } = new List<NewsTag>();
        public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
    }
}