using ChatApp.ChatProtocol.Models;
using ChatApp.Shared.Events;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.Services.MessageService
{
    public class MessageService
    {
        private ChatProtocolService.ChatProtocolService _chatProtocolService;
        private static MessageService _instance;
        private static object _instanceLock = new object();

        private MessageService()
        {
            _chatProtocolService = new ChatProtocolService.ChatProtocolService();
        }

        public static MessageService Instance
        {
            get
            {
                lock (_instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new MessageService();
                    }

                    return _instance;
                }
            }
        }

        public event EventHandler<MessageEventArgs> MessageReceived;

        public void OnMessageReceived(object? sender, MessageEventArgs messageEventArgs)
        {
            var receivedMessage = messageEventArgs.Message;
            Console.WriteLine($"[Client]: Received message: {receivedMessage.ToJson()}");
            MessageReceived?.Invoke(sender, messageEventArgs);
        }

        public async Task SendMessageAsync(Message message)
        {
            var chatProtocolDataPackage =
                new ChatProtocolDataPackage(ChatProtocolPayloadTypes.Message, message.ToJson());
            await _chatProtocolService.SendAsync(chatProtocolDataPackage);
        }
    }
}