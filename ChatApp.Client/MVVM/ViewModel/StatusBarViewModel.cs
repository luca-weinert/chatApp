using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChatApp.Client.Wpf.Services.AuthenticationService;

namespace ChatApp.Client.Wpf.MVVM.ViewModel;

public class StatusBarViewModel : INotifyPropertyChanged
{
    public StatusBarViewModel()
    {
        var userService = AuthenticationService.Instance;
        UserName = userService.GetUser().Name;
        UserId = userService.GetUser().Id.ToString();
    }

    private string _userName;
    private string _userId;


    public string UserId
    {
        get => _userId;
        set
        {
            _userId = value;
            OnPropertyChanged(UserId);
        }
    }
    
    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;
            OnPropertyChanged(_userName);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}