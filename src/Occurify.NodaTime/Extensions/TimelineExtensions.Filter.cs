using NodaTime;
using Occurify.NodaTime.Extensions;
using Occurify.TimelineFilters;

namespace Occurify.Extensions;

public static partial class TimelineExtensions
{
    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the first <paramref name="count"/> instants of <paramref name="source"/> within every interval in <paramref name="mask"/> are bypassed.
    /// </summary>
    public static ITimeline SkipWithin(this ITimeline source, Interval mask, int count) =>
            source.SkipWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the first <paramref name="count"/> instants of <paramref name="source"/> within every interval in <paramref name="mask"/> are bypassed.
    /// </summary>
    public static ITimeline SkipWithin(this ITimeline source, IEnumerable<Interval> mask, int count) =>
        source.SkipWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the last <paramref name="count"/> instants of <paramref name="source"/> within the interval <paramref name="mask"/> are omitted.
    /// </summary>
    public static ITimeline SkipLastWithin(this ITimeline source, Interval mask, int count) =>
        source.SkipLastWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> in which the last <paramref name="count"/> instants of <paramref name="source"/> within every interval in <paramref name="mask"/> are omitted.
    /// </summary>
    public static ITimeline SkipLastWithin(this ITimeline source, IEnumerable<Interval> mask, int count) =>
        source.SkipLastWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first <paramref name="count"/> instants of <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeWithin(this ITimeline source, Interval mask, int count) =>
        source.TakeWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first <paramref name="count"/> instants of <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeWithin(this ITimeline source, IEnumerable<Interval> mask, int count) =>
        source.TakeWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last <paramref name="count"/> instants of <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeLastWithin(this ITimeline source, Interval mask, int count) =>
        source.TakeLastWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last <paramref name="count"/> instants of <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline TakeLastWithin(this ITimeline source, IEnumerable<Interval> mask, int count) =>
        source.TakeLastWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first instant of <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static ITimeline FirstWithin(this ITimeline source, Interval mask) =>
        source.FirstWithin(mask.ToPeriod());

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the first instant of <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline FirstWithin(this ITimeline source, IEnumerable<Interval> mask) =>
        source.FirstWithin(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last instant of <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static ITimeline LastWithin(this ITimeline source, Interval mask) =>
        source.LastWithin(mask.ToPeriod());

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains the last instant of <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline LastWithin(this ITimeline source, IEnumerable<Interval> mask) =>
        source.LastWithin(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains <paramref name="instantToContain"/> if it is also present in <paramref name="source"/>.
    /// </summary>
    public static ITimeline Containing(this ITimeline source, Instant instantToContain) =>
        source.Containing(instantToContain.ToDateTimeUtc());

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static ITimeline Containing(this ITimeline source, IEnumerable<Instant> instantsToContain) =>
        source.Containing(instantsToContain.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static ITimeline Containing(this ITimeline source, params Instant[] instantsToContain) =>
        source.Containing(instantsToContain.Select(i => i.ToDateTimeUtc()).ToArray());

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Within(this ITimeline source, Interval mask) =>
        source.Within(mask.ToPeriod());

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Within(this ITimeline source, IEnumerable<Interval> mask) =>
        source.Within(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Within(this ITimeline source, params Interval[] mask) =>
        source.Within(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Outside(this ITimeline source, Interval mask) =>
        source.Outside(mask.ToPeriod());

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Outside(this ITimeline source, IEnumerable<Interval> mask) =>
        source.Outside(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static ITimeline Outside(this ITimeline source, params Interval[] mask) =>
        source.Outside(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that does not contain <paramref name="instantToExclude"/>.
    /// </summary>
    public static ITimeline Without(this ITimeline source, Instant instantToExclude) =>
        source.Without(instantToExclude.ToDateTimeUtc());

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static ITimeline Without(this ITimeline source, IEnumerable<Instant> instantsToExclude) =>
        source.Without(instantsToExclude.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static ITimeline Without(this ITimeline source, params Instant[] instantsToExclude) =>
        source.Without(instantsToExclude.Select(i => i.ToDateTimeUtc()).ToArray());

    /// <summary>
    /// Filters <paramref name="source"/> based on <paramref name="predicate"/>.
    /// Do not use this method lightly: as it always has to evaluate every instant, the performance impact might be significant.
    /// In order for Occurify to function properly, <paramref name="predicate"/> should be deterministic. 
    /// </summary>
    public static ITimeline WhereInstants(this ITimeline source, Func<Instant, bool> predicate) =>
        source.WhereInstants(dt => predicate(Instant.FromDateTimeUtc(dt)));
}