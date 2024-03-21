using System.Data.Common;
using System.Text;
using System.Text.Json;
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
            var url = "http://AuthService:5206/api/Auth/ValidateToken";
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
            var url = "http://AuthService:5206/api/Auth/ValidateToken";
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
            var urlToken = "http://AuthService:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var resultToken = await _client.GetAsync(urlToken);

            if (!resultToken.IsSuccessStatusCode) return Unauthorized(resultToken.RequestMessage);
            var post = await _postService.AddPost(dto);

            dto.PostId = post.Id;
            
            var url = "http://TimeLineService:5206/api/TimeLine";
            var payload = JsonSerializer.Serialize(dto);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(url, content);

            if (result.IsSuccessStatusCode)
            {
                return StatusCode(201, "Post successfully added");
            }

            return BadRequest(result.RequestMessage);
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
            var url = "http://AuthService:5206/api/Auth/ValidateToken";
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
            var url = "http://AuthService:5206/api/Auth/ValidateToken";
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