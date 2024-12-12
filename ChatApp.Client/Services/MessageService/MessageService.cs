using System.Text.Json;
using System.Threading.Channels;
using ChatApp.ChatProtocol;
using ChatApp.Client.Wpf.Services.Network;

namespace ChatApp.Client.Wpf.Services.Message
{
    public class MessageService : IMessageService
    {
        private IChatProtocolService _chatProtocolService;
        
        public MessageService(IChatProtocolService chatProtocolService)
        {
            _chatProtocolService = chatProtocolService;
        }
        
        public async Task SendMessageAsync(Shared.Message.Message message)
        {
            var serializedMessage = JsonSerializer.Serialize(message);
            Console.WriteLine($"[Client] Message to send: {serializedMessage}");
            var superProtocolDataPackage = new ChatProtocolDataPackage(ChatProtocolDataTypes.Message, serializedMessage);
            await _chatProtocolService.SendAsync(superProtocolDataPackage);
        }
    }
}