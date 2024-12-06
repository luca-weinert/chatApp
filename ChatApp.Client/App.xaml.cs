using System.Windows;
using ChatApp.Client.Wpf.MVVM.View;
using ChatApp.Client.Wpf.Services.Communication;
using ChatApp.Client.Wpf.Services.Connection;
using ChatApp.Client.Wpf.Services.Listener;
using ChatApp.Client.Wpf.Services.Message;
using Ninject;

namespace ChatApp.Client.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Iockernel.Initialze(new IocConfiguration());
            base.OnStartup(e);
           // ComposeObjects();
           // Current.MainWindow.Show();
        }
        

        private void ComposeObjects()
        {
            Current.MainWindow = Iockernel.Get<MainWindow>();
            Current.MainWindow.Title = "DI Test";
        }
    }
}