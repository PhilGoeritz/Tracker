using System;
using Tracker.UI.Controls;

namespace Tracker.UI;

public sealed class MainWindowViewModel
{
    public IWorkTimeTimerViewModel  WorkTimeTimer { get; } = new WorkTimeTimerViewModel();
}
