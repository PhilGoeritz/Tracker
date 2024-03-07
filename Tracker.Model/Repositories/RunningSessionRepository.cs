using DotNext;
using DynamicData;
using Tracker.Model.Objects;

namespace Tracker.Model.Repositories;

public interface IRunningSessionRepository : IRepository<RunningSession, Guid>;

internal sealed class RunningSessionRepository : Repository<RunningSession, Guid>, IRunningSessionRepository
{
    protected override SourceCache<RunningSession, Guid> ItemCache { get; } = new(x => x.ActivityId);
}
