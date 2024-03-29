﻿using UserService.Core.Entities;
using UserService.Core.Entities.Helper;
using UserService.Core.Services.Dtos;

namespace UserService.Core.Services.Interfaces;

public interface IUserService
{
    

    public Task<PaginatedResult<GetUserDTO>> GetUsers(PaginatedDTO dto);
    
    public Task<GetUserDTO> GetUserById(int userId);
    public Task CreateUser(CreateUserDTO user);
    
    public Task UpdateUser(int userId, UpdateUserDTO user);
    
    public Task DeleteUser(int userId);
    
    
}