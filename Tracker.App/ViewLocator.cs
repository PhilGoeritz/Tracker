using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;
using Tracker.UI;
using Tracker.UI.Controls;

namespace Tracker.App;

public class ViewLocator : IDataTemplate
{
    public bool SupportsRecycling => false;

    public Control? Build(object? data)
    {
        if (data is null)
            return new TextBlock { Text = "ViewModel is null"};

        if (ViewResolver.ViewMap.ContainsKey(data.GetType()) == false)
            return new TextBlock { Text = "Not Found: " + data.GetType().Name };

        return Activator.CreateInstance(ViewResolver.ViewMap[data.GetType()]) as Control;
    }

    public bool Match(object? data) => data is ViewModelBase;
}
