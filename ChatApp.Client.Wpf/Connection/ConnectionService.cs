using System.Net;
using System.Net.Sockets;

namespace ChatApp.Client.Wpf.Connection
{
    public class ConnectionService : IConnectionService
    {
        public async Task<Shared.Connection?> ConnectToServerAsync(IPEndPoint serverEndPoint)
        {
            Console.WriteLine("Attempting to connect to the server...");

            try
            {
                var tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(serverEndPoint.Address, serverEndPoint.Port);

                Console.WriteLine("Connection established successfully.");
                return new Shared.Connection(tcpClient);
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"SocketException: Unable to connect to server at {serverEndPoint}. Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while connecting to server: {ex.Message}");
                throw;
            }
        }
    }
}