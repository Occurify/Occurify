using Occurify.Extensions;
using Occurify.TimeZones;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class SearchingDatesExample : IExample
    {
        public string Command => "readme/searching-dates";

        public void Run()
        {
            IPeriodTimeline fridaysOfFebruary = TimeZonePeriods.Months(2, TimeZoneInfo.Utc) & TimeZonePeriods.Days(DayOfWeek.Friday, TimeZoneInfo.Utc);
            Period twoYears = TimeZoneInstants.StartOfYear(1200, TimeZoneInfo.Utc).To(TimeZoneInstants.EndOfYear(1201, TimeZoneInfo.Utc));

            Console.WriteLine("The years 1200 and 1201 have the following fridays in february:");
            foreach (var date in fridaysOfFebruary.EnumeratePeriod(twoYears))
            {
                Console.WriteLine(date.Start!.Value.ToShortDateString());
            }
        }
    }
}
