using System.Reactive.Concurrency;
using DotNext;
using DynamicData;
using FluentAssertions;
using Microsoft.Reactive.Testing;
using Tracker.Model.Repositories;

namespace Tracker.Model.Tests.Repositories;

internal record TestObject(Guid Id, string DebugInfo = "");

internal sealed class TestRepository : Repository<TestObject, Guid>
{
    protected override SourceCache<TestObject, Guid> ItemCache { get; } = new(x => x.Id);
} 

[TestFixture]
internal sealed class WhenGetAndWatchIsCalled
{
    private static readonly Guid WatchedItemId = Guid.NewGuid();
    private static readonly Guid OtherItemId = Guid.NewGuid();
    
    private static readonly TestObject FirstObject = new(WatchedItemId, "First");
    private static readonly TestObject SecondObject = new(WatchedItemId, "Second");
    
    private readonly TestScheduler _scheduler = new();
    
    private readonly TestRepository _target = new();
    
    private ITestableObserver<Optional<TestObject>>? _result;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _scheduler.ScheduleAbsolute(100, () => _target.AddOrUpdate(FirstObject));
        _scheduler.ScheduleAbsolute(200, () => _target.AddOrUpdate(new TestObject(OtherItemId)));
        _scheduler.ScheduleAbsolute(300, () => _target.AddOrUpdate(SecondObject));
        _scheduler.ScheduleAbsolute(400, () => _target.Remove(WatchedItemId));

        _result = _scheduler.Start(() => _target.Watch(WatchedItemId), 0, 10, 500);
    }
    
    [Test]
    public void Then_first_message_should_be_none()
    {
        _result!.Messages[0].Value.Value.Should().Be(Optional<TestObject>.None);
    }
    
    [Test]
    public void Then_first_message_should_be_at_10()
    {
        _result!.Messages[0].Time.Should().Be(10);
    }
    
    [Test]
    public void Then_second_message_should_be_the_first_object()
    {
        _result!.Messages[1].Value.Value.Should().Be(FirstObject);
    }
    
    [Test]
    public void Then_second_message_should_be_at_100()
    {
        _result!.Messages[1].Time.Should().Be(100);
    }
    
    [Test]
    public void Then_third_message_should_be_the_second_object()
    {
        _result!.Messages[2].Value.Value.Should().Be(SecondObject);
    }
    
    [Test]
    public void Then_third_message_should_be_at_300()
    {
        _result!.Messages[2].Time.Should().Be(300);
    }
    
    [Test]
    public void Then_fourth_message_should_be_none()
    {
        _result!.Messages[3].Value.Value.Should().Be(Optional<TestObject>.None);
    }
    
    [Test]
    public void Then_fourth_message_should_be_at_400()
    {
        _result!.Messages[3].Time.Should().Be(400);
    }
}
