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

            var url = "http://UserService/api/User/AddUser";
            var payload = JsonSerializer.Serialize(dto);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var result = await _client.PostAsync(url, content);

            if (result.IsSuccessStatusCode)
            {
                return StatusCode(201, "Successfully registered");
            }

            return BadRequest(result.RequestMessage);
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
    public async Task<bool> ValidateToken()
    {
        try
        {
            // checking for a valid token in the Authorization header
            var re = Request;

            if (!re.Headers.ContainsKey("Authorization"))
                return await Task.Run(() => false);

            if (!re.Headers["Authorization"].ToString().StartsWith("Bearer "))
                return await Task.Run(() => false);

            
            // decode the token & check if it's valid
            var token = re.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _authService.ValidateToken(token);
            return result.Succeeded;
        }
        catch (Exception e)
        {
            return await Task.Run(() => false);
        }
    }
}