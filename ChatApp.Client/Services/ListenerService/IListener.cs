using ChatApp.Client.Wpf.Model.Connection;

namespace ChatApp.Client.Wpf.Services.Listener
{
    public interface IListener
    {
        public Task HandleIncomingData(IServerConnection serverConnection, CancellationToken cancellation);
    }
    
}
