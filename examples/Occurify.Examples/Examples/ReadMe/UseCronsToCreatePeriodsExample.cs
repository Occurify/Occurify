using Occurify.Extensions;
using Occurify.TimeZones;
using Occurify.TimeZones.Extensions;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class UseCronsToCreatePeriodsExample : IExample
    {
        public string Command => "readme/use-crons-to-create-periods";

        public void Run()
        {
            string[] holidayCrons = [
                "0 0 0 1 1 ?", // New Year Day
                "0 0 0 ? 5 MON#4", //Memorial Day
                "0 0 0 4 7 ?", //Independence Day
                "0 0 0 ? 9 MON#1", //Labor Day
                "0 0 0 ? 11 THU#4", //Thanksgiving
                "0 0 0 25 12 ?" //Christmas
            ];

            IPeriodTimeline holidays = TimeZonePeriods.Days(holidayCrons.Select(TimeZoneInstants.FromCron).Combine());
            IPeriodTimeline workingDays = TimeZonePeriods.Workdays();
            IPeriodTimeline workingDaysWithoutHolidays = workingDays - holidays;

            Console.WriteLine("Work days this year:");
            foreach (Period period in workingDaysWithoutHolidays.EnumeratePeriod(TimeZonePeriods.CurrentYear()))
            {
                Console.WriteLine(period.ToString(TimeZoneInfo.Local));
            }
        }
    }
}
