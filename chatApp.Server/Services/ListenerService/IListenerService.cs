using chatApp_server.Events;
using ChatApp.Shared.Connection;

namespace chatApp_server.Listener
{
    public interface IListenerService
    {
        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<UserEventArgs> UserReceived;
        public Task ListenOnConnection(IConnection connection, CancellationToken cancellationToken);
        
    }
    
}
