﻿using ChatApp.Shared.Connection;

namespace ChatApp.Client.Wpf.Listener
{
    public interface IListener
    {
        public Task ListenOnConnection(IConnection connection, CancellationToken cancellation);
    }
    
}