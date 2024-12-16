using ChatApp.ChatProtocol.Models;
using ChatApp.Server.Models.Connection;
using ChatApp.Shared.Events;
using ChatApp.Shared.Model.Message;
using ChatApp.Shared.Model.User;
using Newtonsoft.Json;

namespace ChatApp.Server.Services.ConnectionListenerService
{
    public sealed class ConnectionListenerService
    {
        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<MessageReceivedConformationEventArgs> MessageReceivedConfirmation;
        public event EventHandler<UserEventArgs> UserReceived;
        private void OnMessageReceived(MessageEventArgs e) => MessageReceived?.Invoke(this, e);
        private void OnMessageReceivedConfirmation(MessageReceivedConformationEventArgs e) => MessageReceivedConfirmation?.Invoke(this, e);
        private void OnUserReceived(UserEventArgs e) => UserReceived?.Invoke(this, e);

        private ClientConnection _clientConnection;

        public ConnectionListenerService(ClientConnection clientConnection)
        {
            _clientConnection = clientConnection;
        }

        public async Task ListenOnConnection(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var chatProtocolService = new ChatProtocolService.ChatProtocolService(_clientConnection);
                    var chatData = await chatProtocolService.ListenAsync();
                    HandleReceivedData(chatData);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private void HandleReceivedData(ChatProtocolDataPackage dataPackage)
        {
            Console.WriteLine($"[Server]: Received data: {dataPackage}");
            var chatProtocolPayload = dataPackage.Payload;
            switch (dataPackage.PayloadType)
            { 
                case ChatProtocolPayloadTypes.Message:
                    Console.WriteLine($"[Server]: Received data containing a message");
                    var message = JsonConvert.DeserializeObject<Message>(chatProtocolPayload) ;
                    if (message == null) return;
                    OnMessageReceived(new MessageEventArgs(message));
                    break;
                case ChatProtocolPayloadTypes.User:
                    Console.WriteLine($"[Server]: Received data containing a user");
                    var user = JsonConvert.DeserializeObject<User>(chatProtocolPayload) ;
                    if (user == null) return;
                    OnUserReceived(new UserEventArgs(user, _clientConnection.Id));
                    break;
                case ChatProtocolPayloadTypes.MessageReceivedConfirmation:
                    Console.WriteLine($"[Server]: Received data containing a message confirmation");
                    var messageReceivedConfirmation = JsonConvert.DeserializeObject<MessageReceivedConfirmation>(chatProtocolPayload);
                    if (messageReceivedConfirmation == null) return;
                    OnMessageReceivedConfirmation(new MessageReceivedConformationEventArgs(messageReceivedConfirmation));
                    break;
            }
        }
    }
}