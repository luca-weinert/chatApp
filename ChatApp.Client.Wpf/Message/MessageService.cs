using ChatApp.Client.Wpf.Communication;
using ChatApp.Client.Wpf.Connection;

namespace ChatApp.Client.Wpf.Message
{
    public class MessageService : IMessageService
    {
        private readonly ServerConnection _serverConnection;
        private readonly ICommunicationService _communicationService;
        
        public MessageService(ServerConnection serverConnection, ICommunicationService communicationService)
        {
            _serverConnection = serverConnection;
            _communicationService = communicationService;
        }
        
        public async Task SendAsync(Shared.Message.Message message)
        {
            var sendMessageEvent = _communicationService.CreateSendMessageEvent(message);
            await _communicationService.SendEventToServerAsync(sendMessageEvent);
        }
    }
}