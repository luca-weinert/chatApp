using ChatApp.Client.Wpf.Services.ChatProtocolService;
using ChatApp.Client.Wpf.Services.ListenerService;
using ChatApp.Client.Wpf.Services.MessageService;
using ChatApp.Client.Wpf.Services.NetworkService;
using Ninject.Modules;

namespace ChatApp.Client.Wpf;

public class IocConfiguration : NinjectModule
{
    public override void Load()
    {
        Bind<IListener>().To<Listener>().InSingletonScope();
        Bind<INetworkService>().To<NetworkService>().InSingletonScope();
        Bind<IChatProtocolService>().To<ChatProtocolService>().InSingletonScope(); 
        Bind<IMessageService>().To<MessageService>().InSingletonScope();
    }
}