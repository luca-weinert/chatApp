using System.Net;
using System.Text.Json;
using System.Windows;
using ChatApp.Client.Wpf.Connection;
using ChatApp.Client.Wpf.Listener;
using ChatApp.Client.Wpf.User;
using ChatApp.Shared.Connection;
using ChatApp.SuperProtocol;

namespace ChatApp.Client.Wpf.Communication;

public class CommunicationService : ICommunicationService
{
    private readonly IConnectionService _connectionService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IListener _listener;
    private IConnection? _connection;

    public CommunicationService(IConnectionService connectionService,
        IAuthenticationService authenticationService, IListener listener)
    {
        _connectionService = connectionService;
        _listener = listener;
        _authenticationService = authenticationService;
    }

    public async Task HandleCommunicationAsync()
    {
        if (await TryConnectToServerAsync())
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            _ = Task.Run(() => _listener.ListenOnConnection(_connection, token), token);
            
            SendChatDataToServer();
        }
        else
        {
            Console.WriteLine("[Client]: Could not establish a connection to the server.");
            MessageBox.Show("Could not establish a connection to the server ):");
        }
    }

    private async Task<bool> TryConnectToServerAsync()
    {
        try
        {
            var ipEndPoint = new IPEndPoint(IPAddress.Parse("192.168.178.45"), 8080);
            _connection = await _connectionService.GetConnectionAsync(ipEndPoint);
            return _connection != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Client]: Failed to connect to server - {ex.Message}");
            return false;
        }
    }
    
    public void SendChatDataToServer()
    {
        var user = new Shared.User.User("Luca Weinert");
        var serializedUser = JsonSerializer.Serialize(user);
        
        var chatAppUserDataPackage = new ChatAppDataPackage(ChatAppDataTypes.User, serializedUser);
        var serialize = JsonSerializer.Serialize(chatAppUserDataPackage);
        _connection?.WriteAsync(serialize);
        
        var message = new Shared.Message.Message(Guid.NewGuid(), Guid.NewGuid(), "this is a new message");
        var serializedMessage = JsonSerializer.Serialize(message);
        
        var chatAppMessageDataPackage = new ChatAppDataPackage(ChatAppDataTypes.Message, serializedMessage);
        var serializedChatApplicationData = JsonSerializer.Serialize(chatAppMessageDataPackage);
        _connection?.WriteAsync(serializedChatApplicationData);
    }
}