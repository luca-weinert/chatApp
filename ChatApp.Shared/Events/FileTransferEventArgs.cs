using ChatApp.Shared.Model.File;

namespace ChatApp.Shared.Events;

public class FileTransferEventArgs : EventArgs
{
    public FileTransfer FileTransfer { get; private set; }

    public FileTransferEventArgs(FileTransfer fileTransfer)
    {
        FileTransfer = fileTransfer;
    }
}