namespace Occurify.Extensions;

public static partial class PeriodTimelineKeyCollectionExtensions
{
    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Within<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Within<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Period> mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Within<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Period[] mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Within<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IPeriodTimeline mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Within(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods not in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Period> mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Period[] mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods are not inside any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IPeriodTimeline mask) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Outside(mask));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain <paramref name="periodToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period periodToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(periodToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Period> periodsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(periodsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Period[] periodsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(periodsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the periods in <paramref name="periodsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IPeriodTimeline periodsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(periodsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain <paramref name="instantToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, DateTime instantToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<DateTime> instantsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params DateTime[] instantsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods contain any of the instants in <paramref name="instantsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, ITimeline instantsToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Containing(instantsToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain <paramref name="periodNotToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Without<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Period periodNotToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(periodNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Without<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IEnumerable<Period> periodsNotToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(periodsNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Without<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, params Period[] periodsNotToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(periodsNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which periods do not contain any of the periods in <paramref name="periodsNotToContain"/>.
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> Without<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, IPeriodTimeline periodsNotToContain) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Without(periodsNotToContain));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on <paramref name="predicate"/>.
    /// Do not use this method lightly: as it always has to evaluate every period, the performance impact might be significant.
    /// In order for Occurify to function properly, <paramref name="predicate"/> should be deterministic. 
    /// </summary>
    public static Dictionary<TKey, IPeriodTimeline> WherePeriods<TKey>(this IEnumerable<KeyValuePair<TKey, IPeriodTimeline>> source, Func<Period, bool> predicate) where TKey : notnull =>
        source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.WherePeriods(predicate));
}