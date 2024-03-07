using DotNext;
using Tracker.Model.Objects;
using Tracker.Model.Repositories;

namespace Tracker.Logic;

public interface ISessionService
{
    RunningSession StartSession(Activity activity);
    Optional<RunningSession> GetRunningSession();
    TimeInstance FinishSession(RunningSession session);
}

internal sealed class SessionService : ISessionService
{
    private readonly IRunningSessionRepository _runningSessionRepository;
    private readonly ITimeInstanceRepository _timeInstanceRepository;

    public SessionService(
        IRunningSessionRepository runningSessionRepository,
        ITimeInstanceRepository timeInstanceRepository)
    {
        _runningSessionRepository = runningSessionRepository;
        _timeInstanceRepository = timeInstanceRepository;
    }

    public RunningSession StartSession(Activity activity)
    {
        var session = new RunningSession(DateTime.Now, activity.Id);
        _runningSessionRepository.AddOrUpdate(session);

        return session;
    }

    public Optional<RunningSession> GetRunningSession() => _runningSessionRepository.GetAll().FirstOrDefault();

    public TimeInstance FinishSession(RunningSession session)
    {
        var timeInstance = new TimeInstance(
            session.StartTime,
            DateTime.Now - session.StartTime,
            session.ActivityId);
            
        _runningSessionRepository.Remove(session.ActivityId);
        _timeInstanceRepository.AddOrUpdate(timeInstance);

        return timeInstance;
    }
}
