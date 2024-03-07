using AutoMapper;
using UserService.Core.Entities;
using UserService.Core.Entities.Helper;
using UserService.Core.Repositories.Interfaces;
using UserService.Core.Services.Dtos;
using UserService.Core.Services.Helper;
using UserService.Core.Services.Interfaces;

namespace UserService.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;


    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentException("User repository cannot be null");
        _passwordHasher = passwordHasher ?? throw new ArgumentException("Password hasher cannot be null");
        _mapper = mapper ?? throw new ArgumentException("Automapper cannot be null");
    }


    public async Task<PaginatedResult<GetUserDTO>> GetUsers(PaginatedDTO dto)
    {
        return _mapper.Map<PaginatedResult<GetUserDTO>>(await _userRepository.GetUsers(dto.PageNumber, dto.PageSize));
    }

    public async Task<GetUserDTO> GetUser(int userId)
    {
        if (userId < 1) throw new ArgumentException("Id cannot be less than 1");
        return _mapper.Map<GetUserDTO>(await _userRepository.GetUserById(userId));
    }

    public async Task CreateUser(CreateUserDTO user)
    {
        if (user == null) throw new ArgumentException("User cannot be null");
        
        user.Password = _passwordHasher.HashPassword(user.Password);
        await _userRepository.AddUser(_mapper.Map<User>(user));
    }

    public Task UpdateUser(int userId, UpdateUserDTO user)
    {
        if (userId < 1) 
            throw new ArgumentException("Id cannot be less than 1");
        
        return _userRepository.UpdateUser(userId, user);
    }

    public Task DeleteUser(int userId)
    {
        throw new NotImplementedException();
    }
}