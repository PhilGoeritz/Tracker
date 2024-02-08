using DynamicData;

namespace Tracker.Model;

public interface ITimeInstanceRepository : IRepository<TimeInstance, Guid>
{
}

internal sealed class TimeInstanceRepository : Repository<TimeInstance, Guid>, ITimeInstanceRepository
{
    protected override SourceCache<TimeInstance, Guid> ItemCache { get; } = new(x => x.Id);
}
