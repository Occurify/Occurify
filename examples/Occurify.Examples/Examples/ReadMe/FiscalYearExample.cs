using Occurify.Extensions;
using Occurify.TimeZones;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class FiscalYearExample : IExample
    {
        public string Command => "readme/fiscal-year";

        public void Run()
        {
            IPeriodTimeline calendarYears = TimeZonePeriods.Years();
            IPeriodTimeline fiscalYears = TimeZoneInstants.StartOfMonths(10).AsConsecutivePeriodTimeline();

            Period currentCalendarYear = calendarYears.SampleAt(DateTime.UtcNow).Period!;
            Period currentFiscalYear = fiscalYears.SampleAt(DateTime.UtcNow).Period!;

            IPeriodTimeline workdays = TimeZonePeriods.Workdays();
            int amountOfCalendarDaysWorked = workdays.EnumerateRange(currentCalendarYear.Start!.Value, DateTime.UtcNow).Count();
            int amountOfFiscalYearDaysWorked = workdays.EnumerateRange(currentFiscalYear.Start!.Value, DateTime.UtcNow).Count();

            Console.WriteLine($"Currently we have worked {amountOfCalendarDaysWorked} days this calendar year!");
            Console.WriteLine($"Currently we have worked {amountOfFiscalYearDaysWorked} days this fiscal year!");
        }
    }
}
