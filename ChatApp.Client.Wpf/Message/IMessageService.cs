﻿namespace ChatApp.Client.Wpf.Message;

public interface IMessageService
{
    public Task Send(Connection.Connection connection);
}