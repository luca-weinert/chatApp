namespace ChatApp.Server.Models.Connection;

public interface IClientConnection
{ 
    public Guid Id { get; }
    public Guid UserId { get; set; }
    public Task WriteAsync(string rawMessage);
    public Task<string> ReadAsync();
}