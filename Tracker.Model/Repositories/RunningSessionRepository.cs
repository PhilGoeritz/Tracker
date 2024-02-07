namespace Tracker.Model;

public interface IRunningSessionRepository : IRepository<RunningSession>
{
}

internal sealed class RunningSessionRepository : Repository<RunningSession>
{
}
