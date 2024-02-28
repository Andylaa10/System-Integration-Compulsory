using AutoMapper;
using CommentService.Core.Entities;
using CommentService.Core.Entities.Dtos;
using CommentService.Core.Entities.Helper;
using CommentService.Core.Repositories.Interfaces;
using CommentService.Core.Services.Interfaces;

namespace CommentService.Core.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository ?? throw new ArgumentException("Comment repository cannot be null");
        _mapper = mapper ?? throw new ArgumentException("Automapper cannot be null");
    }

    public async Task<PaginatedResult<Comment>> GetComments(int postId, PaginatedDto dto)
    {
        if (postId < 1) throw new ArgumentException("Id cannot be less than 1");

        return await _commentRepository.GetComments(postId, dto.PageNumber, dto.PageSize);
    }

    public async Task AddComment(AddCommentDto comment)
    {
        await _commentRepository.AddComment(_mapper.Map<Comment>(comment));
    }

    public async Task UpdateComment(int commentId, UpdateCommentDto comment)
    {
        if (commentId != comment.Id)
            throw new ArgumentException("Id in the route must match the update comments id");
        if (commentId < 1) throw new ArgumentException("Id cannot be less than 1");

        if (!await _commentRepository.DoesCommentExists(commentId))
        {
            throw new KeyNotFoundException($"No such comment with id of {commentId}");
        }
        
        await _commentRepository.UpdateComment(commentId, _mapper.Map<Comment>(comment));
    }

    public async Task DeleteComment(int commentId)
    {
        if (commentId < 1) throw new ArgumentException("Id cannot be less than 1");

        if (!await _commentRepository.DoesCommentExists(commentId))
        {
            throw new KeyNotFoundException($"No such comment with id of {commentId}");
        }
        await _commentRepository.DeleteComment(commentId);

    }
}