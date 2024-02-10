using Autofac;

namespace Tracker.Logic;

public static class ClassRegistration
{
    public static ContainerBuilder Register(ContainerBuilder builder)
    {
        builder.RegisterType<SessionService>().As<ISessionService>().SingleInstance();
        
        return builder;
    }
}
