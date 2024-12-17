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
            if (messageEventArgs?.Message == null) return;
            await SendMessageToTargetClient(messageEventArgs.Message);
        }

        public async void OnMessageReceivedConfirmationReceived(object? sender,
            MessageReceivedConformationEventArgs messageReceivedConformationEventArgs)
        {
            Console.WriteLine("OnMessageReceivedConfirmation");
            await SendMessageReceivedConfirmationToSenderClient(messageReceivedConformationEventArgs.ReceivedMessage);
        }

        private async Task SendMessageReceivedConfirmationToSenderClient(
            MessageReceivedConfirmation messageReceivedConfirmation)
        {
            Console.WriteLine("[Server]: Sending message received confirmation to sender client");
            await TrySendDataToTargetClientAsync(messageReceivedConfirmation.ReceivedMessage.SenderUserId,
                ChatProtocolPayloadTypes.MessageReceivedConfirmation,
                messageReceivedConfirmation.ReceivedMessage.ToJson());
        }

        private async Task SendMessageToTargetClient(Message message)
        {
            Console.WriteLine("[Server]: Attempting to send message to target client...");
            await TrySendDataToTargetClientAsync(message.TargetUserId, ChatProtocolPayloadTypes.Message,
                message.ToJson());
        }

        private async Task<bool> TrySendDataToTargetClientAsync(Guid targetUserId,
            ChatProtocolPayloadTypes chatProtocolPayloadType, string JsonData)
        {
            var targetConnection = await _connectionRepository.GetConnectionByUserIdAsync(targetUserId);
            if (targetConnection == null)
            {
                Console.WriteLine($"[Server]: No active connection found for UserId: {targetUserId}");
                return false;
            }

            var chatProtocolService = new ChatProtocolService.ChatProtocolService(targetConnection);
            var chatProtocolPackage = new ChatProtocolDataPackage(chatProtocolPayloadType, JsonData);
            await chatProtocolService.WriteProtocolDataAsync(chatProtocolPackage);
            Console.WriteLine($"[Server]: Data sent successfully to UserId: {targetUserId}");
            return true;
        }
    }
}