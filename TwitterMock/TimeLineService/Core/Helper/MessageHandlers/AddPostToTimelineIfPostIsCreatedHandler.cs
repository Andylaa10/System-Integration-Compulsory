using EasyNetQ;
using Messaging;
using Messaging.SharedMessages;
using TimeLineService.Core.Services.DTO;
using TimeLineService.Core.Services.Interfaces;

namespace TimeLineService.Core.Helper.MessageHandlers;

public class AddPostToTimelineIfPostIsCreatedHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public AddPostToTimelineIfPostIsCreatedHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private async void HandleAddPostToTimelineIfPostIsCreated(AddPostToTimelineIfPostIsCreated post)
    {
        Console.WriteLine(post.Message);

        try
        {
            using var scope = _serviceProvider.CreateScope();
            var timelineService = scope.ServiceProvider.GetRequiredService<ITimeLineService>();
            var dto = new AddToTimeLineDto
            {
                PostId = post.PostId,
            };
            await timelineService.AddToTimeLine(dto);
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

        const string topic = "AddPostToTimelineIfPostIsCreated";

        await messageClient.Listen<AddPostToTimelineIfPostIsCreated>(HandleAddPostToTimelineIfPostIsCreated, topic);
    }
}