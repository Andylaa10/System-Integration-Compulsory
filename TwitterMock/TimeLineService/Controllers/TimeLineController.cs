using Microsoft.AspNetCore.Mvc;
using TimeLineService.Core.Services.DTO;
using TimeLineService.Core.Services.Interfaces;

namespace TimeLineService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeLineController : ControllerBase
{
    private ITimeLineService _timeLineService;

    public TimeLineController(ITimeLineService timeLineService)
    {
        _timeLineService = timeLineService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTimeLines()
    {
        try
        {
            return Ok(await _timeLineService.GetTimeLines());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddToTimeLine([FromBody] AddToTimeLineDto dto)
    {
        try
        {
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
    public async Task<IActionResult> DeletePostFromTimeLine([FromRoute] int postId)
    {
        try
        {
            await _timeLineService.DeleteFromTimeLine(postId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}