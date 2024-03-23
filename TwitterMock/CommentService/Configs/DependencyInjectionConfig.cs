using CommentService.Core.Helper;
using CommentService.Core.Repositories;
using CommentService.Core.Repositories.Interfaces;
using CommentService.Core.Services.Interfaces;
using EasyNetQ;
using Messaging;
using Microsoft.EntityFrameworkCore;

namespace CommentService.Configs;

public static class DependencyInjectionConfig
{
    public static void ConfigureDi(this IServiceCollection services)
    {
        services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));
        services.AddDbContext<DatabaseContext>(options =>
            options.UseInMemoryDatabase("CommentDb"));
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ICommentService, Core.Services.CommentService>();
        services.AddSingleton(AutoMapperConfig.ConfigureAutoMapper());
    }
}