namespace HaberApp.API.DTOs
{
    public class CommentCreateDto
    {
        public int NewsId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}