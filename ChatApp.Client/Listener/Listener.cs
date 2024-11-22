﻿using System.Text.Json;
using ChatApp.Shared.Connection;
using ChatApp.SuperProtocol;

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
                    var rawData = await connection.ReadAsync(cancellationToken);
                    var superProtocolDataPackage = SuperProtocol.SuperProtocol.Deserialize(rawData);
                    switch (superProtocolDataPackage.DataType)
                    {
                        case SuperProtocolDataTypes.Message:
                            var message = JsonSerializer.Deserialize<Shared.Message.Message>(superProtocolDataPackage.Data);
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
