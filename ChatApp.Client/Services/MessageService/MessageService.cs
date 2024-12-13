﻿using ChatApp.ChatProtocol;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.Services.MessageService
{
    public class MessageService
    {
        private ChatProtocolService.ChatProtocolService _chatProtocolService;
        
        public MessageService()
        {
            _chatProtocolService = new ChatProtocolService.ChatProtocolService();
        }
        
        public async Task SendMessageAsync(Message message)
        {
            var superProtocolDataPackage = new ChatProtocolDataPackage(ChatProtocolPayloadTypes.Message, message);
            await _chatProtocolService.SendAsync(superProtocolDataPackage);
        }
    }
}