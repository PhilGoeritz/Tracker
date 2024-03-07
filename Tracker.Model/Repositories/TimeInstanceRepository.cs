using System.Reactive.Linq;
using DynamicData;
using Tracker.Model.Objects;
using Tracker.Utilities;

namespace Tracker.Model.Repositories;

public interface ITimeInstanceRepository : IRepository<TimeInstance, Guid>
{
    IObservable<IChangeSet<TimeInstance, Guid>> Observe(Guid activityId);
    
    IObservable<IChangeSet<TimeInstance, Guid>> ObserveInstancesOfThisWeek();
    
    IObservable<TimeSpan> ObserveTimeSpentThisWeek(string activity);
}

internal sealed class TimeInstanceRepository : Repository<TimeInstance, Guid>, ITimeInstanceRepository
{
    protected override SourceCache<TimeInstance, Guid> ItemCache { get; } = new(x => x.Id);
    private readonly ICalenderService _calenderService;

    public TimeInstanceRepository(ICalenderService calenderService)
    {
        _calenderService = calenderService;
    }

    public IObservable<IChangeSet<TimeInstance, Guid>> Observe(Guid activityId)
        => GetAllAndObserve().Filter(instance => instance.ActivityId.Equals(activityId));

    public IObservable<IChangeSet<TimeInstance, Guid>> ObserveInstancesOfThisWeek()
    {
        var (startOfWeek, endOfWeek) = _calenderService.GetWeekBoundaries(DateTime.Now);
        return GetAllAndObserve().Filter(instance => instance.StartTime >= startOfWeek && instance.StartTime <= endOfWeek);
    }
    
    public IObservable<TimeSpan> ObserveTimeSpentThisWeek(string activity)
    {
        return ObserveInstancesOfThisWeek()
            .Transform(instance => instance.Duration)
            .ToCollection()
            .Select(durations => durations.Aggregate((previous, next) => previous + next));
    }
}
