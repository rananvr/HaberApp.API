namespace HaberApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

       
        public ICollection<News> News { get; set; } = new List<News>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Bookmark> Bookmarks { get; set; } = new List<Bookmark>();
    }
}