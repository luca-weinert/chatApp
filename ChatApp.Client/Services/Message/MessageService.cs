using ChatApp.SuperProtocol;
using ICommunicationService = ChatApp.Client.Wpf.Services.Communication.ICommunicationService;

namespace ChatApp.Client.Wpf.Services.Message
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
            var dataPackage = new SuperProtocolDataPackage(SuperProtocolDataTypes.Message, message.ToString());
            var serializedPackage = SuperProtocol.SuperProtocol.Serialize(dataPackage);
        }
    }
}