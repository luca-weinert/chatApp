using ChatApp.Server.Services.TcpEndpointService;

namespace ChatApp.Server;

internal static class Program
{
    private static readonly TcpEndpoint? TcpEndpoint = TcpEndpoint.GetTcpEndpoint();
    
    public static async Task Main(string[] args)
    {
        var cts = new CancellationTokenSource();
        await TcpEndpoint?.StartAsync(cts.Token)!;
    }
}