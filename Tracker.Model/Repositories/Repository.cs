using DotNext;
using DynamicData;

namespace Tracker.Model;

public interface IRepository<T> where T : BaseRecord
{
    void AddOrUpdate(T session);
    void Remove(Guid id);

    Optional<T> Get(Guid id);
    IEnumerable<T> GetAll();

    IObservable<T> Watch(Guid id);
    IObservable<IChangeSet<T, Guid>> WatchAll();
}

internal abstract class Repository<T> : IRepository<T> where T : BaseRecord
{
    protected readonly SourceCache<T, Guid> _sessionCache
        = new SourceCache<T, Guid>(session => session.Id);

    public void AddOrUpdate(T session) => _sessionCache.AddOrUpdate(session);
    public void Remove(Guid id) => _sessionCache.RemoveKey(id);

    public Optional<T> Get(Guid id) => _sessionCache.Lookup(id).Value;
    public IEnumerable<T> GetAll() => _sessionCache.Items;

    public IObservable<T> Watch(Guid id) => _sessionCache.WatchValue(id);
    public IObservable<IChangeSet<T, Guid>> WatchAll() => _sessionCache.Connect();
}
