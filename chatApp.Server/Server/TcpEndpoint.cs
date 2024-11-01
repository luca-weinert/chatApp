using System.Net;
using System.Net.Sockets;
using chatApp_server.Communication;
using chatApp_server.Connection;

namespace chatApp_server.Server;

public class TcpEndpoint
{
    private readonly ICommunicationService _communicationService;
    private readonly IConnectionService _connectionService;
    private readonly IConnectionRepository _connectionRepository;
    private readonly TcpListener _tcpListener;

    public TcpEndpoint(
        IConnectionService connectionService,
        IConnectionRepository connectionRepository,
        ICommunicationService communicationService)
    {
        _connectionService = connectionService;
        _tcpListener = new TcpListener(IPAddress.Parse("192.168.178.45"), 8080);
        _communicationService = communicationService;
        _connectionRepository = connectionRepository;
    }


    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _tcpListener.Start();
        Console.WriteLine($"Server started. Waiting for connections...");

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var client = await _tcpListener.AcceptTcpClientAsync(cancellationToken);
                Console.WriteLine("Client connected");
                _ = Task.Run(() => HandleClientAsync(client, cancellationToken), cancellationToken);
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Cancellation requested. Server is stopping.");
        }

        catch (Exception e)
        {
            Console.WriteLine($"unexpected error: {e.Message}");
            throw;
        }
        finally
        {
            _tcpListener.Stop();
            Console.WriteLine("Server stopped");
        }
    }

    private async Task HandleClientAsync(TcpClient client, CancellationToken cancellationToken)
    {
        try
        {
            var clientConnection = await _connectionService.GetConnectionForClientAsync(client);
            await _connectionRepository.SaveConnectionAsync(clientConnection);
            await _communicationService.HandleCommunicationForAsync(clientConnection);
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Socket error with client: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error handling client: {ex.Message}");
        }
        finally
        {
            client.Close();
        }
    }

    public void StopAsync(CancellationToken cancellationToken)
    {
        _tcpListener.Stop();
        Console.WriteLine("tcpEndpoint stopped");
    }
}