namespace ChatApp.Shared;

public class Message
{
    public string Sender { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public string Target { get; set; }
}