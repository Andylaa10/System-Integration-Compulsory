namespace CommentService.Core.Entities.Dtos;

public class UpdateCommentDto
{
    
    public int CommentId { get; set; }
    public string Content { get; set; }
    public DateTime UpdatedAt { get; set; }
}