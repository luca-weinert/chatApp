using ChatApp.Shared.Connection;

namespace chatApp_server.Listener
{
    public interface IListener
    {
        public Task ListenOnConnection(IConnection connection, CancellationToken cancellation);
    }
    
}
