using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Tracker.UI.Controls;

namespace Tracker.App;

public partial class App : Application
{
    private readonly IDependencyResolver _dependencyResolver = new DependencyResolver();

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = _dependencyResolver.Resolve<IMainWindowViewModel>()
            };

            desktop.Exit += (sender, e) => _dependencyResolver.Dispose();
        }

        base.OnFrameworkInitializationCompleted();
    }
}
