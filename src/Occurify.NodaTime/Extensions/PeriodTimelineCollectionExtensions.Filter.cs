using NodaTime;

namespace Occurify.Extensions;

public static partial class PeriodTimelineCollectionExtensions
{
    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are inside <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Within(this IEnumerable<IPeriodTimeline> source, Interval mask) =>
        source.Select(t => t.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Within(this IEnumerable<IPeriodTimeline> source, IEnumerable<Interval> mask) =>
        source.Select(t => t.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Within(this IEnumerable<IPeriodTimeline> source, params Interval[] mask) =>
        source.Select(t => t.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals not in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Outside(this IEnumerable<IPeriodTimeline> source, Interval mask) =>
        source.Select(t => t.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Outside(this IEnumerable<IPeriodTimeline> source, IEnumerable<Interval> mask) =>
        source.Select(t => t.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Outside(this IEnumerable<IPeriodTimeline> source, params Interval[] mask) =>
        source.Select(t => t.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain <paramref name="intervalToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, Interval intervalToContain) =>
        source.Select(t => t.Containing(intervalToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the intervals in <paramref name="intervalsToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, IEnumerable<Interval> intervalsToContain) =>
        source.Select(t => t.Containing(intervalsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the intervals in <paramref name="intervalsToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, params Interval[] intervalsToContain) =>
        source.Select(t => t.Containing(intervalsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain <paramref name="instantToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, Instant instantToContain) =>
        source.Select(t => t.Containing(instantToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, IEnumerable<Instant> instantsToContain) =>
        source.Select(t => t.Containing(instantsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, params Instant[] instantsToContain) =>
        source.Select(t => t.Containing(instantsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals do not contain <paramref name="intervalNotToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Without(this IEnumerable<IPeriodTimeline> source, Interval intervalNotToContain) =>
        source.Select(t => t.Without(intervalNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals do not contain any of the intervals in <paramref name="intervalsNotToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Without(this IEnumerable<IPeriodTimeline> source, IEnumerable<Interval> intervalsNotToContain) =>
        source.Select(t => t.Without(intervalsNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals do not contain any of the intervals in <paramref name="intervalsNotToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Without(this IEnumerable<IPeriodTimeline> source, params Interval[] intervalsNotToContain) =>
        source.Select(t => t.Without(intervalsNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which intervals do not contain any of the intervals in <paramref name="intervalsNotToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Without(this IEnumerable<IPeriodTimeline> source, IPeriodTimeline intervalsNotToContain) =>
        source.Select(t => t.Without(intervalsNotToContain));
}