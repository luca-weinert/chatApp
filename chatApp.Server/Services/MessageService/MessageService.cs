using ChatApp.ChatProtocol.Models;
using ChatApp.Server.Events;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Server.Services.MessageService
{
    public class MessageService
    {
        private readonly ConnectionService.ConnectionService _connectionService;
        
        public MessageService()
        {
            _connectionService = new ConnectionService.ConnectionService();
        }

        public async void OnMessageReceived(object? sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            if (await _connectionService.isUserConnected(message.TargetUserId))
            {
                await SendMessage(message);
            }
        }

        private async Task SendMessage(Message message)
        {
          var targetConnection = await _connectionService.GetConnectionByUserIdAsync(message.TargetUserId);
          var chatProtocolService = new ChatProtocolService.ChatProtocolService(targetConnection);
          var chatProtocolPackage = new ChatProtocolDataPackage(ChatProtocolPayloadTypes.Message, message.ToJson());
          await chatProtocolService.SendAsync(chatProtocolPackage);
        }
    }
}