using EasyNetQ;
using Messaging;
using Messaging.SharedMessages;
using UserService.Core.Services.Dtos;
using UserService.Core.Services.Interfaces;

namespace UserService.Core.Helper.MessageHandlers;

public class CreateUserHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public CreateUserHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private async void HandleCreateUser(CreateUser user)
    {
        Console.WriteLine(user.Message);

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            var dto = new CreateUserDTO
            {
                Email = user.Email,
                Password = user.Password,
                Username = user.Username,
                CreatedAt = DateTime.UtcNow,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            await userService.CreateUser(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ArgumentException("Something went wrong");
        }
        
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Message handler is running..");

        var messageClient = new MessageClient(
            RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")    
        );

        var topic = "CreateUser";
        await messageClient.Listen<CreateUser>(HandleCreateUser, topic);
    }
}