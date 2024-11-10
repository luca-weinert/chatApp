using System.Net;
using System.Net.Sockets;
using chatApp_server.Connection;
using chatApp_server.Listener;

namespace chatApp_server.Endpoints;

public class TcpEndpoint : IEndpoint
{
    private readonly IConnectionService _connectionService;
    private readonly IConnectionRepository _connectionRepository;
    private readonly IListener _listener;
    private readonly TcpListener _tcpListener;
    
    public TcpEndpoint(
        IConnectionService connectionService,
        IConnectionRepository connectionRepository,
        IListener listener)
    {
        _connectionService = connectionService;
        _tcpListener = new TcpListener(IPAddress.Parse("192.168.178.45"), 8080);
        _connectionRepository = connectionRepository;
        _listener = listener;
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _tcpListener.Start();
        Console.WriteLine($"[Server]: started. Waiting for connections...");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var client = await _tcpListener.AcceptTcpClientAsync(cancellationToken);
                Console.WriteLine("[Server]: Client connected");
                _ = Task.Run(() => HandleClientAsync(client, cancellationToken), cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("[Server]: Cancellation requested. Server is stopping.");
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
            var clientConnection = await _connectionService.GetConnectionForClientAsync(client);
            await _connectionRepository.SaveConnectionAsync(clientConnection);
            var listenerTask = _listener.ListenOnConnection(clientConnection, cancellationToken);
            
            // do some stuff 
            
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