﻿namespace Tracker.Model.Objects;

public record TimeInstance(DateTime StartTime, TimeSpan Duration, string Activity)
{
    public Guid Id { get; init; } = Guid.NewGuid();
}