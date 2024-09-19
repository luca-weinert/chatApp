using System.Net.Sockets;
using ChatApp.Shared;

namespace chatApp_client;

class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var client = new TcpClient("192.168.8.61", 8080);
            var serverConnection = new TcpConnection(client);
            var messageHandler = new MessageHandler(serverConnection); 
            
            // Start listening to messages from the server
            var listenToMessagesFromServer = serverConnection.ReceiveMessageAsync();

            // Continuously read input and send messages to the server
            while (true)
            {
                try
                {
                    Console.WriteLine("type your message:");
                    var messageText = Console.ReadLine();
                    
                    if (string.IsNullOrWhiteSpace(messageText)) continue;
                    var message = new Message
                    {
                        Target = "server",
                        Content = messageText,
                        Date = DateTime.Now,
                        Sender = "client"
                    };
                    
                    await messageHandler.SendMessageAsync(message);
                    await Task.Delay(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error sending message: {e.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Connection error: {ex.Message}");
        }
    }
}