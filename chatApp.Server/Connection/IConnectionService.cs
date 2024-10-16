namespace chatApp_server.Connection;

public interface IConnectionService
{
    public Task AddConnection(Connection connection);
}