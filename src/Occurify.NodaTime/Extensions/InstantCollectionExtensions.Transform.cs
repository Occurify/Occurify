
using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IEnumerable{Instant}"/>.
/// </summary>
public static partial class InstantCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="startInstants"/> and ending with <paramref name="periodEndInstants"/>.
    /// </summary>
    public static IPeriodTimeline To(this IEnumerable<Instant> startInstants, IEnumerable<Instant> periodEndInstants) =>
        startInstants.AsTimeline().To(periodEndInstants.AsTimeline());

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="startInstants"/> and ending with <paramref name="periodEndInstants"/>.
    /// </summary>
    public static IPeriodTimeline To(this IEnumerable<Instant> startInstants, params Instant[] periodEndInstants) =>
        startInstants.AsTimeline().To(periodEndInstants.AsTimeline());

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with all instants in <paramref name="source"/>.
    /// </summary>
    public static ITimeline AsTimeline(this IEnumerable<Instant> source) => Timeline.FromInstants(source.Select(i => i.ToDateTimeUtc()));

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending with instants in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline AsConsecutivePeriodTimeline(this IEnumerable<Instant> source) => PeriodTimeline.FromInstantsAsConsecutive(source.Select(i => i.ToDateTimeUtc()));
}