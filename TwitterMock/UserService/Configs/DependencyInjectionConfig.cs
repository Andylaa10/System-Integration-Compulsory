using EasyNetQ;
using Messaging;
using Microsoft.EntityFrameworkCore;
using UserService.Core.Helper;
using UserService.Core.Helper.MessageHandlers;
using UserService.Core.Repositories;
using UserService.Core.Repositories.Interfaces;
using UserService.Core.Services.Interfaces;

namespace UserService.Configs;

public static class DependencyInjectionConfig
{
    public static void ConfigureDi(this IServiceCollection services, int userId)
    {
        services.AddSingleton(new MessageClient(RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")));
        services.AddHostedService(provider => new NotifyUserAboutCommentsHandler(userId));
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseInMemoryDatabase("UserDb");
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService.Core.Services.UserService>();
        services.AddSingleton(AutoMapperConfig.ConfigureAutoMapper());
        services.AddHostedService<CreateUserHandler>();
    }
}