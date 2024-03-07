using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using AuthService.Core.Services.Dtos;
using AuthService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly HttpClient _client = new HttpClient();
    
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("Register")]
    public async Task<IActionResult> Register([FromBody] CreateAuthDto dto)
    {
        try
        {
            await _authService.Register(dto);
            
            // var url = "http://localhost:5221/api/User/AddUser";
            // var payload = JsonSerializer.Serialize(dto);
            // var content = new StringContent(payload, Encoding.UTF8, "application/json");
            // var result = await _client.PostAsync(url, content);
            //
            // if (result.IsSuccessStatusCode)
            // {
            //     return StatusCode(201, "Successfully registered");
            // }
            //
            // return BadRequest(result.RequestMessage);

            return StatusCode(201, "Successfully registered");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            return Ok(await _authService.Login(dto));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("ValidateToken")]
    public async Task<IActionResult> ValidateToken([FromHeader] string token)
    {
        try
        {
            return Ok(await _authService.ValidateToken(token));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}