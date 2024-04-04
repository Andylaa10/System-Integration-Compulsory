using CommentService.Core.Services.Interfaces;
using EasyNetQ;
using Messaging;
using Messaging.SharedMessages;

namespace CommentService.Core.Helper.MessageHandlers;

public class DeleteCommentOnPostIfPostIsDeletedHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DeleteCommentOnPostIfPostIsDeletedHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private async void HandleDeleteCommentOnPostIfPostIsDeleted(DeleteCommentOnPostIfPostIsDeleted deleted)
    {
        Console.WriteLine(deleted.Message);

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();
            await commentService.DeleteCommentsOnPost(deleted.PostId);
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

        var topic = "DeleteCommentsOnPostIfPostIsDeleted";

        await messageClient.Listen<DeleteCommentOnPostIfPostIsDeleted>(HandleDeleteCommentOnPostIfPostIsDeleted, topic);
    }
}