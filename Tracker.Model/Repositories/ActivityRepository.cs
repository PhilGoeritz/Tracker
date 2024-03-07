using DynamicData;
using Tracker.Model.Objects;

namespace Tracker.Model.Repositories;

public interface IActivityRepository : IRepository<Activity, Guid>;

internal sealed class ActivityRepository : Repository<Activity, Guid>, IActivityRepository
{
    protected override SourceCache<Activity, Guid> ItemCache { get; }
        = new SourceCache<Activity, Guid>(activity => activity.Id);
}