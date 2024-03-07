namespace Tracker.Utilities;

public interface ICalenderService
{
    (DateTime WeekStart, DateTime WeekEnd) GetWeekBoundaries(DateTime date);
    (DateTime MonthStart, DateTime MonthEnd) GetMonthBoundaries(DateTime date);
}

internal sealed class CalenderService : ICalenderService
{
    public (DateTime WeekStart, DateTime WeekEnd) GetWeekBoundaries(DateTime currentTime)
    {
        var daysToSubtract = (int)currentTime.DayOfWeek - (int)DayOfWeek.Monday;
        if (daysToSubtract < 0)
            daysToSubtract += 7;
        
        var startOfWeek = currentTime.AddDays(-daysToSubtract).Date;
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
