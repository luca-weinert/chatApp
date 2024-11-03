using System.Text;
using System.Text.Json;

namespace chatApp_server.Message
{
    public class MessageService : IMessageService
    {
        public async Task HandleIncomingMessagesAsync(ChatApp.Shared.Connection clientConnection)
        {
            var buffer = new byte[1024];

            try
            {
                // Ensure the connection is still valid before reading
                if (clientConnection?.NetworkStream == null)
                {
                    throw new InvalidOperationException("The connection is not valid.");
                }

                var receivedBytes = await clientConnection.NetworkStream.ReadAsync(buffer);
                var receivedJson = Encoding.UTF8.GetString(buffer, 0, receivedBytes);

                // Use the correct variable name
                if (string.IsNullOrWhiteSpace(receivedJson))
                    throw new InvalidOperationException("Received data is empty or invalid.");

                Console.WriteLine($"Received message: {receivedJson}");
                
                var message = JsonSerializer.Deserialize<ChatApp.Shared.Message.Message>(receivedJson) ??
                              throw new InvalidOperationException("Failed to deserialize the message.");
                await ProcessMessageAsync(message);
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }

        public Task ProcessMessageAsync(ChatApp.Shared.Message.Message message)
        {
            throw new NotImplementedException();
        }

        public Task HandleOutgoingMessages(ChatApp.Shared.Message.Message message)
        {
            throw new NotImplementedException();
        }
    }
}