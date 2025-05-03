namespace Occurify.Extensions;

public static partial class PeriodTimelineValueCollectionExtensions
{
    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Within<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period mask) =>
        source.ToDictionary(kvp => kvp.Key.Within(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Within<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Period> mask) =>
        source.ToDictionary(kvp => kvp.Key.Within(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Within<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Period[] mask) =>
        source.ToDictionary(kvp => kvp.Key.Within(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Within<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IPeriodTimeline mask) =>
        source.ToDictionary(kvp => kvp.Key.Within(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods not in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Outside<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period mask) =>
        source.ToDictionary(kvp => kvp.Key.Outside(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Outside<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Period> mask) =>
        source.ToDictionary(kvp => kvp.Key.Outside(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Outside<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Period[] mask) =>
        source.ToDictionary(kvp => kvp.Key.Outside(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Outside<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IPeriodTimeline mask) =>
        source.ToDictionary(kvp => kvp.Key.Outside(mask), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain <paramref name="periodToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period periodToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(periodToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Period> periodsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(periodsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Period[] periodsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(periodsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IPeriodTimeline periodsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(periodsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain <paramref name="instantToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, DateTime instantToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<DateTime> instantsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params DateTime[] instantsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Containing<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, ITimeline instantsToContain) =>
        source.ToDictionary(kvp => kvp.Key.Containing(instantsToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain <paramref name="periodNotToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Without<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Period periodNotToContain) =>
        source.ToDictionary(kvp => kvp.Key.Without(periodNotToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Without<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IEnumerable<Period> periodsNotToContain) =>
        source.ToDictionary(kvp => kvp.Key.Without(periodsNotToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Without<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, params Period[] periodsNotToContain) =>
        source.ToDictionary(kvp => kvp.Key.Without(periodsNotToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> Without<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, IPeriodTimeline periodsNotToContain) =>
        source.ToDictionary(kvp => kvp.Key.Without(periodsNotToContain), kvp => kvp.Value);

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on <paramref name="predicate"/>.
    /// Do not use this method lightly: as it always has to evaluate every period, the performance impact might be significant.
    /// In order for Occurify to function properly, <paramref name="predicate"/> should be deterministic. 
    /// </summary>
    public static IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> WherePeriods<TValue>(this IEnumerable<KeyValuePair<IPeriodTimeline, TValue>> source, Func<Period, bool> predicate) =>
        source.ToDictionary(kvp => kvp.Key.WherePeriods(predicate), kvp => kvp.Value);
}