using CommentService.Core.Entities;
using CommentService.Core.Entities.Helper;

namespace CommentService.Core.Repositories.Interfaces;

public interface ICommentRepository
{
    /// <summary>
    /// Retrieves the comments on a specific post in a paginated result
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public Task<PaginatedResult<Comment>> GetComments(int postId, int pageNumber, int pageSize);

    /// <summary>
    /// Add a comment on a post 
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    public Task AddComment(Comment comment);

    /// <summary>
    /// Updates a comment on a post
    /// </summary>
    /// <param name="commentId"></param>
    /// <param name="comment"></param>
    /// <returns></returns>
    public Task UpdateComment(int commentId, Comment comment);

    /// <summary>
    /// Deletes comment with a specific id
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    public Task DeleteComment(int commentId);

    /// <summary>
    /// Deletes comments with the given postId 
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public Task DeleteCommentsOnPost(int postId);

    /// <summary>
    /// Check whether a comment exist with the specified id
    /// </summary>
    /// <param name="commentId"></param>
    /// <returns></returns>
    public Task<bool> DoesCommentExists(int commentId);
}