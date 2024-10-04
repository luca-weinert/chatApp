using System.Net;
using System.Net.Sockets;
using chatApp_server.Connection;
using chatApp_server.user;

namespace chatApp_server.Server;

public class Server(UserService userService, ConnectionService connectionService)
{
    private UserService _userService = userService;
    private readonly ConnectionService _connectionService = connectionService;
    private readonly TcpListener _tcpListener = new(IPAddress.Parse("127.0.0.1"), 8080);

    public async Task Start()
    {
        _connectionService.test();
        _tcpListener.Start();
        
        // Buffer for reading data
        var bytes = new Byte[256];
        String data = null;

        while (true)
        { 
            Console.WriteLine("Waiting for connection..."); 
            var client = await _tcpListener.AcceptTcpClientAsync();
            Console.WriteLine("Client connected");
            
            NetworkStream stream = client.GetStream();
            
            int i;

            // Loop to receive all the data sent by the client.
            while((i = stream.Read(bytes, 0, bytes.Length))!=0)
            {
                // Translate data bytes to a ASCII string.
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Console.WriteLine("Received: {0}", data);

                // Process the data sent by the client.
                data = data.ToUpper();

                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }
        }
    }

    public void Stop()
    {
        _tcpListener.Stop();
    }
}