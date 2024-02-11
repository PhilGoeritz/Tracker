using DotNext;
using Tracker.Model;

namespace Tracker.Logic;

public interface ISessionService
{
    RunningSession StartSession();
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

    public RunningSession StartSession()
    {
        var session = new RunningSession(DateTime.UtcNow, DefaultActivities.Work.ToString());
        _runningSessionRepository.AddOrUpdate(session);

        return session;
    }

    public Optional<RunningSession> GetRunningSession() => _runningSessionRepository.GetAll().FirstOrDefault();

    public TimeInstance FinishSession(RunningSession session)
    {
        var timeInstance = new TimeInstance(
            session.StartTime,
            DateTime.UtcNow - session.StartTime,
            session.Activity);
            
        _runningSessionRepository.Remove(session.Activity);
        _timeInstanceRepository.AddOrUpdate(timeInstance);

        return timeInstance;
    }
}
