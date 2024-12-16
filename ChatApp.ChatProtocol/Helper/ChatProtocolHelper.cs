using ChatApp.ChatProtocol.Models;
using Newtonsoft.Json;
using JsonException = System.Text.Json.JsonException;

namespace ChatApp.ChatProtocol.Helper;

public static class ChatProtocolHelper
{
    public static ChatProtocolDataPackage? Deserialize(string rawData)
    {
        try
        {
            return JsonConvert.DeserializeObject<ChatProtocolDataPackage>(rawData);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Deserialization failed: {ex.Message}");
            throw;
        }
    }
}