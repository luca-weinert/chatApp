using ChatApp.Shared.Events;

namespace ChatApp.Server.Services.FileService;

public class FileService
{
    public void OnFileTransferReceived(object sender, FileTransferEventArgs e)
    {
        var fileTransfer = e.FileTransfer;
        const string targetDirectory = @"C:\Users\igus\Documents\ReceivedFiles";
        var fileName = string.IsNullOrWhiteSpace(fileTransfer.FileName)
            ? "unknown_file"
            : fileTransfer.FileName;
        var fullPath = Path.Combine(targetDirectory, fileName);

        try
        {
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
                Console.WriteLine($"[Server]: Created directory: {targetDirectory}");
            }

            File.WriteAllBytes(fullPath, fileTransfer.Content);
            Console.WriteLine($"[Server]: File transfered to: {fullPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Server]: Failed to write file transfer: {ex}");
        }
    }
}