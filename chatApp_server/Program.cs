using chatApp_server;

var server = new TcpServer();
var listenerTask = server.ListenAsync();

while (true)
{
    if (server.ConnectedClients.Count <= 0) continue;
    Console.WriteLine("send a message to client");
    var message = Console.ReadLine();
    if (message != null) await server.SentMessage(server.ConnectedClients[0], message);
}
        
await listenerTask;