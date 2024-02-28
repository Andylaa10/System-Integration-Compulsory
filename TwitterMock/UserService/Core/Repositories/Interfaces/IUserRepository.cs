using UserService.Core.Entities;
using UserService.Core.Entities.Helper;

namespace UserService.Core.Repositories.Interfaces;

public interface IUserRepository
{
    /// <summary>
    /// Retrieves Users in a paginated result
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public Task<PaginatedResult<User>> GetUsers(int pageNumber, int pageSize);

    /// <summary>
    /// Get user by Id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<User> GetUserById(int userId);
    
    /// <summary>
    /// Add user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task AddUser(User user);
    
    /// <summary>
    /// Updates user with a specific id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public Task UpdateUser(int userId, User user);
    
    /// <summary>
    /// Delete user with a specific id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task DeleteUser(int userId);
    
    /// <summary>
    /// Check you user exists
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public Task<bool> DoesUserExist(int userId);
}