using AuthService.Core.Models;

namespace AuthService.Core.Repositories.Interfaces;

public interface IAuthRepository
{
    public Task Register(Auth auth);

    public Task<Auth> GetAuthById(int authId);

    public Task DeleteAuth(int authId);

    public Task<bool> DoesAuthExists(string email);
    
}