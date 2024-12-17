using System.IO;
using ChatApp.ChatProtocol.Models;
using ChatApp.Shared.Model.File;

namespace ChatApp.Client.Wpf.Services.FileService;

public class FileService
{
    private readonly ChatProtocolService.ChatProtocolService _chatProtocolService;
    private static FileService? _instance;
    private static readonly object Lock = new();
    public static FileService Instance
    {
        get
        {
            lock (Lock)
            {
                return _instance ??= new FileService();
            }
        }
    }
    
    
    private FileService()
    {
        _chatProtocolService = new ChatProtocolService.ChatProtocolService();
    }

    public async Task SendFileAsync(FileInfo fileInfo, Guid senderUserId, Guid targetUserId)
    {
        var fileBytes = await File.ReadAllBytesAsync(fileInfo.FullName);
        var fileTransfer = new FileTransfer(senderUserId, targetUserId, fileInfo.Name, fileBytes);
        var package = new ChatProtocolDataPackage(ChatProtocolPayloadTypes.FileTransfer, fileTransfer.ToJson());
        await _chatProtocolService.SendAsync(package);
    }
}