using ChatApp.Shared.Connection;


namespace ChatApp.Shared.Listener
{
    public interface IListener
    {
        public Task ListenOnConnection(IConnection connection, CancellationToken cancellation);
    }
    
}
