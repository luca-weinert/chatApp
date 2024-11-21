using System.Text.Json;
using chatApp_server.Events;
using ChatApp.Shared.Connection;
using ChatApp.SuperProtocol;

namespace chatApp_server.Listener
{
    public class Listener : IListener
    {
        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<UserEventArgs> UserReceived;
        protected virtual void OnMessageReceived(MessageEventArgs e) => MessageReceived?.Invoke(this, e);
        protected virtual void OnUserReceived(UserEventArgs e) => UserReceived?.Invoke(this, e);

        public async Task ListenOnConnection(IConnection connection, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var receivedData = await connection.ReadAsync(cancellationToken);
                    if (string.IsNullOrEmpty(receivedData)) continue;
                    HandleReceivedData(receivedData, connection);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private void HandleReceivedData(string data, IConnection connection)
        {
            Console.WriteLine($"[Server]: received data: {data}");
            var chatData = JsonSerializer.Deserialize<ChatAppDataPackage>(data);
            switch (chatData.DataType)
            {
                case ChatAppDataTypes.Message:
                    Console.WriteLine($"message!");
                    if (!string.IsNullOrWhiteSpace(chatData.Data))
                    {
                        var message = JsonSerializer.Deserialize<ChatApp.Shared.Message.Message>(chatData.Data);
                        if (message != null)
                        {
                            Console.WriteLine($"message: {message}");
                            OnMessageReceived(new MessageEventArgs(message));
                        }
                    }

                    break;
                case ChatAppDataTypes.User:
                    Console.WriteLine($"user!");
                    if (!string.IsNullOrWhiteSpace(chatData.Data))
                    {
                        var user = JsonSerializer.Deserialize<ChatApp.Shared.User.User>(chatData.Data);
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