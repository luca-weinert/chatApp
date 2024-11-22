﻿using System.Net;
using System.Text.Json;
using System.Windows;
using ChatApp.Client.Wpf.Connection;
using ChatApp.Client.Wpf.Listener;
using ChatApp.SuperProtocol;

namespace ChatApp.Client.Wpf.Communication;

public class CommunicationService : ICommunicationService
{
    private readonly IConnectionService _connectionService;
    private readonly IListener _listener;
    private IServerConnection? _serverConnection;

    public CommunicationService(IConnectionService connectionService, IListener listener)
    {
        _connectionService = connectionService;
        _listener = listener;
    }

    public async Task HandleCommunicationAsync()
    {
        if (await TryConnectToServerAsync())
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            _ = Task.Run(() => _listener.ListenOnConnection(_serverConnection, token), token);
            
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
            _serverConnection = await _connectionService.ConnectToServerAsync(ipEndPoint);
            return _serverConnection != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Client]: Failed to connect to server - {ex.Message}");
            return false;
        }
    }
    
    public void SendChatDataToServer()
    {
        var user = new Shared.User.User("Hans Peter");
        var serializedUser = JsonSerializer.Serialize(user);
        var chatAppUserDataPackage = new SuperProtocolDataPackage(SuperProtocolDataTypes.User, serializedUser);
        _serverConnection?.WriteAsync(chatAppUserDataPackage);
        Console.WriteLine($"[Client]: Sent user to server");
        
        var message = new Shared.Message.Message(Guid.NewGuid(), user.Id, "this is a new message");
        var serializedMessage = JsonSerializer.Serialize(message);
        var chatAppMessageDataPackage = new SuperProtocolDataPackage(SuperProtocolDataTypes.Message, serializedMessage);
        _serverConnection?.WriteAsync(chatAppMessageDataPackage);
        Console.WriteLine($"[Client]: Sent message to server");
    }
}