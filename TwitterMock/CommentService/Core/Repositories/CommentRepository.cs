using CommentService.Core.Helper;
using CommentService.Core.Repositories.Interfaces;

namespace CommentService.Core.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DatabaseContext _context;

    public CommentRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    
}