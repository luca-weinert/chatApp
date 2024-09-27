namespace ChatApp.Shared;

public class Message
{
    public Message(Guid messageId, Guid senderId, Guid targetId, DateTime createdAt, string content)
    {
        Id = messageId;
        SenderUserId = senderId;
        TargetUserId = targetId;
        createdAt = createdAt;
        Content = content;
    }

    public Guid Id; 
    public Guid SenderUserId { get; set; }
    public Guid TargetUserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Content { get; set; }
}