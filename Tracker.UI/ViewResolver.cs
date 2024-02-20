using Tracker.UI.Controls;

namespace Tracker.UI;

public static class ViewResolver
{
    /// <summary>
    /// Contains ViewModels mapped to their corresponding Views (ViewModel, View).
    /// </summary>
    public static readonly IReadOnlyDictionary<Type, Type> ViewMap = new Dictionary<Type, Type>
    {
        { typeof(WorkTimeTimerViewModel), typeof(WorkTimeTimer) },
        { typeof(ActivityOverviewViewModel), typeof(ActivityOverview) },
    };
}
