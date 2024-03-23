using CommentService.Core.Entities;
using CommentService.Core.Entities.Dtos;
using CommentService.Core.Entities.Helper;

namespace CommentService.Core.Services.Interfaces;

public interface ICommentService
{
    /// <summary>
    /// Retrieves the comments on a specific post
    /// </summary>
    /// <param name="postId"></param>
    /// <returns></returns>
    public Task<PaginatedResult<Comment>> GetComments(int postId, PaginatedDto dto);

    /// <summary>
    /// Add a comment on a post 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task AddComment(AddCommentDto comment, int userIdOfPost);

    /// <summary>
    /// Updates a comment on a post
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public Task UpdateComment(int commentId, UpdateCommentDto comment);

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
}