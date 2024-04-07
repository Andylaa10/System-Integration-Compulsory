using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthService.Core.Entities;
using AuthService.Core.Repositories.Interfaces;
using AuthService.Core.Services.Dtos;
using AuthService.Core.Services.Interfaces;
using Messaging;
using Messaging.SharedMessages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Core.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly MessageClient _messageClient;


    private const string SecurityKey =
        "mMdAhocQbIAa1/4iD8W5BiDCD9Lxg9ULp4qROgJVN8oRZommyAsnRalnNlzWGbKGJItr/kh2jVd2d9brhSBAJttV7NE47dvyX6n36cFKlnz3k9AodqqVgH/S52oQMYamtI+HsQqBmsvZMqOE+oGlEIzJG9tmDZ1JE/qJHq+bXo3RCEuBf26dGuIG4DWpjh+G4xTVC7ZoByCmq5zTUUyTlFZCQ2483iJe1Thkem9mlzt3cOy8O5SYJBafIb0xdIBYEoHl56Z805fO/W4eAw+M5stSCUdJTBUtWbCiId9zSapmilb20sCg4l5xYTsaJImTfHlo0t9kF1o/RXwr1cw3zCPoyt9tjWhZ83LMsi1ydBg=";


    public AuthService(IAuthRepository authRepository, MessageClient messageClient)
    {
        _authRepository = authRepository;
        _messageClient = messageClient;
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

        await _messageClient.Send(
            new CreateUser("Creating user", dto.Username, dto.Email, newAuth.PasswordHash, dto.FirstName, dto.LastName), "CreateUser");
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

    public async Task<AuthenticateResult> ValidateToken(string token)
    {
        if (token.IsNullOrEmpty()) return await Task.Run(() => AuthenticateResult.Fail("Invalid token"));

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecurityKey);
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true
            }, out var validatedToken);


            if (principal == null)
            {
                return AuthenticateResult.Fail("Invalid token");
            }

            var claims = new List<Claim>();
            foreach (var claim in principal.Claims)
            {
                claims.Add(new Claim(claim.Type, claim.Value));
            }

            var claimsIdentity = new ClaimsIdentity(claims, "dev");
            var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
            var ticket = new AuthenticationTicket(claimPrincipal, "dev");
            return AuthenticateResult.Success(ticket);
        }
        catch
        {
            return await Task.Run(() => AuthenticateResult.Fail("Invalid token"));
        }
    }


    private AuthenticationToken GenerateToken(Auth auth)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("Id", auth.Id.ToString()),
            new Claim("Email", auth.Email),
        };
        
        var tokenOptions = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.Now.AddMinutes(5)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        var authToken = new AuthenticationToken
        {
            Value = tokenString
        };

        return authToken;
    }
}