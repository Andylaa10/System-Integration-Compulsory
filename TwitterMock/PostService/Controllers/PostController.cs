using Microsoft.AspNetCore.Mvc;
using PostService.Core.Services.DTOs;
using PostService.Core.Services.Interfaces;

namespace PostService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly HttpClient _client = new HttpClient();


    //TODO Check if user exists
    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPosts([FromHeader] string token)
    {
        try
        {
            var url = "http://localhost:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetAsync(url);

            if (!result.IsSuccessStatusCode) return Unauthorized(result.RequestMessage);
            return Ok(await _postService.GetPosts());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("{postId}")]
    public async Task<IActionResult> GetPostById([FromRoute] int postId, [FromHeader] string token)
    {
        try
        {
            var url = "http://localhost:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetAsync(url);

            if (!result.IsSuccessStatusCode) return Unauthorized(result.RequestMessage);
            return Ok(await _postService.GetPostById(postId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddPost([FromBody] AddPostDTO dto, [FromHeader] string token)
    {
        try
        {
            var url = "http://localhost:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetAsync(url);

            if (!result.IsSuccessStatusCode) return Unauthorized(result.RequestMessage);
            await _postService.AddPost(dto);
            return StatusCode(201, "Post successfully added");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("{postId}")]
    public async Task<IActionResult> UpdatePost([FromRoute] int postId, [FromBody] UpdatePostDTO dto, [FromHeader] string token)
    {
        try
        {
            var url = "http://localhost:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetAsync(url);

            if (!result.IsSuccessStatusCode) return Unauthorized(result.RequestMessage);
            
            await _postService.UpdatePost(postId, dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{postId}")]
    public async Task<IActionResult> DeletePost([FromRoute] int postId, [FromHeader] string token)
    {
        try
        {
            var url = "http://localhost:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetAsync(url);

            if (!result.IsSuccessStatusCode) return Unauthorized(result.RequestMessage);
            
            await _postService.DeletePost(postId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}