using Tracker.UI.Controls;

namespace Tracker.App;

internal sealed class MainWindowViewModel
{
    public IWorkTimeTimerViewModel WorkTimeTimer { get; }

    public MainWindowViewModel(IWorkTimeTimerViewModel workTimeTimerViewModel)
    {
        WorkTimeTimer = workTimeTimerViewModel;
    }
}
