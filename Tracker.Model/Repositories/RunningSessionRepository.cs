using DynamicData;

namespace Tracker.Model;

/// <summary>
/// Probably not needed right now, but it's here for future use.
/// </summary>
public interface IRunningSessionRepository : IRepository<RunningSession, string>
{
}

internal sealed class RunningSessionRepository : Repository<RunningSession, string>, IRunningSessionRepository
{
    protected override SourceCache<RunningSession, string> ItemCache { get; } = new(x => x.Activity);
}
