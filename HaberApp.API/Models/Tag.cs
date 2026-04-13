namespace HaberApp.API.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagName { get; set; } = string.Empty;

        // Navigation Properties
        public ICollection<NewsTag> NewsTags { get; set; } = new List<NewsTag>();
    }
}