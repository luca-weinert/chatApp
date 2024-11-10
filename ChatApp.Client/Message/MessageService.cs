using ChatApp.Client.Wpf.Event;
using ICommunicationService = ChatApp.Client.Wpf.Communication.ICommunicationService;

namespace ChatApp.Client.Wpf.Message
{
    public class MessageService : IMessageService
    {
        private readonly IEventFactory _eventFactory;
        private readonly ICommunicationService _communicationService;
        
        public MessageService(ICommunicationService communicationService, IEventFactory eventFactory)
        {
            _communicationService = communicationService;
            _eventFactory = eventFactory;
        }
        
        public async Task SendMessageAsync(Shared.Message.Message message)
        {
            var messageEvent = _eventFactory.CreateSendMessageEvent(message);
            await _communicationService.SendEventToServer<Shared.Message.Message>(messageEvent);
        }
    }
}