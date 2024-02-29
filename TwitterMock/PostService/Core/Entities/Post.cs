namespace PostService.Core.Entities;

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}