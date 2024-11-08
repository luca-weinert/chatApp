using ChatApp.Shared.Connection;

namespace ChatApp.Communication.Listener
{
    public interface IListener
    {
        public Task ListenOnConnection(IConnection connection, CancellationToken cancellation);
    }
    
}
