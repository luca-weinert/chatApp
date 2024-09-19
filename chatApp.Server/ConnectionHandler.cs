using System.Collections.Concurrent;
using System.Text;

namespace chatApp_server;

public class ConnectionHandler
{
    // Using a thread-safe collection to handle multiple client connections
    private readonly ConcurrentBag<Connection> _connections = new();

    public ConnectionHandler()
    {
    }

    public bool IsClientConnected(Connection connection)
    {
        return _connections.Contains(connection);
    }
    
    public async Task HandleConnection(Connection connection)
    {
        _connections.Add(connection);
        Console.WriteLine("Client added to connection list.");
        
        var buffer = new byte[1024];
        try
        {
            while (true)
            {
                var bytesRead = await connection.Stream.ReadAsync(buffer);
                if (bytesRead == 0)
                {
                    // Client disconnected gracefully
                    Console.WriteLine("Client disconnected.");
                    break;
                }

                var rawMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received raw message: {rawMessage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
        }
        finally
        {
            // Clean up after client disconnection
            _connections.TryTake(out _); // Removing the connection from the list
            connection.CLoseConnection();
            Console.WriteLine("Connection removed from connection list.");
        }
    }

    public Connection? GetConnection()
    {
        return _connections.FirstOrDefault();
    }
}