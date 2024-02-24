using CommentService.Core.Entities;
using CommentService.Core.Entities.Dtos;
using CommentService.Core.Entities.Helper;

namespace CommentService.Core.Repositories.Interfaces;

public interface ICommentRepository
{
    /// <summary>
    /// Retrieves the comments on a specific post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public Task<PaginatedResult<Comment>> GetComments(int postId, int pageIndex, int pageSize);

    /// <summary>
    /// Add a comment on a post 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task AddComment(Comment comment);

    /// <summary>
    /// Updates a comment on a post
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task UpdateComment(int commentId, Comment comment);

    /// <summary>
    /// Deletes comment with a specific id
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    public Task DeleteComment(int commentId);
}