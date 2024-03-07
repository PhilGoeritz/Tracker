using Autofac;
using Tracker.Model.Repositories;

namespace Tracker.Model;

public static class ClassRegistration
{
    public static ContainerBuilder Register(ContainerBuilder builder)
    {
        builder.RegisterType<RunningSessionRepository>().As<IRunningSessionRepository>().SingleInstance();
        builder.RegisterType<TimeInstanceRepository>().As<ITimeInstanceRepository>().SingleInstance();
        builder.RegisterType<ActivityRepository>().As<IActivityRepository>().SingleInstance();

        return builder;
    }
}
