using System.Text.Json;
using ChatApp.ChatProtocol;
using ChatApp.Server.Events;
using ChatApp.Server.Services.ConnectionService;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Server.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IConnectionService _connectionService;
        
        public MessageService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async void OnMessageReceived(object? sender, MessageEventArgs messageEventArgs)
        {
            Console.WriteLine("in message service");
            var message = messageEventArgs.Message;
            if (await _connectionService.isUserConnected(message.TargetUserId))
            {
                await SendMessage(message);
            }
        }

        private async Task SendMessage(Message message)
        {
          var targetConnection = await _connectionService.GetConnectionByUserIdAsync(message.TargetUserId);
          var superProtocolPackage = new ChatProtocolDataPackage(ChatProtocolDataTypes.Message, JsonSerializer.Serialize(message));
          var serialized = ChatProtocolHelper.Serialize(superProtocolPackage);
          await targetConnection.WriteAsync(serialized);
        }
    }
}