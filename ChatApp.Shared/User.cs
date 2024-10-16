namespace ChatApp.Shared;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { set; get; } = string.Empty;
}