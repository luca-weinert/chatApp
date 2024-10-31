namespace chatApp_server.Message;

public interface IMessageService
{
    public Task HandleIncomingMessages(Connection.Connection connection);
}