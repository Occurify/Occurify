
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="IEnumerable{DateTime}"/>.
/// </summary>
public static partial class DateTimeCollectionExtensions
{
    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="startInstants"/> and ending with <paramref name="periodEndInstants"/>.
    /// </summary>
    public static IPeriodTimeline To(this IEnumerable<DateTime> startInstants, IEnumerable<DateTime> periodEndInstants) =>
        TimelineExtensions.To(startInstants.AsTimeline(), periodEndInstants.AsTimeline());

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="startInstants"/> and ending with <paramref name="periodEndInstants"/>.
    /// </summary>
    public static IPeriodTimeline To(this IEnumerable<DateTime> startInstants, params DateTime[] periodEndInstants) =>
        TimelineExtensions.To(startInstants.AsTimeline(), periodEndInstants.AsTimeline());

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="startInstants"/> and ending with <paramref name="periodEndTimeline"/>.
    /// </summary>
    public static IPeriodTimeline To(this IEnumerable<DateTime> startInstants, ITimeline periodEndTimeline) =>
        TimelineExtensions.To(startInstants.AsTimeline(), periodEndTimeline);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with all instants in <paramref name="source"/>.
    /// </summary>
    public static ITimeline AsTimeline(this IEnumerable<DateTime> source) => Timeline.FromInstants(source);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> with consecutive periods starting and ending with instants in <paramref name="source"/>.
    /// </summary>
    public static IPeriodTimeline AsConsecutivePeriodTimeline(this IEnumerable<DateTime> source) => PeriodTimeline.FromInstantsAsConsecutive(source);
}