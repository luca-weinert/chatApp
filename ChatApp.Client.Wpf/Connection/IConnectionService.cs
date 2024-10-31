﻿using System.Net;

namespace ChatApp.Client.Wpf.Connection;

public interface IConnectionService
{ 
    public Task<Connection> ConnectToAsync(IPEndPoint endpoint);
}