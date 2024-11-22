using ChatApp.Client.Wpf.Connection;
using ChatApp.Shared.Connection;

namespace ChatApp.Client.Wpf.Listener
{
    public interface IListener
    {
        public Task ListenOnConnection(IServerConnection serverConnection, CancellationToken cancellation);
    }
    
}
