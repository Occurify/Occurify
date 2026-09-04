using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class TimelineKeyCollectionExtensions
{
    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; in which the first <paramref name="count"/> instants of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/> are bypassed.
    /// </summary>
    public static Dictionary<TKey, ITimeline> SkipWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Interval mask, int count) where TKey : notnull =>
        source.SkipWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; in which the first <paramref name="count"/> instants of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/> are bypassed.
    /// </summary>
    public static Dictionary<TKey, ITimeline> SkipWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Interval> mask, int count) where TKey : notnull =>
        source.SkipWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; in which the last <paramref name="count"/> instants of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/> are omitted.
    /// </summary>
    public static Dictionary<TKey, ITimeline> SkipLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Interval mask, int count) where TKey : notnull =>
        source.SkipLastWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; in which the last <paramref name="count"/> instants of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/> are omitted.
    /// </summary>
    public static Dictionary<TKey, ITimeline> SkipLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Interval> mask, int count) where TKey : notnull =>
        source.SkipLastWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; that contains the first <paramref name="count"/> instants of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> TakeWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Interval mask, int count) where TKey : notnull =>
        source.TakeWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; that contains the first <paramref name="count"/> instants of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> TakeWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Interval> mask, int count) where TKey : notnull =>
        source.TakeWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; that contains the last <paramref name="count"/> instants of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> TakeLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Interval mask, int count) where TKey : notnull =>
        source.TakeLastWithin(mask.ToPeriod(), count);

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; that contains the last <paramref name="count"/> instants of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> TakeLastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Interval> mask, int count) where TKey : notnull =>
        source.TakeLastWithin(mask.Select(i => i.ToPeriod()), count);

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; that contains the first instant of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> FirstWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Interval mask) where TKey : notnull =>
        source.FirstWithin(mask.ToPeriod());

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; that contains the first instant of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> FirstWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Interval> mask) where TKey : notnull =>
        source.FirstWithin(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; that contains the last instant of the timelines in <paramref name="source"/> within the interval <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> LastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Interval mask) where TKey : notnull =>
        source.LastWithin(mask.ToPeriod());

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; that contains the last instant of the timelines in <paramref name="source"/> within every interval in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> LastWithin<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Interval> mask) where TKey : notnull =>
        source.LastWithin(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; that contains <paramref name="instantToContain"/> if it is also present in the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instantToContain) where TKey : notnull =>
        source.Containing(instantToContain.ToDateTimeUtc());

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Instant> instantsToContain) where TKey : notnull =>
        source.Containing(instantsToContain.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Containing<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, params Instant[] instantsToContain) where TKey : notnull =>
        source.Containing(instantsToContain.Select(i => i.ToDateTimeUtc()).ToArray());

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are inside <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Within<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Interval mask) where TKey : notnull =>
        source.Within(mask.ToPeriod());

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Within<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Interval> mask) where TKey : notnull =>
        source.Within(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Within<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, params Interval[] mask) where TKey : notnull =>
        source.Within(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are not inside <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Interval mask) where TKey : notnull =>
        source.Outside(mask.ToPeriod());

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Interval> mask) where TKey : notnull =>
        source.Outside(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on which instants are not inside any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Outside<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, params Interval[] mask) where TKey : notnull =>
        source.Outside(mask.Select(i => i.ToPeriod()));

    /// <summary>
    /// Returns a Dictionary&lt;TKey, ITimeline&gt; in which the timelines do not contain <paramref name="instantToExclude"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Without<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Instant instantToExclude) where TKey : notnull =>
        source.Without(instantToExclude.ToDateTimeUtc());

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Without<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, IEnumerable<Instant> instantsToExclude) where TKey : notnull =>
        source.Without(instantsToExclude.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from the timelines in <paramref name="source"/>.
    /// </summary>
    public static Dictionary<TKey, ITimeline> Without<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, params Instant[] instantsToExclude) where TKey : notnull =>
        source.Without(instantsToExclude.Select(i => i.ToDateTimeUtc()).ToArray());

    /// <summary>
    /// Filters the timelines in <paramref name="source"/> based on <paramref name="predicate"/>.
    /// Do not use this method lightly: as it always has to evaluate every instant, the performance impact might be significant.
    /// In order for Occurify to function properly, <paramref name="predicate"/> should be deterministic.
    /// </summary>
    /// <remarks>
    /// When both <c>Occurify.Extensions</c> and <c>Occurify.NodaTime.Extensions</c> are imported, type the lambda parameter (<c>(Instant i) => ...</c>) to disambiguate from the <see cref="DateTime"/> overload.
    /// </remarks>
    public static Dictionary<TKey, ITimeline> WhereInstants<TKey>(this IEnumerable<KeyValuePair<TKey, ITimeline>> source, Func<Instant, bool> predicate) where TKey : notnull =>
        source.WhereInstants((DateTime dt) => predicate(dt.ToInstant()));
}
