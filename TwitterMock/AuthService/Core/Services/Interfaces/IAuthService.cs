using AuthService.Core.Services.Dtos;
using Microsoft.AspNetCore.Authentication;

namespace AuthService.Core.Services.Interfaces;

public interface IAuthService
{
    public Task Register(CreateAuthDto auth);
    public Task<AuthenticationToken> Login(LoginDto user);
    public Task<bool> ValidateToken(string token);
}