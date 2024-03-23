namespace CommentService.Core.Entities.Dtos;

public class AddCommentDto
{
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}