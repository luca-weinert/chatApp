using System.Text.Json;
using ChatApp.ChatProtocol;
using ChatApp.Client.Wpf.Services.ChatProtocolService;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private IChatProtocolService _chatProtocolService;
        
        public MessageService(IChatProtocolService chatProtocolService)
        {
            _chatProtocolService = chatProtocolService;
        }
        
        public async Task SendMessageAsync(Message message)
        {
            var serializedMessage = JsonSerializer.Serialize(message);
            Console.WriteLine($"[Client] Message to send: {serializedMessage}");
            var superProtocolDataPackage = new ChatProtocolDataPackage(ChatProtocolDataTypes.Message, serializedMessage);
            await _chatProtocolService.SendAsync(superProtocolDataPackage);
        }
    }
}