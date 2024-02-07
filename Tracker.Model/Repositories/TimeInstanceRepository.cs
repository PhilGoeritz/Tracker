namespace Tracker.Model;

public interface ITimeInstanceRepository : IRepository<TimeInstance>
{
}

internal sealed class TimeInstanceRepository : Repository<TimeInstance>, ITimeInstanceRepository
{
}
