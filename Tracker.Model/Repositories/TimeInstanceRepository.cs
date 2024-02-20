using DynamicData;
using Tracker.Model.Objects;

namespace Tracker.Model.Repositories;

public interface ITimeInstanceRepository : IRepository<TimeInstance, Guid>
{
    IObservable<IChangeSet<TimeInstance, Guid>> ObserveInstancesOfThisWeek();
    IObservable<DateTime> TimeSpentThisWeek(string activity);
}

internal sealed class TimeInstanceRepository : Repository<TimeInstance, Guid>, ITimeInstanceRepository
{
    protected override SourceCache<TimeInstance, Guid> ItemCache { get; } = new(x => x.Id);
    
    public IObservable<IChangeSet<TimeInstance, Guid>> ObserveInstancesOfThisWeek()
    {
        throw new NotImplementedException();
    }
    
    public IObservable<DateTime> TimeSpentThisWeek(string activity)
    {
        throw new NotImplementedException();
    }
}
