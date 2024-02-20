using DynamicData;
using DynamicData.Alias;
using ReactiveUI.Fody.Helpers;
using Tracker.Model;
using Tracker.Model.Repositories;

namespace Tracker.UI.Controls;

public interface IActivityOverviewViewModel;

public sealed class ActivityOverviewViewModel : ViewModelBase, IActivityOverviewViewModel
{
    private readonly IRunningSessionRepository _sessionRepository;
    private readonly ITimeInstanceRepository _timeInstanceRepository;

    [Reactive] public string TotalTimeSpent { get; set; } = "some time";

    public ActivityOverviewViewModel(
        IRunningSessionRepository sessionRepository,
        ITimeInstanceRepository timeInstanceRepository)
    {
        _sessionRepository = sessionRepository;
        _timeInstanceRepository = timeInstanceRepository;
        
        // _timeInstanceRepository
        //     .WatchAll()
        //     .Transform(instance => instance.Duration)
    }
}