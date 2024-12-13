using ChatApp.ChatProtocol.Models;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.Services.MessageService
{
    public class MessageService
    {
        private ChatProtocolService.ChatProtocolService _chatProtocolService;
        
        public MessageService()
        {
            _chatProtocolService = new ChatProtocolService.ChatProtocolService();
        }
        
        public async Task SendMessageAsync(Message message)
        {
            var chatProtocolDataPackage = new ChatProtocolDataPackage(ChatProtocolPayloadTypes.Message, message.ToJson());
            await _chatProtocolService.SendAsync(chatProtocolDataPackage);
        }
    }
}