using ChatApp.Client.Wpf.Services.Listener;
using ChatApp.Client.Wpf.Services.Message;
using ChatApp.Client.Wpf.Services.Network;
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