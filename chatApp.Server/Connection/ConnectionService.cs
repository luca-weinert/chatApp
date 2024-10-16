namespace chatApp_server.Connection;

public class ConnectionService : IConnectionService
{
    private readonly IConnectionRepository _connectionRepository;

    public ConnectionService(IConnectionRepository connectionRepository)
    {
        _connectionRepository = connectionRepository;
    }

    public async Task AddConnection(Connection connection)
    {
        await _connectionRepository.SaveConnectionAsync(connection);
    }
}