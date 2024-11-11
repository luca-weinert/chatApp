using chatApp_server.Events;
using ChatApp.Shared.Connection;

namespace chatApp_server.Listener
{
    public interface IListener
    {
        public event EventHandler<MessageEventArgs> MessageSend;
        public Task ListenOnConnection(IConnection connection, CancellationToken cancellation);
    }
    
}
