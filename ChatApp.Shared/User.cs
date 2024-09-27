namespace ChatApp.Shared;

public class User(string username)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { set; get; } = username;
}