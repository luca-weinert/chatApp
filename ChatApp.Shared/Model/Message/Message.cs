﻿using Newtonsoft.Json;

namespace ChatApp.Shared.Model.Message
{
    public class Message
    {

        public Message()
        {
            
        }
        
        public Message(Guid senderId, Guid targetId, string content)
        {
            SenderUserId = senderId;
            TargetUserId = targetId;
            Content = content;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
        
        public Guid Id { get; init; } 
        public Guid SenderUserId { get; init; } 
        public Guid TargetUserId { get; init; } 
        public DateTime CreatedAt { get; init; } 
        public string Content { get; init; }

        public string ToJson()
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            return json;
        }
    }
}