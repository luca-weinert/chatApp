using System.Net;
using System.Net.Sockets;
using ChatApp.Server.Models.Connection;
using ChatApp.Server.Repositories.Connection;

namespace ChatApp.Server.Services.TcpEndpointService;

public class TcpEndpoint
{
    private readonly TcpListener _tcpSocket;
    private static TcpEndpoint? _instance;

    private TcpEndpoint()
    {
        _tcpSocket = new TcpListener(IPAddress.Any, 8080);
    }
    
    public static TcpEndpoint? GetTcpEndpoint()
    {
        if (_instance == null)
        {
            _instance = new TcpEndpoint();
        }
        return _instance;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _tcpSocket.Start();
        Console.WriteLine($"[Server]: Server started and waiting for connections");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // waiting for incoming client connections
                var client = await _tcpSocket.AcceptTcpClientAsync(cancellationToken);
                Console.WriteLine("[Server]: A client connected to server");

                // handle current connection on separate task so the endpoint can handle other incoming connections  
                _ = Task.Run(() => HandleClientAsync(client, cancellationToken), cancellationToken);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"[Server]: unexpected error: {e.Message}");
            throw;
        }
        finally
        {
            _tcpSocket.Stop();
            Console.WriteLine("[Server]: Server stopped");
        }
    }

    private static async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
    {
        try
        {
            var connectionRepository = new ConnectionRepository();
            var clientConnection = new ClientConnection(client);
            await connectionRepository.AddConnectionAsync(clientConnection);
            
            // start a background task that listen on the connection
            var connectionListenerService = new ConnectionListenerService.Listener(clientConnection);
            var messageService = new MessageService.MessageService();
            var userService = new UserService.UserService(); 
            var fileService = new FileService.FileService();
            
            connectionListenerService.MessageReceived += messageService.OnMessageReceived;
            connectionListenerService.MessageReceivedConfirmationReceived += messageService.OnMessageReceivedConfirmationReceived;
            connectionListenerService.UserReceived += userService.OnUserInformationReceived;
            connectionListenerService.FileReceived += fileService.OnFileTransferReceived;
            
            
            var listenerTask = connectionListenerService.ListenOnConnection(cancellationToken);
            await listenerTask;
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"[Server]: Socket error with client: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Server]: Error handling client: {ex.Message}");
        }
        finally
        {
            client.Close();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _tcpSocket.Stop();
        Console.WriteLine("[Server]: tcpEndpoint stopped");
        return Task.CompletedTask;
    }
}