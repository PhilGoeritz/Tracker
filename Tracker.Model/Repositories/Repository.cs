using DotNext;
using DynamicData;

namespace Tracker.Model;

public interface IRepository<Type, Key>
    where Type : notnull
    where Key : notnull
{
    void AddOrUpdate(Type item);
    void Remove(Type item);
    void Remove(Key id);

    Optional<Type> Get(Key id);
    IEnumerable<Type> GetAll();

    IObservable<Type> Watch(Key id);
    IObservable<IChangeSet<Type, Key>> WatchAll();
}

internal abstract class Repository<Type, Key> : IRepository<Type, Key>
    where Type : notnull
    where Key : notnull
{
    protected abstract SourceCache<Type, Key> ItemCache { get; }

    public void AddOrUpdate(Type item) => ItemCache.AddOrUpdate(item);
    public void Remove(Type item) => ItemCache.Remove(item);
    public void Remove(Key id) => ItemCache.RemoveKey(id);

    public Optional<Type> Get(Key id) => ItemCache.Lookup(id).Value;
    public IEnumerable<Type> GetAll() => ItemCache.Items;

    public IObservable<Type> Watch(Key id) => ItemCache.WatchValue(id);
    public IObservable<IChangeSet<Type, Key>> WatchAll() => ItemCache.Connect();
}
