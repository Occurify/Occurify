using System.Reactive.Concurrency;
using Occurify.Reactive.Extensions;

namespace Occurify.Examples.Examples.Scheduling
{
    internal class MultipleTimelinesExample : IExample
    {
        public string Command => "scheduling/multiple-timelines";

        public void Run()
        {
            var utcNow = DateTime.UtcNow;
            var multipleOfThree = Timeline.Periodic(utcNow, TimeSpan.FromSeconds(3));
            var multipleOfFour = Timeline.Periodic(utcNow, TimeSpan.FromSeconds(4));
            var multipleOfFive = Timeline.Periodic(utcNow, TimeSpan.FromSeconds(5));
            var timelines = new Dictionary<ITimeline, string>
            {
                { multipleOfThree, "three" },
                { multipleOfFour, "four" },
                { multipleOfFive, "five" }
            };

            timelines.Subscribe(x => Console.WriteLine($"It is {x.Key} and a multiple of {string.Join(", ", x.Value)} seconds has occurred since {utcNow}."), Scheduler.Default, includeCurrentInstant: false);

            Console.ReadLine();
        }
    }
}
