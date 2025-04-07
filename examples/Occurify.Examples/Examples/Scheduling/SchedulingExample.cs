using System.Reactive.Concurrency;
using Occurify.Extensions;
using Occurify.Reactive.Extensions;

namespace Occurify.Examples.Examples.Scheduling
{
    internal class SchedulingExample : IExample
    {
        public string Command => "example1";
        public string Description => "Bla";

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
