using AutoMapper;
using UserService.Core.Entities;
using UserService.Core.Entities.Helper;
using UserService.Core.Services.Dtos;

namespace UserService.Configs;

public static class AutoMapperConfig
{
    public static IMapper ConfigureAutoMapper()
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            //DTO to Entity
            config.CreateMap<CreateUserDTO, User>();
            config.CreateMap<UpdateUserDTO, User>();
            
            // Entity to DTO
            config.CreateMap<PaginatedResult<User>, PaginatedResult<GetUserDTO>>();
            config.CreateMap<User, GetUserDTO>();
        });

        return mapperConfig.CreateMapper();
    }
}