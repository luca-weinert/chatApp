namespace ChatApp.Shared.Events
{
    public class Event<TPayloadType>
    {
        public EventType EventType { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public string Version { get; private set; }
        public TPayloadType Payload { get; private set; } 
        
        public Event(EventType eventType, TPayloadType payload)
        {
            EventType = eventType;
            TimeStamp = DateTime.UtcNow;
            Version = "1.0";
            Payload = payload;
        }
        
        public override string ToString()
        {
            return $"Action: {EventType}, TimeStamp: {TimeStamp:o}, Version: {Version}, Payload: {Payload}";
        }
    }
}