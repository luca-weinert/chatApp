using System.Collections.ObjectModel;
using ChatApp.Shared.Model.Message;

namespace ChatApp.Client.Wpf.MVVM.Model;

public class MessageModel
{
    private static MessageModel? _instance;
    private static readonly object LockObject = new();
    public static MessageModel GetInstance
    {
        get
        {
            lock (LockObject)
            {
                return _instance ??= new MessageModel();
            }
        }
    }
    
    private MessageModel()
    {
        Messages = new ObservableCollection<Message>();
    }
    public ObservableCollection<Message> Messages { get; set; }
}