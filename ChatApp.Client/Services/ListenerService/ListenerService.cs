using System.Text.Json;
using ChatApp.ChatProtocol;
using ChatApp.Client.Wpf.Models.Connection;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.Services.ListenerService
{
    public class ListenerService
    {
        private ChatProtocolService.ChatProtocolService _chatProtocolService;
        
        public ListenerService(ServerConnection serverConnection)
        {
            _chatProtocolService = new ChatProtocolService.ChatProtocolService();
        }
        
        public async Task ListenOnServerConnection(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var superProtocolDataPackage = await _chatProtocolService.ListenAsync();
                    switch (superProtocolDataPackage.PayloadType)
                    {
                        case ChatProtocolPayloadTypes.Message:
                            var message = (Message)superProtocolDataPackage.Payload;
                            Console.WriteLine($"[Client]: received message");
                            var serializedMessage = message.ToJson();
                            Console.WriteLine($"[Client]: serialized message: {serializedMessage}");
                            break;
                        default:
                            Console.WriteLine($"[Client]: received unknown message");
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
