namespace ChatApp.SuperProtocol; 
public struct ChatAppDataPackage
{
    public ChatAppDataPackage(ChatAppDataTypes dataType, string? data)
    {
        Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        DataType = dataType;
        Data = data;
    }
    
    public string PackageVersion { get; init; } = "1.0.0.0";

    public string Timestamp { get; init; }

    public ChatAppDataTypes DataType { get; init; }

    public string? Data { get; init; }
}   