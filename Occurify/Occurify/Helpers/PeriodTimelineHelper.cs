using Occurify.Extensions;
using Occurify.Timelines;

namespace Occurify.Helpers
{
    internal class PeriodTimelineHelper
    {
        internal static IPeriodTimeline CreatePeriodTimelineFromPeriods(IEnumerable<Period> periods)
        {
            // While we could use Merge to combine period timelines per provided period but this approach scales poorly as data grows. 
            // Further optimization is possible (e.g., merging here or limiting timelines by adding periods to ones that fit), 
            // but this implementation handles many common cases well.
            var orderedPeriods = periods.Order().ToArray();

            if (!orderedPeriods.Any())
            {
                return PeriodTimeline.Empty();
            }

            var periodTimelines = new List<IPeriodTimeline>();
            var periodStarts = new List<DateTime>();
            var periodEnds = new List<DateTime>();
            for (var i = 0; i < orderedPeriods.Length - 1; i++)
            {
                var current = orderedPeriods[i];
                var next = orderedPeriods[i + 1];

                if (current.Start != null)
                {
                    periodStarts.Add(current.Start.Value);
                }
                if (current.End != null)
                {
                    periodEnds.Add(current.End.Value);
                }

                if (next.Start == null || current.ContainsInstant(next.Start.Value))
                {
                    // If out next start is null or inside the current period, we start over and let the Merge method handle merging these.
                    periodTimelines.Add(new PeriodTimeline(
                        periodStarts.Any() ? new CollectionTimeline(periodStarts) : new EmptyTimeline(),
                        periodEnds.Any() ? new CollectionTimeline(periodEnds) : new EmptyTimeline()));
                    periodStarts = [];
                    periodEnds = [];
                }
            }

            var last = orderedPeriods.Last();
            if (last.Start != null)
            {
                periodStarts.Add(last.Start.Value);
            }
            if (last.End != null)
            {
                periodEnds.Add(last.End.Value);
            }
            periodTimelines.Add(new PeriodTimeline(
                periodStarts.Any() ? new CollectionTimeline(periodStarts) : new EmptyTimeline(),
                periodEnds.Any() ? new CollectionTimeline(periodEnds) : new EmptyTimeline()));

            return periodTimelines.Merge();
        }
    }
}
