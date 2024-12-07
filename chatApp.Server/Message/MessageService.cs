﻿using System.Text.Json;
using chatApp_server.Connection;
using chatApp_server.Events;
using ChatApp.SuperProtocol;

namespace chatApp_server.Message
{
    public class MessageService : IMessageService
    {
        private readonly IConnectionService _connectionService;
        
        public MessageService(IConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        public async void OnMessageReceived(object? sender, MessageEventArgs messageEventArgs)
        {
            Console.WriteLine("in message service");
            var message = messageEventArgs.Message;
            if (await _connectionService.isUserConnected(message.TargetUserId))
            {
                await SendMessage(message);
            }
        }

        private async Task SendMessage(ChatApp.Shared.Message.Message message)
        {
          var targetConnection = await _connectionService.GetConnectionByUserIdAsync(message.TargetUserId);
          var superProtocolPackage = new SuperProtocolDataPackage(SuperProtocolDataTypes.Message, JsonSerializer.Serialize(message));
          var serialized = SuperProtocolHelper.Serialize(superProtocolPackage);
          await targetConnection.WriteAsync(serialized);
        }
    }
}