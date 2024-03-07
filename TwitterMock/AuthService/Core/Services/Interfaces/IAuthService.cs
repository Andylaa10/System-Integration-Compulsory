using AuthService.Core.Services.Dtos;

namespace AuthService.Core.Services.Interfaces;

public interface IAuthService
{
    public Task Register(CreateAuthDto auth);
    public Task<TokenDto> Login(LoginDto user);
    public Task<bool> ValidateToken(string token);
}