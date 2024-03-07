namespace Tracker.Model.Objects;

public record Activity(string Name, string Description)
{
    public Guid Id { get; init; } = Guid.NewGuid();
}