using System.Reactive.Concurrency;
using Occurify.Astro;
using Occurify.Extensions;
using Occurify.Reactive.Extensions;
using Occurify.TimeZones;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class UsageExample : IExample
    {
        public string Command => "readme/usage";

        public void Run()
        {
            ITimeline sunsets = AstroInstants.LocalSunsets;

            ITimeline twentyMinAfterSunset = sunsets + TimeSpan.FromMinutes(20);

            ITimeline elevenPm = TimeZoneInstants.DailyAt(hour: 23);
            IPeriodTimeline lightOnPeriods = twentyMinAfterSunset.To(elevenPm);

            lightOnPeriods = lightOnPeriods.Within(TimeZonePeriods.Workdays());
            lightOnPeriods = lightOnPeriods.Randomize(TimeSpan.FromMinutes(10));

            if (lightOnPeriods.IsNow())
            {
                Console.WriteLine("Turning the lights on!"); // Turn lights on.
            }
            else
            {
                Console.WriteLine("Turning the lights off!"); // Turn lights off.
            }

            Console.WriteLine("The rest of the current month the lights will go on at:");
            foreach (Period period in lightOnPeriods.EnumerateRange(DateTime.UtcNow, TimeZonePeriods.CurrentMonth().End!.Value))
            {
                Console.WriteLine(period.Start!.Value.ToLocalTime());
            }

            Console.WriteLine("In February 2050 the lights will go on at:");
            foreach (Period period in lightOnPeriods.EnumeratePeriod(TimeZonePeriods.Month(2, 2050)))
            {
                Console.WriteLine(period.Start!.Value.ToLocalTime());
            }

            var scheduler = Scheduler.Default;
            lightOnPeriods.SubscribeStartEnd(
                () => Console.WriteLine("Turning the lights on!"),
                () => Console.WriteLine("Turning the lights off!"), scheduler);

            Console.WriteLine("Scheduler is running and will continue to toggle the lights at the correct times.");
            Console.WriteLine("Press any button to stop this example.");
            Console.ReadLine();
        }
    }
}
