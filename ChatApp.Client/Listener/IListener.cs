using ChatApp.Shared.Connection;

namespace ChatApp.Client.Wpf.Communication;

public interface IListener
{
    public Task ListenOnConnection(IConnection connection, CancellationToken cancellation);
}