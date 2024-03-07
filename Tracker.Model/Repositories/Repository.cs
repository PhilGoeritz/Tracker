using System.Reactive.Linq;
using DotNext;
using DynamicData;

namespace Tracker.Model.Repositories;

public interface IRepository<TType, TKey>
    where TType : notnull
    where TKey : notnull
{
    void AddOrUpdate(TType item);
    void Remove(TType item);
    void Remove(TKey key);

    Optional<TType> Get(TKey key);
    IEnumerable<TType> GetAll();

    /// <summary>
    /// If no item with the given id exists or the item has been deleted,
    /// the returned observable will emit Optional.None.
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    IObservable<Optional<TType>> Watch(TKey key);
    IObservable<IChangeSet<TType, TKey>> GetAllAndObserve();
}

internal abstract class Repository<TType, TKey> : IRepository<TType, TKey>
    where TType : notnull
    where TKey : notnull
{
    protected abstract SourceCache<TType, TKey> ItemCache { get; }

    public void AddOrUpdate(TType item) => ItemCache.AddOrUpdate(item);
    public void Remove(TType item) => ItemCache.Remove(item);
    public void Remove(TKey key) => ItemCache.RemoveKey(key);

    public Optional<TType> Get(TKey key) => ItemCache.Lookup(key).Value;
    public IEnumerable<TType> GetAll() => ItemCache.Items;

    public IObservable<Optional<TType>> Watch(TKey key)
        => ItemCache
            .Watch(key)
            .Select(ToOptional)
            .StartWith(Optional<TType>.None);

    public IObservable<IChangeSet<TType, TKey>> GetAllAndObserve() => ItemCache.Connect();

    private static Optional<TType> ToOptional(Change<TType, TKey> change) 
        => change.Reason == ChangeReason.Remove
            ? Optional<TType>.None
            : change.Current;
}
