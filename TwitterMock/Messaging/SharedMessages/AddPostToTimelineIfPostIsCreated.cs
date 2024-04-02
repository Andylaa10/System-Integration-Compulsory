namespace Messaging.SharedMessages;

public class AddPostToTimelineIfPostIsCreated
{
    public string Message { get; set; }
    public int PostId { get; set; }

    public AddPostToTimelineIfPostIsCreated(string message, int postId)
    {
        Message = message;
        PostId = postId;
    }
}