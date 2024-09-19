using chatApp_server;
using ChatApp.Shared;

var connectionHandler = new ConnectionHandler();
var tcpServer = new TcpServer(connectionHandler);
var messageHandler = new MessageHandler(connectionHandler);

// Start the TCP server in a background task
var tcpServerTask = tcpServer.StartAsync();

while (true)
{
    var connection = connectionHandler.GetConnection();
    if (connection == null) continue;

    Console.WriteLine("Send a message to the client:");
    var messageContent = Console.ReadLine();

    var message = new Message()
    {
        Content = messageContent,
        Date = DateTime.Now,
        Sender = "Server",
        Target = "Client"
    };

    // Send the message to the client
    await messageHandler.SendAsync(message, connection);
}

await tcpServerTask;