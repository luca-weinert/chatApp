using ICommunicationService = ChatApp.Client.Wpf.Communication.ICommunicationService;

namespace ChatApp.Client.Wpf.Message
{
    public class MessageService : IMessageService
    {
        private readonly ICommunicationService _communicationService;
        
        public MessageService(ICommunicationService communicationService)
        {
            _communicationService = communicationService;
        }
        
        public async Task SendMessageAsync(Shared.Message.Message message)
        {
        }
    }
}