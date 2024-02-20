using Tracker.UI.Controls;

namespace Tracker.App;

public interface IMainWindowViewModel;

internal sealed class MainWindowViewModel : IMainWindowViewModel
{
    public IWorkTimeTimerViewModel WorkTimeTimer { get; }
    public IActivityOverviewViewModel ActivityOverviewViewModel { get; }

    public MainWindowViewModel(
        IWorkTimeTimerViewModel workTimeTimerViewModel,
        IActivityOverviewViewModel activityOverviewViewModel)
    {
        WorkTimeTimer = workTimeTimerViewModel;
        ActivityOverviewViewModel = activityOverviewViewModel;
    }
}
