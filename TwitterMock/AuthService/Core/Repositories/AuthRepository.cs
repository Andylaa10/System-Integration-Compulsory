using AuthService.Core.Helper;
using AuthService.Core.Models;
using AuthService.Core.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Core.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly DatabaseContext _context;

    public AuthRepository(DatabaseContext context)
    {
        _context = context;
    }


    public async Task Register(Auth auth)
    {
        await _context.Auths.AddAsync(auth);
        await _context.SaveChangesAsync();
    }

    public async Task<Auth> GetAuthById(int authId)
    {
        var auth = await _context.Auths.FirstOrDefaultAsync(a => a.Id == authId);

        if (auth is null) throw new ArgumentException($"No auth with id of {authId}");

        return auth;
    }

    public async Task DeleteAuth(int authId)
    {
        var auth = await GetAuthById(authId);
        if (auth is null) throw new ArgumentException($"No auth with id of {authId}");
        _context.Auths.Remove(auth);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DoesAuthExists(string email)
    {
        var auth = await _context.Auths.FirstOrDefaultAsync(a => a.Email == email);
        if (auth != null || auth != default)
        {
            return true;
        }

        return false;
    }
}