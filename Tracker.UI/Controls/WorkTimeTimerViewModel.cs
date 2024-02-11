using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Tracker.Logic;
using Tracker.Model;

namespace Tracker.UI.Controls;

public interface IWorkTimeTimerViewModel : IDisposable {}

public sealed class WorkTimeTimerViewModel : ViewModelBase, IWorkTimeTimerViewModel
{
    private readonly SerialDisposable _timerSubscribtion = new();
    private readonly CompositeDisposable _disposables = new();

    private readonly ISessionService _sessionService;

    [Reactive] public string Timer { get; set; } = "00:00:00";
    public ActionCommand StartCommand { get; }
    public ActionCommand StopCommand { get; }

    [Reactive] public RunningSession? RunningSession { get; set; }

    public WorkTimeTimerViewModel(ISessionService sessionService)
    {
        _sessionService = sessionService;

        var sessionObservable = this.WhenAnyValue(x => x.RunningSession);
        StartCommand = new ActionCommand(StartSession, sessionObservable.Select(x => x is null));
        StopCommand = new ActionCommand(StopSession, sessionObservable.Select(x => x is not null));

        StartCommand.DisposeWith(_disposables);
        StopCommand.DisposeWith(_disposables);

        var runningSession = _sessionService.GetRunningSession();
        if (runningSession.HasValue)
            SetupRunningSession(runningSession.Value);

        _timerSubscribtion.DisposeWith(_disposables);
    }

    public void Dispose() => _disposables.Dispose();

    private void SetupRunningSession(RunningSession value)
    {
        RunningSession = value;
        _timerSubscribtion.Disposable = Observable
            .Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ => Timer = (DateTime.UtcNow - value.StartTime).ToString(@"hh\:mm\:ss"));
    }

    private void StartSession()
    {
        var session = _sessionService.StartSession();
        SetupRunningSession(session);
    }

    private void StopSession()
    {
        if (RunningSession is null)
            return;

        _sessionService.FinishSession(RunningSession);
        RunningSession = null;
        _timerSubscribtion.Disposable = Disposable.Empty;
    }
}
