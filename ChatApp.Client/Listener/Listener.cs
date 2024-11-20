using System.Text.Json;
using ChatApp.Shared.Connection;

namespace ChatApp.Client.Wpf.Listener
{
    public class Listener : IListener
    {
        public async Task ListenOnConnection(IConnection connection, CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var receivedData = await connection.ReadAsync(cancellationToken);
                    Console.WriteLine($"[Listener]: received data: {receivedData}");
                    if (string.IsNullOrEmpty(receivedData)) continue;
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

