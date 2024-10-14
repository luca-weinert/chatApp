namespace ChatApp.Client.Wpf;

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Contact> _contacts;

    public ObservableCollection<Contact> Contacts
    {
        get => _contacts;
        set
        {
            _contacts = value;
            OnPropertyChanged();
        }
    }

    public MainWindowViewModel()
    {
        Contacts = new ObservableCollection<Contact>
        {
            new Contact { Name = "Luca" },
            new Contact { Name = "Peter" },
            new Contact { Name = "Hans" }
        };
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
