using Autofac;
using Tracker.UI.Controls;

namespace Tracker.UI;

public static class ClassRegistration
{
    public static ContainerBuilder Register(ContainerBuilder builder)
    {
        builder.RegisterType<WorkTimeTimerViewModel>().As<IWorkTimeTimerViewModel>().SingleInstance();

        return builder;
    }
}
