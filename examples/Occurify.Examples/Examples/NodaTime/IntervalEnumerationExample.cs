using NodaTime;
using Occurify.Extensions;
using Occurify.NodaTime.Extensions;
using Occurify.TimeZones;

namespace Occurify.Examples.Examples.NodaTime
{
    internal class IntervalEnumerationExample : IExample
    {
        public string Command => "nodatime/interval-enumeration";

        public void Run()
        {
            var now = SystemClock.Instance.GetCurrentInstant();
            var nextThreeDays = new Interval(now, now + Duration.FromDays(3));
            var utcDays = TimeZonePeriods.Days(TimeZoneInfo.Utc);

            Console.WriteLine($"UTC days touching {nextThreeDays}:");
            foreach (var day in utcDays.EnumerateIntervals(nextThreeDays, PeriodIncludeOptions.PartialAllowed))
            {
                Console.WriteLine($"  {day}");
            }

            Console.WriteLine();
            Console.WriteLine($"Six-hourly instants in {nextThreeDays}:");
            foreach (var instant in Timeline.Periodic(TimeSpan.FromHours(6)).EnumerateInstants(nextThreeDays))
            {
                Console.WriteLine($"  {instant}");
            }

            Console.WriteLine();
            Console.WriteLine($"Next complete UTC day: {utcDays.GetNextCompleteInterval(now)}");
            Console.WriteLine($"Time until it starts: {utcDays.StartTimeline.GetTimeToNextInstant(now)}");
        }
    }
}
