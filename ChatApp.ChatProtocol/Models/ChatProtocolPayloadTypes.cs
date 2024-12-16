namespace ChatApp.ChatProtocol.Models;

public enum ChatProtocolPayloadTypes
{
    Message,
    User,
    MessageReceivedConfirmation,
    FileTransfer
}