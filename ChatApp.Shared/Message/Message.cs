namespace ChatApp.Shared;

public class Message
{
    public Message(Guid senderId, Guid targetId, string content)
    {
        SenderUserId = senderId;
        TargetUserId = targetId;
        Content = content;
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id;
    public Guid SenderUserId;
    public Guid TargetUserId;
    public DateTime CreatedAt;
    public string Content;
}