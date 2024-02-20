using System.Reactive.Disposables;
using System.Windows.Input;

namespace Tracker.UI.Utility;

public sealed class ActionCommand : ICommand, IDisposable
{
    private readonly CompositeDisposable _disposables = new();

    private readonly Action _action;
    private bool _canExecute;

    public ActionCommand(Action action)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _canExecute = true;
    }

    public ActionCommand(Action action, IObservable<bool> canExecute)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        canExecute
            .Subscribe(value =>
            {
                _canExecute = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            })
            .DisposeWith(_disposables);
    }

    public void Dispose() => _disposables.Dispose();

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => _canExecute;

    public void Execute(object? parameter) => _action();
}
