using System.IO;
using System.Net.Sockets;

namespace ChatApp.Client.Wpf;

public class Connection(TcpClient client, Stream networkStrean)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public TcpClient Client { get; set; } = client;
    public Stream NetworkStrean { get; set; } = networkStrean;
}