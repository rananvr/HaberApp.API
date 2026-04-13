namespace HaberApp.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; }

        // Navigation Properties
        public ICollection<News> News { get; set; } = new List<News>();
    }
}