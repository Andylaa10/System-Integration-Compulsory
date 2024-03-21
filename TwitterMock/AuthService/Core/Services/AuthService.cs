using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthService.Core.Entities;
using AuthService.Core.Repositories.Interfaces;
using AuthService.Core.Services.Dtos;
using AuthService.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Core.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task Register(CreateAuthDto dto)
    {
        var auth = await _authRepository.DoesAuthExists(dto.Email);

        if (auth) throw new DuplicateNameException("Email is already in use");

        var saltBytes = new byte[32];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(saltBytes);


        var salt = Convert.ToBase64String(saltBytes);

        var newAuth = new Auth()
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password + salt),
            Salt = salt,
        };

        await _authRepository.Register(newAuth);
    }

    public async Task<AuthenticationToken> Login(LoginDto auth)
    {
        var loggedInUser = await _authRepository.GetAuthByEmail(auth.Email);
        if (loggedInUser == null) throw new Exception("Invalid login");

        if (BCrypt.Net.BCrypt.Verify(auth.Password + loggedInUser.Salt, loggedInUser.PasswordHash))
        {
            return GenerateToken(loggedInUser);
        }

        throw new Exception("Invalid login");
    }

    public async Task<bool> ValidateToken(string token)
    {
        if (token.IsNullOrEmpty())return await Task.Run(()=>false);;
        
        string secretKeyString  = "SuperSecret123!SuperSecret123!1234";
        
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString))
        };

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, validationParameters, out _);
            return await Task.Run(()=>true);
        }
        catch
        {
            return await Task.Run(()=>false);
        }
    }

    private AuthenticationToken GenerateToken(Auth auth)
    {
        const string secretKey1  = "SuperSecret123!SuperSecret123!1234";
        
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey1));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>
        {
        };
        
        var tokenOptions = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            claims: claims
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        var authToken = new AuthenticationToken
        {
            Value = tokenString,
        };

        return authToken;
    }
}