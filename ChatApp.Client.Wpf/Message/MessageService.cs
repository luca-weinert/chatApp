using System.Text;
using System.Text.Json;
using ChatApp.Client.Wpf.Connection;

namespace ChatApp.Client.Wpf.Message
{
    public class MessageService : IMessageService
    {
        private readonly ServerConnection _serverConnection;

        public MessageService(ServerConnection serverConnection)
        {
            _serverConnection = serverConnection;
        }

        public async Task SendAsync(Shared.Message.Message message)
        {
            var serializedMessage = JsonSerializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(serializedMessage);
            await _serverConnection.NetworkStream.WriteAsync(bytes);
            Console.WriteLine($"Message sent to server: {message.Content}");
        }
    }
}