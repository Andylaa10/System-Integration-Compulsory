using AuthService.Core.Services.Dtos;

namespace AuthService.Core.Services.Interfaces;

public interface IAuthService
{
    public Task<TokenDto> Register(CreateAuthDto auth);
    public Task<TokenDto> Login(LoginDto user);
    public bool ValidateToken(string token);
}