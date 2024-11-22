namespace ChatApp.SuperProtocol; 
public struct SuperProtocolDataPackage
{
    public SuperProtocolDataPackage(SuperProtocolDataTypes dataType, string? data)
    {
        Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        DataType = dataType;
        Data = data;
    }
    
    public string PackageVersion { get; init; } = "1.0.0.0";

    public string Timestamp { get; init; }

    public SuperProtocolDataTypes DataType { get; init; }

    public string? Data { get; init; }
}   