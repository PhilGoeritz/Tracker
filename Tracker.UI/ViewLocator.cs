using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Tracker.UI.Controls;

namespace Tracker.UI;

public class ViewLocator : IDataTemplate
{
    private static readonly IReadOnlyDictionary<Type, Type> _viewMap = new Dictionary<Type, Type>
    {
        { typeof(WorkTimeTimerViewModel), typeof(WorkTimeTimer) }
    };

    public bool SupportsRecycling => false;

    public Control? Build(object? data)
    {
        if (data is null)
            return new TextBlock { Text = "ViewModel is null"};

        if (_viewMap.ContainsKey(data.GetType()) == false)
            return new TextBlock { Text = "Not Found: " + data.GetType().Name };

        return Activator.CreateInstance(_viewMap[data.GetType()]) as Control;
    }

    public bool Match(object? data) => data is ViewModelBase;
}
