using Newtonsoft.Json;

namespace ChatApp.ChatProtocol; 

public struct ChatProtocolDataPackage
{
    public ChatProtocolDataPackage(ChatProtocolPayloadTypes payloadType, object payload)
    {
        Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        PayloadType = payloadType;
        Payload = payload;
    }
    
    public string ChatProtocolVersion { get; init; } = "1.0.0.0";

    public string Timestamp { get; init; }

    public ChatProtocolPayloadTypes PayloadType { get; init; }

    public object Payload { get; init; }

    public string ToJson()
    {
        var json = JsonConvert.SerializeObject(this);
        return json;
    }
}   