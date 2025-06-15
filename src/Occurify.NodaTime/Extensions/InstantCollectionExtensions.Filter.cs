using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class InstantCollectionExtensions
{
    /// <summary>
    /// Filters a sequence of Instant based on whether they are contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Within(this IEnumerable<Instant> source, IEnumerable<Period> mask) =>
        source.Where(mask.ContainsInstant);

    /// <summary>
    /// Filters a sequence of Instant based on whether they are contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Within(this IEnumerable<Instant> source, params Period[] mask) =>
        source.Where(mask.ContainsInstant);

    /// <summary>
    /// Filters a sequence of Instant based on whether they are contained in any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Within(this IEnumerable<Instant> source, IEnumerable<Interval> mask) =>
        source.Where(mask.ContainsInstant);

    /// <summary>
    /// Filters a sequence of Instant based on whether they are contained in any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Within(this IEnumerable<Instant> source, params Interval[] mask) =>
        source.Where(mask.ContainsInstant);

    /// <summary>
    /// Filters a sequence of Instant based on whether they are contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Within(this IEnumerable<Instant> source, IPeriodTimeline mask) =>
        source.Where(mask.ContainsInstant);

    /// <summary>
    /// Filters a sequence of Instant based on whether they are contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Within(this IEnumerable<Instant> source, IEnumerable<IPeriodTimeline> mask) =>
        source.Where(mask.ContainsInstant);

    /// <summary>
    /// Filters a sequence of Instant based on whether they are contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Within(this IEnumerable<Instant> source, params IPeriodTimeline[] mask) =>
        source.Where(mask.ContainsInstant);

    /// <summary>
    /// Filters a sequence of Instant based on whether they are not contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Outside(this IEnumerable<Instant> source, IEnumerable<Period> mask) =>
        source.Where(i => !mask.ContainsInstant(i));

    /// <summary>
    /// Filters a sequence of Instant based on whether they are not contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Outside(this IEnumerable<Instant> source, params Period[] mask) =>
        source.Where(i => !mask.ContainsInstant(i));

    /// <summary>
    /// Filters a sequence of Instant based on whether they are not contained in any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Outside(this IEnumerable<Instant> source, IEnumerable<Interval> mask) =>
        source.Where(i => !mask.ContainsInstant(i));

    /// <summary>
    /// Filters a sequence of Instant based on whether they are not contained in any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Outside(this IEnumerable<Instant> source, params Interval[] mask) =>
        source.Where(i => !mask.ContainsInstant(i));

    /// <summary>
    /// Filters a sequence of Instant based on whether they are not contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Outside(this IEnumerable<Instant> source, IPeriodTimeline mask) =>
        source.Where(i => !mask.ContainsInstant(i));

    /// <summary>
    /// Filters a sequence of Instant based on whether they are not contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Outside(this IEnumerable<Instant> source, IEnumerable<IPeriodTimeline> mask) =>
        source.Where(i => !mask.ContainsInstant(i));

    /// <summary>
    /// Filters a sequence of Instant based on whether they are not contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<Instant> Outside(this IEnumerable<Instant> source, params IPeriodTimeline[] mask) =>
        source.Where(i => !mask.ContainsInstant(i));
}