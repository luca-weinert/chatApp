using ChatApp.ChatProtocol.Models;
using ChatApp.Server.Repositories.Connection;
using ChatApp.Shared.Events;
using ChatApp.Shared.Model.File;

namespace ChatApp.Server.Services.FileService;

public class FileService
{
    private readonly ConnectionRepository _connectionRepository;
    private const string TargetDirectory = @"C:\Users\igus\Documents\ReceivedFiles";

    public FileService()
    { 
       _connectionRepository = new ConnectionRepository();
    }
    
    public async void OnFileTransferReceived(object sender, FileTransferEventArgs fileTransferEvent)
    {
        var fileTransfer = fileTransferEvent.FileTransfer;

        var fileName = string.IsNullOrWhiteSpace(fileTransfer.FileName)
            ? "[Server]: unknown_file"
            : fileTransfer.FileName;
        var fullPath = Path.Combine(TargetDirectory, fileName);
        
        try
        {
            if (!Directory.Exists(TargetDirectory))
            {
                Directory.CreateDirectory(TargetDirectory);
                Console.WriteLine($"[Server]: Created directory: {TargetDirectory}");
            }

            await File.WriteAllBytesAsync(fullPath, fileTransfer.Content);
            Console.WriteLine($"[Server]: File transferred to: {fullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Server]: Failed to write file transfer: {ex}");
        }
        
        await SendFileToTargetClientAsync(fileTransfer);
    }

    private async Task SendFileToTargetClientAsync(FileTransfer fileTransfer)
    {
        var targetConnection = await _connectionRepository.GetConnectionByUserIdAsync(fileTransfer.TargetUserId);
        if (targetConnection == null) return;
        var chatProtocolService = new ChatProtocolService.ChatProtocolService(targetConnection);
        var package = new ChatProtocolDataPackage(ChatProtocolPayloadTypes.FileTransfer, fileTransfer.ToJson());
        await chatProtocolService.WriteProtocolDataAsync(package);
    }
}