namespace ChatApp.Communication.Events
{
    public class Event<T>
    {
        public EventType EventType { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public string Version { get; private set; }
        public T Payload { get; private set; } 

        public Event(EventType eventType, T payload)
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