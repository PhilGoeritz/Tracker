namespace Tracker.Logic;

public interface ICalenderService
{
    (DateTime WeekStart, DateTime WeekEnd) GetWeekBoundaries(DateTime date);
    (DateTime MonthStart, DateTime MonthEnd) GetMonthBoundaries(DateTime date);
}

internal sealed class CalenderService : ICalenderService
{
    public (DateTime WeekStart, DateTime WeekEnd) GetWeekBoundaries(DateTime currentTime)
    {
        // Calculate the first day of the week (assuming Monday is the first day)
        var daysToSubtract = (int)currentTime.DayOfWeek - (int)DayOfWeek.Monday;
        if (daysToSubtract < 0)
        {
            // Adjust for cultures where the week starts on Sunday
            daysToSubtract += 7;
        }
        var startOfWeek = currentTime.AddDays(-daysToSubtract).Date;
        
        // Calculate the last day of the week
        var endOfWeek = startOfWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);

        return (startOfWeek, endOfWeek);
    }
    
    public (DateTime MonthStart, DateTime MonthEnd) GetMonthBoundaries(DateTime currentTime)
    {
        var startOfMonth = new DateTime(currentTime.Year, currentTime.Month, 1);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
        return (startOfMonth, endOfMonth);
    }
}