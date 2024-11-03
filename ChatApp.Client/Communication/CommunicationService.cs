using System.Net;
using System.Text.Json;
using System.Windows;
using ChatApp.Client.Wpf.Connection;
using ChatApp.Client.Wpf.User;
using ChatApp.Communication.Event;
using ChatApp.Shared.Connection;
using ChatApp.Shared.Listener;

namespace ChatApp.Client.Wpf.Communication;

public class CommunicationService : ICommunicationService
{
    private readonly IConnectionService _connectionService;
    private readonly IAuthenticationService _authenticationService;
    private readonly IEventFactory _eventFactory;
    private readonly IListener _listener;
    private IConnection? _connection;

    public CommunicationService(IConnectionService connectionService,
        IAuthenticationService authenticationService, IEventFactory eventFactory, IListener listener)
    {
        _connectionService = connectionService;
        _eventFactory = eventFactory;
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

            var userInformation = await _authenticationService.GetUserInformationAsync();
            var userInformationEvent = _eventFactory.CreateUserInformationResponseEvent(userInformation);
            await SendEventToServer(userInformationEvent);
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

    public async Task SendEventToServer<T>(Event<T> eventToSend)
    {
        var serializedEvent = JsonSerializer.Serialize(eventToSend);
        if (_connection != null) await _connection.WriteAsync(serializedEvent);
    }
}