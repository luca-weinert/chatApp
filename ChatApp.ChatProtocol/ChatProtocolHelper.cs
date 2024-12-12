using System.Text.Json;

namespace ChatApp.ChatProtocol;

public static class ChatProtocolHelper
{
    public static string Serialize(ChatProtocolDataPackage chatProtocolDataPackage)
    {
        var serializedData = JsonSerializer.Serialize(chatProtocolDataPackage);
        return serializedData;
    }

    public static ChatProtocolDataPackage Deserialize(string rawData)
    {
        if (string.IsNullOrWhiteSpace(rawData))
        {
            throw new ArgumentException("Input data is null or empty.", nameof(rawData));
        }

        try
        {
            return JsonSerializer.Deserialize<ChatProtocolDataPackage>(rawData);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Deserialization failed: {ex.Message}");
            throw;
        }
    }
}