namespace chatApp_server;

public class Message
{
    public string Target { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public string Sender { get; set; }
}