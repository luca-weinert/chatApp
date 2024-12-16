using ChatApp.ChatProtocol.Models;
using ChatApp.Shared.Events;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.Services.MessageService
{
    public class MessageService
    {
        private ChatProtocolService.ChatProtocolService _chatProtocolService;
        private static MessageService _instance;
        private bool lastMessageReceivedByTarget = true;

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

        public async void OnMessageReceived(object? sender, MessageEventArgs messageEventArgs)
        {
            var receivedMessage = messageEventArgs.Message;
            Console.WriteLine($"[Client]: Received message: {receivedMessage.ToJson()}");
            await SendMessageReceivedConfirmation(receivedMessage);
            MessageReceived?.Invoke(sender, messageEventArgs);
        }

        public async void OnMessageReceivedConfirmationReceived(object? sender,
            MessageReceivedConformationEventArgs messageReceivedConformationEventArgs)
        {
            lastMessageReceivedByTarget = true;
            Console.WriteLine($"[Client]: received message received confirmation");
        }

        private async Task SendMessageReceivedConfirmation(Message receivedMessage)
        {
            var messageReceivedConfirmation = new MessageReceivedConfirmation(receivedMessage);
            var chatProtocolDataPackage =
                new ChatProtocolDataPackage(ChatProtocolPayloadTypes.MessageReceivedConfirmation,
                    messageReceivedConfirmation.ToJson());
            await _chatProtocolService.SendAsync(chatProtocolDataPackage);
        }

        public async Task SendMessageAsync(Message message)
        {
            if (lastMessageReceivedByTarget)
            {
                lastMessageReceivedByTarget = false;
                var chatProtocolDataPackage =
                    new ChatProtocolDataPackage(ChatProtocolPayloadTypes.Message, message.ToJson());
                await _chatProtocolService.SendAsync(chatProtocolDataPackage);
            }
            else
            {
                Console.WriteLine(
                    $"[Client]: The last message was not received by the target client. For this reason, the message will not be sent.");
            }
        }
    }
}