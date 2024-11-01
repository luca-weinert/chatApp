namespace ChatApp.Shared.Message
{
    public class Message
    {
        // Parameterless constructor for deserialization purposes
        public Message() { }

        public Message(Guid senderId, Guid targetId, string content)
        {
            SenderUserId = senderId;
            TargetUserId = targetId;
            Content = content;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; } // Read-only property
        public Guid SenderUserId { get; private set; } // Read-only property
        public Guid TargetUserId { get; private set; } // Read-only property
        public DateTime CreatedAt { get; private set; } // Read-only property
        public string Content { get; set; } // Allow content to be set outside if needed
    }
}