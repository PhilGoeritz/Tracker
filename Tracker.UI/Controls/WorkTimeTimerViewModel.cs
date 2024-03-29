using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Tracker.Logic;
using Tracker.Model.Objects;
using Tracker.UI.Utility;

namespace Tracker.UI.Controls;

public interface IWorkTimeTimerViewModel : IDisposable;

public sealed class WorkTimeTimerViewModel : ViewModelBase, IWorkTimeTimerViewModel
{
    private readonly SerialDisposable _timerSubscription = new();
    private readonly CompositeDisposable _disposables = new();

    private readonly ISessionService _sessionService;

    [Reactive] public string Timer { get; set; } = "00:00:00";
    public ActionCommand StartCommand { get; }
    public ActionCommand StopCommand { get; }

    [Reactive] public RunningSession? RunningSession { get; private set; }

    public WorkTimeTimerViewModel(ISessionService sessionService)
    {
        _sessionService = sessionService;

        var sessionObservable = this.WhenAnyValue(viewModel => viewModel.RunningSession);
        StartCommand = new ActionCommand(StartSession, sessionObservable.Select(session => session is null));
        StopCommand = new ActionCommand(StopSession, sessionObservable.Select(session => session is not null));

        StartCommand.DisposeWith(_disposables);
        StopCommand.DisposeWith(_disposables);

        var runningSession = _sessionService.GetRunningSession();
        if (runningSession.HasValue)
            SetupRunningSession(runningSession.Value);

        _timerSubscription.DisposeWith(_disposables);
    }

    public void Dispose() => _disposables.Dispose();

    private void SetupRunningSession(RunningSession value)
    {
        RunningSession = value;
        _timerSubscription.Disposable = Observable
            .Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ => Timer = (DateTime.Now - value.StartTime).ToString(@"hh\:mm\:ss"));
    }

    private void StartSession()
    {
        // var session = _sessionService.StartSession();
        // SetupRunningSession(session);
    }

    private void StopSession()
    {
        if (RunningSession is null)
            return;

        _sessionService.FinishSession(RunningSession);
        RunningSession = null;
        _timerSubscription.Disposable = Disposable.Empty;
    }
}
