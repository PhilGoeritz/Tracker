using System.Collections;

// ReSharper disable once CheckNamespace
namespace Tracker.Logic.Tests.CalenderServiceTests;

[TestFixture]
public sealed class WeekBoundariesTests
{
    private readonly CalenderService _calenderService = new ();
    
    [TestCaseSource(typeof(CalenderServiceTest), nameof(CalenderServiceTest.WeekBoundaryDates))]
    public (DateTime WeekStart, DateTime WeekEnd) GetWeekBoundaries_ReturnsCorrectWeekBoundaries(DateTime date)
    {
        return _calenderService.GetWeekBoundaries(date);
    }
}

[TestFixture]
public sealed class MonthBoundariesTests
{
    private readonly CalenderService _calenderService = new ();
    
    [TestCaseSource(typeof(CalenderServiceTest), nameof(CalenderServiceTest.MonthBoundaryDates))]
    public (DateTime MonthStart, DateTime MonthEnd) GetMonthBoundaries_ReturnsCorrectMonthBoundaries(DateTime date)
    {
        return _calenderService.GetMonthBoundaries(date);
    }
}

public sealed class CalenderServiceTest
{
    public static IEnumerable WeekBoundaryDates
    {
        get
        {
            var testWeek = (new DateTime(2024, 2, 19, 0, 0, 0),
                            new DateTime(2024, 2, 25, 23, 59, 59)); 
            
            yield return new TestCaseData(new DateTime(2024, 2, 20, 19, 54, 24)).Returns(testWeek);
            yield return new TestCaseData(new DateTime(2024, 2, 19, 00, 00, 00)).Returns(testWeek);
            yield return new TestCaseData(new DateTime(2024, 2, 25, 23, 59, 59)).Returns(testWeek);
        }
    }
    public static IEnumerable MonthBoundaryDates
    {
        get
        {
            var testWeek = (new DateTime(2024, 2, 1, 0, 0, 0),
                            new DateTime(2024, 2, 29, 23, 59, 59)); 
            
            yield return new TestCaseData(new DateTime(2024, 2, 20, 19, 54, 24)).Returns(testWeek);
            yield return new TestCaseData(new DateTime(2024, 2, 01, 00, 00, 00)).Returns(testWeek);
            yield return new TestCaseData(new DateTime(2024, 2, 29, 23, 59, 59)).Returns(testWeek);
        }
    }
}
