using ChatApp.Client.Wpf.Communication;
using ChatApp.Client.Wpf.Connection;
using ChatApp.Communication;

namespace ChatApp.Client.Wpf.Message
{
    public class MessageService : IMessageService
    {
        private readonly ICommunicationService _communicationService;
        private readonly IEventFactory _eventFactory;
        
        public MessageService(ICommunicationService communicationService, IEventFactory eventFactory)
        {
            _communicationService = communicationService;
            _eventFactory = eventFactory;
        }
        
        public async Task SendAsync(Shared.Message.Message message)
        {
            var sendMessageEvent = _eventFactory.CreateSendMessageEvent(message);
            await _communicationService.SendEventToServerAsync(sendMessageEvent);
        }
    }
}