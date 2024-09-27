namespace chatApp_server.Connection;

public class ConnectionService
{
    private IConnectionRepository _repository;

    public ConnectionService(IConnectionRepository repository)
    {
        _repository = repository;
        Console.WriteLine("in connection service");
    }

    public void test()
    {
        Console.WriteLine("in test");
    }
    
    public async Task ManageConnection(Connection connection)
    {
        throw new NotImplementedException();
    }

    private bool IsUserWithUserIdConnected(Guid userId)
    {
        throw new NotImplementedException();
    }

    private Connection GetConnectionFromUserId(Guid userId)
    {
        throw new NotImplementedException();
    }
}