namespace Tracker.Model.Objects;

public record TimeInstance(DateTime StartTime, TimeSpan Duration, Guid ActivityId)
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
