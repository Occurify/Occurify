using System.Reactive.Concurrency;
using Occurify.Astro;
using Occurify.Extensions;
using Occurify.Reactive.Extensions;
using Occurify.TimeZones;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class MorningLightExample : IExample
    {
        public string Command => "readme/morning-light";

        public void Run()
        {
            ITimeline fifteenMinAfterSunRise = AstroInstants.LocalSunrise + TimeSpan.FromMinutes(15);
            ITimeline sevenAm = TimeZoneInstants.DailyAt(hour: 7);
            IPeriodTimeline between7AndSunRise = sevenAm.To(fifteenMinAfterSunRise); // Creates timeline that represents periods starting at 7am and ending 15 minutes after sunrise.

            IPeriodTimeline between7AndSunRiseInTheMorning = between7AndSunRise.Within(TimeZonePeriods.Days());
            //Alternative: IPeriodTimeline between7AndSunRiseInTheMorning = TimeZonePeriods.DailyBetween(fifteenMinAfterSunRise, sevenAm);

            var scheduler = Scheduler.Default;
            between7AndSunRiseInTheMorning.SubscribeStartEnd(
                () => Console.WriteLine("Turning the lights on!"),
                () => Console.WriteLine("Turning the lights off!"), scheduler);

            Console.WriteLine("Scheduler is running and will continue to toggle the lights at the correct times.");
            Console.WriteLine("Press any button to stop this example.");
            Console.ReadLine();
        }
    }
}
