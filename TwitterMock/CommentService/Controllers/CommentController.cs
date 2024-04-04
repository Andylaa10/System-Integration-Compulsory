using System.Text.Json;
using CommentService.Core.Entities.Dtos;
using CommentService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommentService.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly HttpClient _client = new HttpClient();

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
            var request = await _client.GetAsync($"http://PostService/api/Post/{dto.PostId}");

            if (!request.IsSuccessStatusCode) throw new KeyNotFoundException($"No post with the id of {dto.PostId}");
            var result = await request.Content.ReadAsStringAsync();
            JsonDocument doc = JsonDocument.Parse(result);
            JsonElement root = doc.RootElement;
            int userId = root.GetProperty("userId").GetInt32();
            
            await _commentService.AddComment(dto, userId);
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

    [HttpDelete]
    [Route("DeleteCommentsOnPost/{postId}")]
    public async Task<IActionResult> DeleteCommentsOnPost([FromRoute] int postId)
    {
        try
        {
            await _commentService.DeleteCommentsOnPost(postId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}