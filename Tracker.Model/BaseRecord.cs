namespace Tracker.Model;

public abstract record BaseRecord
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
