namespace chatApp_server.Connection;

public class ConnectionService : IConnectionService
{
    private readonly IConnectionRepository _repository;

    public ConnectionService(IConnectionRepository repository)
    {
        _repository = repository;
    }

    public Task AddConnection(Connection connection)
    {
        throw new NotImplementedException();
    }
}