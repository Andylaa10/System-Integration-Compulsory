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
            return StatusCode(201, "Successfully added user to DB");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("GetUsers")]
    public async Task<IActionResult> GetUsers([FromQuery] PaginatedDTO paginatedDto)
    {
        try
        {
            return Ok(await _userService.GetUsers(paginatedDto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetUserById([FromRoute] int userId)
    {
        try
        {
            return Ok(await _userService.GetUserById(userId));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("{userId}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UpdateUserDTO dto)
    {
        try
        {
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
    public async Task<IActionResult> DeleteUser([FromRoute] int userId)
    {
        try
        {
            await _userService.DeleteUser(userId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}