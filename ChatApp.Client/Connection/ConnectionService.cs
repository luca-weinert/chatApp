using System.Net;
using System.Net.Sockets;

namespace ChatApp.Client.Wpf.Connection
{
    public class ConnectionService : IConnectionService
    {
        public async Task<IServerConnection?> ConnectToServerAsync(IPEndPoint serverEndPoint)
        {
            Console.WriteLine("[Client]: Attempting to connect to the server...");

            try
            {
                var tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(serverEndPoint.Address, serverEndPoint.Port);
                Console.WriteLine("[Client]: Connection established successfully");
                return new ServerConnection(tcpClient);
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"[Client]: SocketException: Unable to connect to server at {serverEndPoint}. Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Client]: Unexpected error while connecting to server: {ex.Message}");
                throw;
            }
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}