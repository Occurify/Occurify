using Occurify.Astro;
using Occurify.Extensions;
using Occurify.TimeZones;

namespace Occurify.Examples.Examples.ReadMe
{
    internal class TimelineCollectionExample : IExample
    {
        public string Command => "readme/timeline-collection";

        public void Run()
        {
            Dictionary<ITimeline, bool> sunStates = new Dictionary<ITimeline, bool>
            {
                { AstroInstants.LocalSunrises, true },
                { AstroInstants.LocalSunsets, false }
            };
            foreach (KeyValuePair<DateTime, bool[]> state in sunStates.EnumeratePeriod(TimeZonePeriods.CurrentMonth()))
            {
                bool sunIsRising = state.Value.First(); // Since we're combining multiple timelines, a single instant may correspond to multiple values. However, for this example, we assume sunrise and sunset don't occur simultaneously, so we just take the first value.
                Console.WriteLine(sunIsRising ?
                    $"At {state.Key} the sun is rising!" :
                    $"At {state.Key} the sun is setting!");
            }
        }
    }
}
