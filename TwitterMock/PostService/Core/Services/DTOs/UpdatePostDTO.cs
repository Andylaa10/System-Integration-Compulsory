namespace PostService.Core.Services.DTOs;

public class UpdatePostDTO
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime UpdatedAt { get; set; }
}