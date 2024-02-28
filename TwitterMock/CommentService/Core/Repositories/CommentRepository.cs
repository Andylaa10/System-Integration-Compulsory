using CommentService.Core.Entities;
using CommentService.Core.Entities.Helper;
using CommentService.Core.Helper;
using CommentService.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Core.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly DatabaseContext _context;

    public CommentRepository(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<PaginatedResult<Comment>> GetComments(int postId, int pageIndex, int pageSize)
    {
        var comments = await _context.Comments
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .Where(c => c.PostId == postId)
            .ToListAsync();

        var totalCount =  await _context.Comments
            .CountAsync(c => c.PostId == postId);
        
        return new PaginatedResult<Comment>
        {
            Items = comments,
            TotalCount = totalCount
        };
    }

    public async Task AddComment(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateComment(int commentId, Comment updatedComment)
    {
        var commentToUpdate = await _context.Comments.FirstOrDefaultAsync(c => c.Id == updatedComment.Id);

        if (commentId != commentToUpdate.Id)
        {
            throw new ArgumentException("The ids do not match");
        }

        commentToUpdate.Content = updatedComment.Content;
        commentToUpdate.UpdatedAt = DateTime.Now;
        _context.Comments.Update(commentToUpdate);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteComment(int commentId)
    {
        var commentToDelete = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        _context.Comments.Remove(commentToDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesCommentExists(int commentId)
    {
        var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment != null || comment != default)
        {
            return true;
        }

        return false;
    }
}