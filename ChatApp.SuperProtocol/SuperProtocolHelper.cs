using System.Text.Json;

namespace ChatApp.SuperProtocol;

public static class SuperProtocolHelper
{
    public static string Serialize(SuperProtocolDataPackage superProtocolDataPackage)
    {
        var serializedData = JsonSerializer.Serialize(superProtocolDataPackage);
        return serializedData;
    }

    public static SuperProtocolDataPackage Deserialize(string rawData)
    {
        if (string.IsNullOrWhiteSpace(rawData))
        {
            throw new ArgumentException("Input data is null or empty.", nameof(rawData));
        }

        try
        {
            return JsonSerializer.Deserialize<SuperProtocolDataPackage>(rawData);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Deserialization failed: {ex.Message}");
            throw;
        }
    }
}