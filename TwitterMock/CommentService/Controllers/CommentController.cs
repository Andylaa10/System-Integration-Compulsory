using CommentService.Core.Entities.Dtos;
using CommentService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet]
    [Route("{postId}")]
    public async Task<IActionResult> GetComments([FromRoute] int postId, [FromQuery] PaginatedDto dto)
    {
        try
        {
            return Ok(await _commentService.GetComments(postId, dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddComment([FromBody] AddCommentDto dto)
    {
        try
        {
            await _commentService.AddComment(dto);
            return StatusCode(201, "Comment successfully added");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("{commentId}")]
    public async Task<IActionResult> UpdateComment([FromRoute] int commentId, [FromBody] UpdateCommentDto dto)
    {
        try
        {
            await _commentService.UpdateComment(commentId, dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        try
        {
            await _commentService.DeleteComment(commentId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}