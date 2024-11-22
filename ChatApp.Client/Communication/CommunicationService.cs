﻿using System.Net;
using System.Text.Json;
using System.Windows;
using ChatApp.Client.Wpf.Connection;
using ChatApp.Client.Wpf.Listener;
using ChatApp.Shared.Connection;
using ChatApp.SuperProtocol;

namespace ChatApp.Client.Wpf.Communication;

public class CommunicationService : ICommunicationService
{
    private readonly IConnectionService _connectionService;
    private readonly IListener _listener;
    private IConnection? _connection;

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
            _connection = await _connectionService.ConnectToServerAsync(ipEndPoint);
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
        var user = new Shared.User.User("Hans Peter");
        var serializedUser = JsonSerializer.Serialize(user);
        var chatAppUserDataPackage = new SuperProtocolDataPackage(SuperProtocolDataTypes.User, serializedUser);
        var serialize = SuperProtocol.SuperProtocol.Serialize(chatAppUserDataPackage);
        _connection?.WriteAsync(serialize);
        Console.WriteLine($"[Client]: Sent user to server");
        
        var message = new Shared.Message.Message(Guid.NewGuid(), user.Id, "this is a new message");
        var serializedMessage = JsonSerializer.Serialize(message);
        var chatAppMessageDataPackage = new SuperProtocolDataPackage(SuperProtocolDataTypes.Message, serializedMessage);
        var serializedChatApplicationData= SuperProtocol.SuperProtocol.Serialize(chatAppMessageDataPackage);
        _connection?.WriteAsync(serializedChatApplicationData);
        Console.WriteLine($"[Client]: Sent message to server");
    }
}