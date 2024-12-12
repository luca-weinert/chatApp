namespace ChatApp.Server.Endpoints;

public interface IEndpoint
{
    public Task StartAsync(CancellationToken cancellationToken);
    public Task StopAsync(CancellationToken cancellationToken);
}