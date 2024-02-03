using System.ComponentModel;
using ReactiveUI;

namespace Tracker.UI.Controls;

public interface IViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging {}

public abstract class ViewModelBase : ReactiveObject, IViewModelBase {}
