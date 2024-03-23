using EasyNetQ;
using Messaging;
using Messaging.SharedMessages;

namespace UserService.Core.Helper.MessageHandlers;

public class NotifyUserAboutCommentsHandler : BackgroundService
{
    private readonly int _userId;

    public NotifyUserAboutCommentsHandler(int userId)
    {
        _userId = userId;
    }

    /// <summary>
    /// Right now we only print to the console, in the future we could maybe write to a db or something else
    /// I have test it and it works for now. 
    /// </summary>
    /// <param name="notifyUserAboutComments"></param>
    private void HandleNotifyUserAboutComments(NotifyUserAboutComments notifyUserAboutComments)
    {
        Console.WriteLine(notifyUserAboutComments.Message);
        Console.WriteLine($"Total comments count: {notifyUserAboutComments.CommentsCount}");
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Message handler is running..");

        var messageClient = new MessageClient(
            RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")    
        );

        var topic = $"CommentsOnPostCreatedByUserId_{_userId}";
        await messageClient.Listen<NotifyUserAboutComments>(HandleNotifyUserAboutComments, topic);
    }
}