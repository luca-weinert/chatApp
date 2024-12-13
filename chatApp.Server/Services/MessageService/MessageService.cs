using ChatApp.ChatProtocol;
using ChatApp.Server.Events;
using ChatApp.Server.Models.Connection;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Server.Services.MessageService
{
    public class MessageService
    {
        private readonly ClientConnection _clientConnection;
        private readonly ConnectionService.ConnectionService _connectionService;
        
        public MessageService(ClientConnection clientConnection)
        {
            _clientConnection = clientConnection;
            _connectionService = new ConnectionService.ConnectionService();
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
          var chatProtocolService = new ChatProtocolService.ChatProtocolService(targetConnection);
          var chatProtocolPackage = new ChatProtocolDataPackage(ChatProtocolPayloadTypes.Message, message);
          await chatProtocolService.SendAsync(chatProtocolPackage);
        }
    }
}