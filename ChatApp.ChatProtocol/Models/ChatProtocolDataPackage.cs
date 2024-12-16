using Newtonsoft.Json;

namespace ChatApp.ChatProtocol.Models; 

public struct ChatProtocolDataPackage
{
    public ChatProtocolDataPackage(ChatProtocolPayloadTypes payloadType, string payload)
    {
        Timestamp = DateTime.UtcNow.ToString("o"); // ISO 8601 format
        ChatProtocolVersion = new Version(1, 0, 0);
        PayloadType = payloadType;
        Payload = payload;
    }

    public Version ChatProtocolVersion { get; init; }
    public string Timestamp { get; init; }
    public ChatProtocolPayloadTypes PayloadType { get; init; }
    public string Payload { get; init; }

    public string ToJson()
    {
        var json = JsonConvert.SerializeObject(this, Formatting.Indented);
        return json;
    }
}   