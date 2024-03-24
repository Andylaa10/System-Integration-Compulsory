namespace Messaging.SharedMessages;

public class DeleteCommentOnPostIfPostIsDeleted
{
    public string Message { get; set; }
    public int PostId { get; set; }

    public DeleteCommentOnPostIfPostIsDeleted(string message, int postId)
    {
        Message = message;
        PostId = postId;
    }
}