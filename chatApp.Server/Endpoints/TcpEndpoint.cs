using System.Net;
using System.Net.Sockets;
using chatApp_server.Repositorys.Connection;
using chatApp_server.Services.ConnectionService;
using chatApp_server.Services.ListenerService;

namespace chatApp_server.Endpoints;

public class TcpEndpoint : IEndpoint
{
    private readonly IConnectionService _connectionService;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IListenerService _iListenerService;
    private readonly TcpListener _tcpListener;

    public TcpEndpoint(
        IConnectionService connectionService,
        IConnectionRepository connectionRepository,
        IListenerService iListenerService)
    {
        _connectionService = connectionService;
        _tcpListener = new TcpListener(IPAddress.Any, 8080);
        _connectionRepository = connectionRepository;
        _iListenerService = iListenerService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _tcpListener.Start();
        Console.WriteLine($"[Server]: started. Waiting for connections...");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // waiting for incoming client connections
                var client = await _tcpListener.AcceptTcpClientAsync(cancellationToken);
                Console.WriteLine("[Server]: Client connected");

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
            _tcpListener.Stop();
            Console.WriteLine("[Server]: Server stopped");
        }
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
    {
        try
        {
            // save the current (active) connection in connection repository 
            var clientConnection = _connectionService.GetConnectionForClient(client);
            await _connectionRepository.SaveConnectionAsync(clientConnection);
            var listenerTask = _iListenerService.ListenOnConnection(clientConnection, cancellationToken);
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
        _tcpListener.Stop();
        Console.WriteLine("[Server]: tcpEndpoint stopped");
        return Task.CompletedTask;
    }
}