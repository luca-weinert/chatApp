using System.Text.Json;
using ChatApp.ChatProtocol.Models;
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
                    var chatProtocolDataPackage = await _chatProtocolService.ListenAsync();
                    switch (chatProtocolDataPackage.PayloadType)
                    {
                        case ChatProtocolPayloadTypes.Message:
                            Console.WriteLine($"[Client]: received message");
                            var chatProtocolPayload = chatProtocolDataPackage.Payload;
                            // var message = (Message)chatProtocolPayload;
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
