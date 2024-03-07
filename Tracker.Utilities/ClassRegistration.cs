using Autofac;

namespace Tracker.Utilities;

public static class ClassRegistration
{
    public static ContainerBuilder Register(ContainerBuilder builder)
    {
        builder.RegisterType<CalenderService>().As<ICalenderService>().SingleInstance();

        return builder;
    }
}