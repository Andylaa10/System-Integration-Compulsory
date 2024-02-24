using CommentService.Core.Repositories.Interfaces;
using CommentService.Core.Services.Interfaces;

namespace CommentService.Core.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
}