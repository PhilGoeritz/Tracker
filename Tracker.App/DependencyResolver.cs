using System;
using Autofac;

namespace Tracker.App;

public interface IDependencyResolver : IDisposable
{
    T Resolve<T>() where T : notnull;
}

public sealed class DependencyResolver : IDependencyResolver
{
    private readonly IContainer _container;

    public DependencyResolver()
    {
        var builder = new ContainerBuilder();
        builder = Model.ClassRegistration.Register(builder);
        builder = Logic.ClassRegistration.Register(builder);
        builder = UI.ClassRegistration.Register(builder);
        builder = ClassRegistration.Register(builder);

        _container = builder.Build();
    }

    public void Dispose() => _container.Dispose();

    public T Resolve<T>() where T : notnull
    {
        return _container.Resolve<T>();
    }
}
