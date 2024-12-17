using ChatApp.ChatProtocol.Models;
using ChatApp.Server.Models.Connection;
using ChatApp.Shared.Events;
using ChatApp.Shared.Model.File;
using ChatApp.Shared.Model.Message;
using ChatApp.Shared.Model.User;
using Newtonsoft.Json;

namespace ChatApp.Server.Services.ConnectionListenerService
{
    public sealed class Listener
    {
        public event EventHandler<MessageEventArgs>? MessageReceived;
        public event EventHandler<UserEventArgs>? UserReceived;
        public event EventHandler<FileTransferEventArgs>? FileReceived;
        public event EventHandler<MessageReceivedConformationEventArgs>? MessageReceivedConfirmationReceived;
        
        private void OnMessageReceived(MessageEventArgs e) => MessageReceived?.Invoke(this, e);

        private void OnMessageReceivedConfirmationReceived(MessageReceivedConformationEventArgs e) =>
            MessageReceivedConfirmationReceived?.Invoke(this, e);

        private void OnFileTransferReceived(FileTransferEventArgs e) => FileReceived?.Invoke(this, e);

        private void OnUserInformationReceived(UserEventArgs e) => UserReceived?.Invoke(this, e);

        private ClientConnection ClientConnection {get; set;}

        public Listener(ClientConnection clientConnection)
        {
            ClientConnection = clientConnection;
        }

        public async Task ListenOnConnection(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var chatProtocolService = new ChatProtocolService.ChatProtocolService(ClientConnection);
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

        private void HandleReceivedData(ChatProtocolDataPackage? receivedPackage)
        {
            Console.WriteLine($"[Server]: Received data: {receivedPackage}");
            Action<string> handler = receivedPackage.PayloadType switch
            {
                ChatProtocolPayloadTypes.Message => HandleMessage,
                ChatProtocolPayloadTypes.User => HandleUser,
                ChatProtocolPayloadTypes.MessageReceivedConfirmation => HandleMessageReceivedConfirmation,
                ChatProtocolPayloadTypes.FileTransfer => HandleFileTransfer,
                _ => throw new Exception($"Unknown payload type {receivedPackage.PayloadType}")
            };
            handler(receivedPackage.Payload);
        }

        private static T? DeserializePayload<T>(string payload)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(payload);
            }
            catch (Exception e)
            {
                Console.WriteLine("$[Server]: Failed to deserialize payload]");
                throw;
            }
        }

        private void HandleUser(string payload)
        {
            var user = DeserializePayload<User>(payload);
            if (user == null) return;
            OnUserInformationReceived(new UserEventArgs(user, ClientConnection.Id));
        }

        private void HandleMessage(string payload)
        {
            var message = DeserializePayload<Message>(payload);
            if (message == null) return;
            OnMessageReceived(new MessageEventArgs(message));
        }

        private void HandleFileTransfer(string payload)
        {
            var fileTransfer = FileTransfer.FromJson(payload);
            if (fileTransfer == null) return;
            var fileTransferEvent = new FileTransferEventArgs(fileTransfer);
            OnFileTransferReceived(fileTransferEvent);
        }

        private void HandleMessageReceivedConfirmation(string payload)
        {
            var messageReceivedConfirmation = DeserializePayload<MessageReceivedConfirmation>(payload);
            if (messageReceivedConfirmation == null) return;
            var messageReceivedConfirmationEvent = new MessageReceivedConformationEventArgs(messageReceivedConfirmation);
            OnMessageReceivedConfirmationReceived(messageReceivedConfirmationEvent);
        }
    }
}