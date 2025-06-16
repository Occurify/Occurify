using NodaTime;

namespace Occurify.Extensions;

public static partial class PeriodTimelineCollectionExtensions
{
    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Within(this IEnumerable<IPeriodTimeline> source, Interval mask) =>
        source.Select(t => t.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Within(this IEnumerable<IPeriodTimeline> source, IEnumerable<Period> mask) =>
        source.Select(t => t.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Within(this IEnumerable<IPeriodTimeline> source, params Period[] mask) =>
        source.Select(t => t.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Within(this IEnumerable<IPeriodTimeline> source, IPeriodTimeline mask) =>
        source.Select(t => t.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods not in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Outside(this IEnumerable<IPeriodTimeline> source, Period mask) =>
        source.Select(t => t.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Outside(this IEnumerable<IPeriodTimeline> source, IEnumerable<Period> mask) =>
        source.Select(t => t.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Outside(this IEnumerable<IPeriodTimeline> source, params Period[] mask) =>
        source.Select(t => t.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Outside(this IEnumerable<IPeriodTimeline> source, IPeriodTimeline mask) =>
        source.Select(t => t.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain <paramref name="periodToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, Period periodToContain) =>
        source.Select(t => t.Containing(periodToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, IEnumerable<Period> periodsToContain) =>
        source.Select(t => t.Containing(periodsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, params Period[] periodsToContain) =>
        source.Select(t => t.Containing(periodsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, IPeriodTimeline periodsToContain) =>
        source.Select(t => t.Containing(periodsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain <paramref name="instantToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, Instant instantToContain) =>
        source.Select(t => t.Containing(instantToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, IEnumerable<Instant> instantsToContain) =>
        source.Select(t => t.Containing(instantsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, params Instant[] instantsToContain) =>
        source.Select(t => t.Containing(instantsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Containing(this IEnumerable<IPeriodTimeline> source, ITimeline instantsToContain) =>
        source.Select(t => t.Containing(instantsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain <paramref name="periodNotToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Without(this IEnumerable<IPeriodTimeline> source, Period periodNotToContain) =>
        source.Select(t => t.Without(periodNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Without(this IEnumerable<IPeriodTimeline> source, IEnumerable<Period> periodsNotToContain) =>
        source.Select(t => t.Without(periodsNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Without(this IEnumerable<IPeriodTimeline> source, params Period[] periodsNotToContain) =>
        source.Select(t => t.Without(periodsNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static IEnumerable<IPeriodTimeline> Without(this IEnumerable<IPeriodTimeline> source, IPeriodTimeline periodsNotToContain) =>
        source.Select(t => t.Without(periodsNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on <paramref name="predicate"/>.
    /// Do not use this method lightly: as it always has to evaluate every period, the performance impact might be significant.
    /// In order for Occurify to function properly, <paramref name="predicate"/> should be deterministic. 
    /// </summary>
    public static IEnumerable<IPeriodTimeline> WherePeriods(this IEnumerable<IPeriodTimeline> source, Func<Period, bool> predicate) =>
        source.Select(t => t.WherePeriods(predicate));
}