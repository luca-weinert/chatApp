using Ninject;
using Ninject.Modules;

namespace ChatApp.Client.Wpf;

public static class Iockernel
{
    private static StandardKernel _kernel;

    public static T Get<T>()
    {
        return _kernel.Get<T>();
    }

    public static void Initialze(params INinjectModule[] modules)
    {
        if (_kernel == null)
        {
            _kernel = new StandardKernel(modules);
        }
    }
}