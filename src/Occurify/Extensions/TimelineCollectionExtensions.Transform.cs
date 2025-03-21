using Occurify.TimelineTransformations;

namespace Occurify.Extensions;

public static partial class TimelineCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from all <paramref name="timelines"/>.
    /// </summary>
    public static ITimeline Combine(this IEnumerable<ITimeline> timelines) => AsCombinedTimeline(timelines);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from all <paramref name="timelines"/>.
    /// </summary>
    public static ITimeline AsCombinedTimeline(this IEnumerable<ITimeline> timelines) => new CompositeTimeline(timelines);

    // Note: No additional transform (or filter) methods are created for IEnumerable<ITimeline>, as they would be simple Select statements with little additional value. Filter methods could even be confusing, as it might not be clear whether they apply to individual timelines or samples taken from all provided timelines (e.g. with TakeWithin etc).
}