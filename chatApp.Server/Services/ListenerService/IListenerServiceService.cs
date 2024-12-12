using System.Text.Json;
using chatApp_server.Events;
using ChatApp.ChatProtocol;
using ChatApp.Shared.Connection;

namespace chatApp_server.Listener
{
    public sealed class IListenerServiceService : IListenerService
    {
        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<UserEventArgs> UserReceived;
        private void OnMessageReceived(MessageEventArgs e) => MessageReceived?.Invoke(this, e);
        private void OnUserReceived(UserEventArgs e) => UserReceived?.Invoke(this, e);

        public async Task ListenOnConnection(IConnection connection, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var rawData = await connection.ReadAsync(cancellationToken);
                    var chatData = ChatProtocolHelper.Deserialize(rawData);
                    HandleReceivedData(chatData, connection);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private void HandleReceivedData(ChatProtocolDataPackage dataPackage, IConnection connection)
        {
            Console.WriteLine($"[Server]: received data: {dataPackage}");
            switch (dataPackage.DataType)
            {
                case ChatProtocolDataTypes.Message:
                    Console.WriteLine($"message!");
                    if (!string.IsNullOrWhiteSpace(dataPackage.Data))
                    {
                        var message = JsonSerializer.Deserialize<ChatApp.Shared.Message.Message>(dataPackage.Data);
                        if (message != null)
                        {
                            Console.WriteLine($"message: {message}");
                            OnMessageReceived(new MessageEventArgs(message));
                        }
                    }

                    break;
                case ChatProtocolDataTypes.User:
                    Console.WriteLine($"user!");
                    if (!string.IsNullOrWhiteSpace(dataPackage.Data))
                    {
                        var user = JsonSerializer.Deserialize<ChatApp.Shared.User.User>(dataPackage.Data);
                        if (user != null)
                        {
                            Console.WriteLine($"user: {user}");
                            OnUserReceived(new UserEventArgs(connection, user));
                        }
                    }

                    break;
            }
        }
    }
}