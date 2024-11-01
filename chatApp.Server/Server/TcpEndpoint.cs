using System.Net;
using System.Net.Sockets;
using chatApp_server.Connection;
using chatApp_server.Message;
using chatApp_server.User;

namespace chatApp_server.Server;

public class TcpEndpoint
{
    private readonly IUserService _userService;
    private readonly IConnectionService _connectionService;
    private readonly IMessageService _messageService;
    private readonly TcpListener _tcpListener;

    public TcpEndpoint(IUserService userService, IConnectionService connectionService, IMessageService messageService)
    {
        _userService = userService;
        _connectionService = connectionService;
        _messageService = messageService;
        _tcpListener = new TcpListener(IPAddress.Parse("192.168.178.45"), 8080);
    }
    
    
    public async Task StartAsync()
    {
        _tcpListener.Start();

        while (true)
        {
            Console.WriteLine("Waiting for connection...");
            var client = await _tcpListener.AcceptTcpClientAsync();
            Console.WriteLine("Client connected");
            await HandleClient(client);
        }
    }

    private async Task HandleClient(TcpClient client)
    {
        try
        {
            var connection = new ClientConnection(client);
            var user = await _userService.GetUserInformation(connection);
            connection.RelatedUserId = user.Id;
            await _connectionService.AddConnection(connection);
            Console.WriteLine($"User connected: id: {user.Id}, name: {user.Name}");

            await _messageService.HandleIncomingMessagesAsync(connection);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling client: {ex.Message}");
        }
        finally
        {
            // Ensure the client connection is closed properly
            client.Close();
        }
    }

    public void StopAsync(CancellationToken cancellationToken)
    {
        _tcpListener.Stop();
    }
}