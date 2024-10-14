using System.Net.Sockets;

namespace ChatApp.Client.Wpf;

public abstract class TcpConnector
{
    public TcpConnector()
    {
    }

    public static void ConnectToServer(string message)
    {
        try
        {
            var tcpClient = new TcpClient("192.168.21.2", 8080);
            var stream = tcpClient.GetStream();
            var data = System.Text.Encoding.ASCII.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Console.WriteLine($"sent message to server: {message}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}