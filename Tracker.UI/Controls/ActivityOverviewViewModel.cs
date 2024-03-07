using System.Reactive.Disposables;
using System.Reactive.Linq;
using DotNext;
using ReactiveUI.Fody.Helpers;
using Tracker.Model.Objects;
using Tracker.Model.Repositories;

namespace Tracker.UI.Controls;

public interface IActivityOverviewViewModel : IDisposable;

public sealed class ActivityOverviewViewModel : ViewModelBase, IActivityOverviewViewModel
{
    private readonly CompositeDisposable _disposables = new();
    
    [Reactive] public string TotalTimeSpent { get; set; }
    
    [Reactive] public string TimeSpentThisWeek { get; set; }

    public ActivityOverviewViewModel(
        Activity activity,
        IRunningSessionRepository runningSessionRepository,
        ITimeInstanceRepository timeInstanceRepository)
    {
    }
    
    public void Dispose() => _disposables.Dispose();
}