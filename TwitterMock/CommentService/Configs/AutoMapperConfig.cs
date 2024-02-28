using AutoMapper;
using CommentService.Core.Entities;
using CommentService.Core.Entities.Dtos;

namespace CommentService.Configs;

public static class AutoMapperConfig
{
    public static IMapper ConfigureAutoMapper()
    {
        var mapperConfig = new MapperConfiguration(config =>
        {
            //DTO to entity
            config.CreateMap<AddCommentDto, Comment>();
            config.CreateMap<UpdateCommentDto, Comment>();
        });

        return mapperConfig.CreateMapper();
    }
}