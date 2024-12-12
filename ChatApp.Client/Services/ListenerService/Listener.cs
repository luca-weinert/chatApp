using System.Text.Json;
using ChatApp.ChatProtocol;
using ChatApp.Client.Wpf.Model.Connection;
using ChatApp.Client.Wpf.Services.NetworkService;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.Services.ListenerService
{
    public class Listener : IListener
    {
        private INetworkService _networkService;
        
        public Listener(INetworkService networkService)
        {
            _networkService = networkService;
        }
        
        public async Task HandleIncomingData(IServerConnection serverConnection, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var rawData = await _networkService.ListenAsync(); 
                    var superProtocolDataPackage = ChatProtocolHelper.Deserialize(rawData);
                    switch (superProtocolDataPackage.DataType)
                    {
                        case ChatProtocolDataTypes.Message:
                            var message = JsonSerializer.Deserialize<Message>(superProtocolDataPackage.Data);
                            Console.WriteLine($"[Client]: received message");
                            var serializedMessage = JsonSerializer.Serialize(message);
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

