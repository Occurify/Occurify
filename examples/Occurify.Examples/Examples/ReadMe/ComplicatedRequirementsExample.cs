using System.Reactive.Concurrency;
using Occurify.Astro;
using Occurify.Extensions;
using Occurify.Reactive.Extensions;
using Occurify.TimeZones;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class ComplicatedRequirementsExample : IExample
    {
        public string Command => "readme/complicated-requirements";

        public void Run()
        {
            int seed = 1337;

            // Determine start instants
            ITimeline tenMinAfterSunset = AstroInstants.LocalSunset + TimeSpan.FromMinutes(10);
            ITimeline randomizedSunset = tenMinAfterSunset.Randomize(seed, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(10));
            // Ensure the light doesn't turn on before 5:15 PM
            ITimeline after515Pm = (randomizedSunset + TimeZoneInstants.DailyAt(hour: 17, minute: 15)).LastWithin(TimeZonePeriods.Days());
            // Ensure the light doesn't turn on after 8 PM
            ITimeline turnOnAt = (after515Pm + TimeZoneInstants.DailyAt(hour: 20)).FirstWithin(TimeZonePeriods.Days());

            // Determine end instants
            ITimeline turnOffAt = TimeZoneInstants.DailyAt(hour: 23, minute: 15).Randomize(seed, TimeSpan.FromMinutes(20));

            // Create period
            IPeriodTimeline lightOnPeriods = turnOnAt.To(turnOffAt);

            // Schedule
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
