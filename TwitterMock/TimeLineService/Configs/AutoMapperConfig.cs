using AutoMapper;
using TimeLineService.Core.Entities;
using TimeLineService.Core.Services.DTO;


namespace TimeLineService.Configs;

public static class AutoMapperConfig
{
    public static IMapper ConfigureAutoMapper()
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<AddToTimeLineDto, TimeLine>();

        });

        return mapperConfig.CreateMapper();
    }
}