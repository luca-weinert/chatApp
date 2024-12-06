using ChatApp.Client.Wpf.Services.Communication;
using ChatApp.Client.Wpf.Services.Connection;
using ChatApp.Client.Wpf.Services.Listener;
using ChatApp.Client.Wpf.Services.Message;
using Ninject.Modules;

namespace ChatApp.Client.Wpf;

public class IocConfiguration : NinjectModule
{
    public override void Load()
    {
        Bind<IConnectionService>().To<ConnectionService>().InSingletonScope();
        Bind<IListener>().To<Listener>().InSingletonScope();
        Bind<ICommunicationService>().To<CommunicationService>().InSingletonScope();
        Bind<IMessageService>().To<MessageService>().InSingletonScope();
    }
}