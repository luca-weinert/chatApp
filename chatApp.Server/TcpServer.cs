using System.Net;
using System.Net.Sockets;

namespace chatApp_server;

public class TcpServer(ConnectionHandler connectionHandler)
{
    private readonly TcpListener _tcpListener = new(IPAddress.Parse("192.168.178.45"), 8080);

    public async Task StartAsync()
    {
        _tcpListener.Start();
        Console.WriteLine("Server is running and ready for connections...");

        try
        {
            while (true)
            {
                var client = await _tcpListener.AcceptTcpClientAsync();
                var connection = new Connection(client);
                Console.WriteLine("New client connected.");

                // Handle the connection in a separate task
                _ = Task.Run(() => connectionHandler.HandleConnection(connection));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Server error: {e.Message}");
        }
        finally
        {
            _tcpListener.Stop();
        }
    }
}