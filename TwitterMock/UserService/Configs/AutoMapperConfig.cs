using AutoMapper;

namespace UserService.Configs;

public static class AutoMapperConfig
{
    public static IMapper ConfigureAutoMapper()
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            
        });

        return mapperConfig.CreateMapper();
    }
}