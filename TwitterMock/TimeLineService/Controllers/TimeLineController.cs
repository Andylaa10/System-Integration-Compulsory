using Microsoft.AspNetCore.Mvc;
using TimeLineService.Core.Services.DTO;
using TimeLineService.Core.Services.Interfaces;

namespace TimeLineService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeLineController : ControllerBase
{
    private readonly ITimeLineService _timeLineService;
    private readonly HttpClient _client = new HttpClient();


    public TimeLineController(ITimeLineService timeLineService)
    {
        _timeLineService = timeLineService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTimeLines([FromHeader] string token)
    {
        try
        {
            var url = "http://localhost:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetAsync(url);

            if (!result.IsSuccessStatusCode) return Unauthorized(result.RequestMessage);
            
            return Ok(await _timeLineService.GetTimeLines());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToTimeLine([FromBody] AddToTimeLineDto dto, [FromHeader] string token)
    {
        try
        {
            // We are not checking the Token, because we already check that in the PostController where we make this API call
            
            await _timeLineService.AddToTimeLine(dto);
            return StatusCode(201, "Post successfully added to the timeline");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{postId}")]
    public async Task<IActionResult> DeletePostFromTimeLine([FromRoute] int postId, [FromHeader] string token)
    {
        try
        {
            var url = "http://localhost:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetAsync(url);

            if (!result.IsSuccessStatusCode) return Unauthorized(result.RequestMessage);
            
            await _timeLineService.DeleteFromTimeLine(postId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}