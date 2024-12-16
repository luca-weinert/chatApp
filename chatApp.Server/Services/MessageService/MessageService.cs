using ChatApp.ChatProtocol.Models;
using ChatApp.Server.Repositories.Connection;
using ChatApp.Shared.Events;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Server.Services.MessageService
{
    public class MessageService
    {
        private readonly ConnectionRepository _connectionRepository;

        public MessageService()
        {
            _connectionRepository = new ConnectionRepository();
        }

        public async void OnMessageReceived(object? sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            await SendMessage(message);
        }

        public async void OnMessageReceivedConfirmation(object? sender,
            MessageReceivedConformationEventArgs messageReceivedConformationEventArgs)
        {
            Console.WriteLine("OnMessageReceivedConfirmation");
            await SendMessageReceivedConfirmation(messageReceivedConformationEventArgs.ReceivedMessage);
        }

        public async Task SendMessageReceivedConfirmation(MessageReceivedConfirmation messageReceivedConfirmation)
        {
            Console.WriteLine("[Server]: Sending message received confirmation to sender client");
            var targetConnection =
                await _connectionRepository.GetConnectionByUserIdAsync(messageReceivedConfirmation.ReceivedMessage
                    .SenderUserId);
            if (targetConnection == null) return;
            var chatProtocolService = new ChatProtocolService.ChatProtocolService(targetConnection);
            var chatProtocolPackage = new ChatProtocolDataPackage(ChatProtocolPayloadTypes.MessageReceivedConfirmation,
                messageReceivedConfirmation.ReceivedMessage.ToJson());
            await chatProtocolService.SendAsync(chatProtocolPackage);
        }

        private async Task SendMessage(Message message)
        {
            Console.WriteLine("[Server]: Sending message to target client");
            var targetConnection = await _connectionRepository.GetConnectionByUserIdAsync(message.TargetUserId);
            if (targetConnection == null) return;
            var chatProtocolService = new ChatProtocolService.ChatProtocolService(targetConnection);
            var chatProtocolPackage =
                new ChatProtocolDataPackage(ChatProtocolPayloadTypes.Message, message.ToJson());
            await chatProtocolService.SendAsync(chatProtocolPackage);
        }
    }
}