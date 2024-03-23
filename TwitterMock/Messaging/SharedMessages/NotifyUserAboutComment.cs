namespace Messaging.SharedMessages;

public class NotifyUserAboutComments
{
    public string Message { get; set; }
    public int CommentsCount { get; set; }

    public NotifyUserAboutComments(string message, int commentsCount)
    {
        Message = message;
        CommentsCount = commentsCount;
    }
}