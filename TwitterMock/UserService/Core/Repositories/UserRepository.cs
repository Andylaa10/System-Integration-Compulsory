using Microsoft.EntityFrameworkCore;
using UserService.Core.Entities;
using UserService.Core.Entities.Helper;
using UserService.Core.Helper;
using UserService.Core.Repositories.Interfaces;

namespace UserService.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;

    public UserRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResult<User>> GetUsers(int pageNumber, int pageSize)
    {
        var users = await _context.Users
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .ToListAsync();

        var totalCount = await _context.Users.CountAsync();

        return new PaginatedResult<User>
        {
            Items = users,
            TotalCount = totalCount
        };
    }

    public async Task<User> GetUserById(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) throw new KeyNotFoundException($"No user with id of {userId}");
        return user;
    }

    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(int userId, User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);

        if (existingUser != null)
        {
            // Update user properties
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteUser(int userId)
    {
        var userToDelete = await _context.Users.FindAsync(userId);

        if (userToDelete != null)
        {
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> DoesUserExist(int userId)
    {
        return await _context.Users.AnyAsync(u => u.Id == userId);
    }
    
    
    
}