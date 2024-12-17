using System.Net;
using System.Net.Sockets;
using ChatApp.Server.Models.Connection;
using ChatApp.Server.Repositories.Connection;
using ChatApp.Server.Services.ConnectionListenerService;

namespace ChatApp.Server.Services.TcpEndpointService;

public class TcpEndpoint
{
    private static readonly object Lock = new();
    private readonly TcpListener _tcpSocket;
    private static TcpEndpoint? _instance;
    private readonly ConnectionRepository _connectionRepository; 
    private readonly MessageService.MessageService _messageService;
    private readonly UserService.UserService _userService;
    private readonly FileService.FileService _fileService;
    
    private TcpEndpoint()
    {
        _tcpSocket = new TcpListener(IPAddress.Any, 8080);
        _userService = new UserService.UserService();
        _connectionRepository = new ConnectionRepository();
        _messageService = new MessageService.MessageService();
        _fileService = new FileService.FileService();
    }
    
    public static TcpEndpoint? GetTcpEndpoint()
    {
        lock (Lock)
        {
            return _instance ??= new TcpEndpoint();
        }
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

    private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
    {
        try
        {
            var clientConnection = new ClientConnection(client);
            await _connectionRepository.AddConnectionAsync(clientConnection);
            
            // start a background task that listen on the connection
            var listener = new Listener(clientConnection);
            
            listener.MessageReceived += _messageService.OnMessageReceived;
            listener.MessageReceivedConfirmationReceived += _messageService.OnMessageReceivedConfirmationReceived;
            listener.UserReceived += _userService.OnUserInformationReceived;
            listener.FileReceived += _fileService.OnFileTransferReceived;
            
            var listenerTask = listener.Listen(cancellationToken);
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