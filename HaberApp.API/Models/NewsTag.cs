namespace HaberApp.API.Models
{
    public class NewsTag
    {
        public int NewsId { get; set; }
        public int TagId { get; set; }

        // Navigation Properties
        public News News { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
}