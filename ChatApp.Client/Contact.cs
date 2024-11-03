namespace ChatApp.Client.Wpf;

public class Contact
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
}