namespace HaberApp.API.DTOs
{
    public class CategoryCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
    }
}