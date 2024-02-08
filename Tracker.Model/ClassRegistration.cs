using Autofac;

namespace Tracker.Model;

public static class ClassRegistration
{
    public static ContainerBuilder RegisterModel(ContainerBuilder builder)
    {
        builder.RegisterType<RunningSessionRepository>().As<IRunningSessionRepository>().SingleInstance();
        builder.RegisterType<TimeInstanceRepository>().As<ITimeInstanceRepository>().SingleInstance();

        return builder;
    }
}
