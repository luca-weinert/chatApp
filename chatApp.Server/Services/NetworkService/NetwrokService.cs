using ChatApp.Server.Models.Connection;

namespace ChatApp.Server.Services.NetworkService;

public class NetworkService
{
    private ClientConnection ClientConnection { get; set; }

    public NetworkService(ClientConnection clientConnection)
    {
        ClientConnection = clientConnection;
    }
    
    public async Task<string> ListenAsync()
    {
        if (ClientConnection == null) throw new Exception("no connection to client established");
        var receivedData = await ClientConnection.ReadAsync();
        return receivedData;
    }

    public async Task WriteAsync(string data)
    {
        if (ClientConnection == null) throw new Exception("no connection to client established");
        Console.WriteLine($"[Client]: Sending Data: {data}");
        await ClientConnection.WriteAsync(data);
    }
}