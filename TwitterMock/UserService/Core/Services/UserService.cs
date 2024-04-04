using AutoMapper;
using UserService.Core.Entities;
using UserService.Core.Entities.Helper;
using UserService.Core.Repositories.Interfaces;
using UserService.Core.Services.Dtos;
using UserService.Core.Services.Interfaces;

namespace UserService.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository ?? throw new ArgumentException("User repository cannot be null");
        _mapper = mapper ?? throw new ArgumentException("Automapper cannot be null");
    }
    
    public async Task<PaginatedResult<GetUserDTO>> GetUsers(PaginatedDTO dto)
    {
        return _mapper.Map<PaginatedResult<GetUserDTO>>(await _userRepository.GetUsers(dto.PageNumber, dto.PageSize));
    }

    public async Task<GetUserDTO> GetUserById(int userId)
    {
        if (userId < 1) throw new ArgumentException("Id cannot be less than 1");
        return _mapper.Map<GetUserDTO>(await _userRepository.GetUserById(userId));
    }

    public async Task CreateUser(CreateUserDTO user)
    {
        if (user == null) throw new ArgumentException("User cannot be null");
        
        await _userRepository.AddUser(_mapper.Map<User>(user));
    }

    public async Task UpdateUser(int userId, UpdateUserDTO user)
    {
        var doesUserExist = await _userRepository.DoesUserExist(userId);
        if (!doesUserExist) throw new ArgumentException("This user ID doesn't exist in the database");
        if (userId != user.Id) throw new ArgumentException("ID in the route is not the same as ID in the body");
        if (userId < 1) throw new ArgumentException("Id cannot be less than 1");
        
        await _userRepository.UpdateUser(userId, _mapper.Map<User>(user));
    }

    public async Task DeleteUser(int userId)
    {
        var user = await _userRepository.DoesUserExist(userId);

        if (!user) throw new ArgumentException($"No user with id of {userId}");

        await _userRepository.DeleteUser(userId);
    }
}