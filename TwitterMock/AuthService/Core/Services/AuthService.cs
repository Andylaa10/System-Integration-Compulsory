using AuthService.Core.Services.Dtos;
using AuthService.Core.Services.Interfaces;
using AutoMapper;

namespace AuthService.Core.Services;

public class AuthService : IAuthService
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthService(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
        _mapper = mapper;
    }

    public Task<TokenDto> Register(CreateAuthDto auth)
    {
        throw new NotImplementedException();
    }

    public Task<TokenDto> Login(LoginDto user)
    {
        throw new NotImplementedException();
    }

    public bool ValidateToken(string token)
    {
        throw new NotImplementedException();
    }
}