using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using UserService.Core.Services.Dtos;
using UserService.Core.Services.Interfaces;

namespace UserService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly HttpClient _client = new HttpClient();

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("AddUser")]
    public async Task<IActionResult> AddUser([FromBody] CreateUserDTO dto)
    {
        try
        {
            await _userService.CreateUser(dto);
            return StatusCode(201, "Succesfully added user to DB");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("GetUsers")]
    public async Task<IActionResult> GetUsers([FromQuery] PaginatedDTO paginatedDto, [FromHeader] string token)
    {
        try
        {
            var url = "http://localhost:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetAsync(url);

            if (!result.IsSuccessStatusCode) return Unauthorized(result.RequestMessage);
            return Ok(await _userService.GetUsers(paginatedDto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetUserById([FromRoute] int userId, [FromHeader] string token)
    {
        try
        {
            var url = "http://localhost:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetAsync(url);

            if (result.IsSuccessStatusCode)
            {
                return Ok(await _userService.GetUserById(userId));
            }

            return Unauthorized();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("{userId}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UpdateUserDTO dto,
        [FromHeader] string token)
    {
        try
        {
            var url = "http://localhost:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetAsync(url);

            if (!result.IsSuccessStatusCode) return Unauthorized();
            await _userService.UpdateUser(userId, dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpDelete]
    [Route("{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int userId, [FromHeader] string token)
    {
        try
        {
            var url = "http://localhost:5206/api/Auth/ValidateToken";
            _client.DefaultRequestHeaders.Add("token", token);
            var result = await _client.GetAsync(url);

            if (!result.IsSuccessStatusCode) return Unauthorized();
            await _userService.DeleteUser(userId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}