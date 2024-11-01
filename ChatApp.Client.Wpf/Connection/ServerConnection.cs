using System;
using System.IO;
using System.Net.Sockets;

namespace ChatApp.Client.Wpf.Connection
{
    public class ServerConnection : IServerConnection
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public TcpClient Client { get; private set; }
        public Stream NetworkStream { get; private set; }

        public ServerConnection(TcpClient tcpClient)
        {
            Client = tcpClient;
            NetworkStream = tcpClient.GetStream(); // Initialize the NetworkStream based on the TcpClient
        }
    }
}