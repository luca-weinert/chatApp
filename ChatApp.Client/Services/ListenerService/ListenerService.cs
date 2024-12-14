using ChatApp.ChatProtocol.Models;
using ChatApp.Shared.Events;
using ChatApp.Shared.Model.Message;
using Newtonsoft.Json;
using JsonException = System.Text.Json.JsonException;

namespace ChatApp.Client.Wpf.Services.ListenerService
{
    public class ListenerService
    {
        private ChatProtocolService.ChatProtocolService _chatProtocolService;

        public event EventHandler<MessageEventArgs> MessageReceived;
        private void OnMessageReceived(MessageEventArgs e) => MessageReceived?.Invoke(this, e);

        public ListenerService()
        {
            _chatProtocolService = new ChatProtocolService.ChatProtocolService();
        }

        public async Task ListenOnServerConnection(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var dataPackage = await _chatProtocolService.ListenAsync();

                    Console.WriteLine($"[Client]: Received data: {dataPackage}");
                    var chatProtocolPayload = dataPackage.Payload;
                    switch (dataPackage.PayloadType)
                    {
                        case ChatProtocolPayloadTypes.Message:
                            Console.WriteLine($"[Client]: Received data containing a message");
                            var message = JsonConvert.DeserializeObject<Message>(chatProtocolPayload);
                            if (message == null) return;
                            OnMessageReceived(new MessageEventArgs(message));
                            break;
                    }
                }
            }
            catch (JsonException e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}