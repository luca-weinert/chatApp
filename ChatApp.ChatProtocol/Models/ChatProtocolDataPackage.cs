using Newtonsoft.Json;

namespace ChatApp.ChatProtocol.Models; 

public struct ChatProtocolDataPackage
{
    public ChatProtocolDataPackage(ChatProtocolPayloadTypes payloadType, string payload)
    {
        Timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
        PayloadType = payloadType;
        Payload = payload;
    }
    
    public string ChatProtocolVersion { get; init; } = "1.0.0.0";

    public string Timestamp { get; init; }

    public ChatProtocolPayloadTypes PayloadType { get; init; }

    public string Payload { get; init; }

    public string ToJson()
    {
        var json = JsonConvert.SerializeObject(this);
        return json;
    }
}   