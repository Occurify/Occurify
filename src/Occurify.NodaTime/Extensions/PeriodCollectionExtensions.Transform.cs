
using NodaTime;

namespace Occurify.Extensions;

public static partial class PeriodCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instant"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, Instant instant) => source.AsPeriodTimeline().Cut(instant);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, IEnumerable<Instant> instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which periods in <paramref name="source"/> are cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this IEnumerable<Period> source, params Instant[] instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static IEnumerable<Period> Offset(this IEnumerable<Period> source, Duration offset) => source.Select(p => PeriodExtensions.Offset(p, offset));
}