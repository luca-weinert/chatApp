using chatApp_server.Events;
using ChatApp.Shared.Model.Connection;

namespace chatApp_server.Services.ListenerService
{
    public interface IListenerService
    {
        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<UserEventArgs> UserReceived;
        public Task ListenOnConnection(IConnection connection, CancellationToken cancellationToken);
        
    }
    
}
