using Occurify.Extensions;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class TimelineCreationExample : IExample
    {
        public string Command => "readme/timeline-creation";

        public void Run()
        {
            DateTime utcNow = DateTime.UtcNow;

            // Using extension methods
            ITimeline timeline1 = utcNow.AsTimeline();
            ITimeline timeline2 = new[] { utcNow, utcNow + TimeSpan.FromHours(1) }.AsTimeline();

            // Using static methods
            ITimeline timeline3 = Timeline.FromInstants(utcNow, utcNow + TimeSpan.FromHours(1), utcNow, utcNow + TimeSpan.FromHours(3));
            ITimeline timeline4 = Timeline.Periodic(TimeSpan.FromHours(1));
        }
    }
}
