using ChatApp.Client.Wpf.Services.Connection;

namespace ChatApp.Client.Wpf.Services.Listener
{
    public interface IListener
    {
        public Task ListenOnConnection(IServerConnection serverConnection, CancellationToken cancellation);
    }
    
}
