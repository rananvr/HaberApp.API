namespace HaberApp.API.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int NewsId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation Properties
        public User User { get; set; } = null!;
        public News News { get; set; } = null!;
    }
}