using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="Instant"/>.
/// </summary>
public static partial class InstantExtensions
{
    /// <summary>
    /// Determines whether <paramref name="source"/> is contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsWithin(this Instant source, IEnumerable<Period> mask) =>
        mask.ContainsInstant(source);

    /// <summary>
    /// Determines whether <paramref name="source"/> is contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsWithin(this Instant source, params Period[] mask) =>
        mask.ContainsInstant(source);

    /// <summary>
    /// Determines whether <paramref name="source"/> is contained in any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static bool IsWithin(this Instant source, IEnumerable<Interval> mask) =>
        mask.ContainsInstant(source);

    /// <summary>
    /// Determines whether <paramref name="source"/> is contained in any of the intervals in <paramref name="mask"/>.
    /// </summary>
    public static bool IsWithin(this Instant source, params Interval[] mask) =>
        mask.ContainsInstant(source);

    /// <summary>
    /// Determines whether <paramref name="source"/> is contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsWithin(this Instant source, IPeriodTimeline mask) =>
        mask.ContainsInstant(source);

    /// <summary>
    /// Determines whether <paramref name="source"/> is contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsWithin(this Instant source, IEnumerable<IPeriodTimeline> mask) =>
        mask.ContainsInstant(source);

    /// <summary>
    /// Determines whether <paramref name="source"/> is contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsWithin(this Instant source, params IPeriodTimeline[] mask) =>
        mask.ContainsInstant(source);

    /// <summary>
    /// Determines whether <paramref name="source"/> is outside all the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsOutside(this Instant source, IEnumerable<Period> mask) =>
        mask.All(p => !p.ContainsInstant(source));

    /// <summary>
    /// Determines whether <paramref name="source"/> is outside all the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsOutside(this Instant source, params Period[] mask) =>
        mask.All(p => !p.ContainsInstant(source));

    /// <summary>
    /// Determines whether <paramref name="source"/> is outside all the intervals in <paramref name="mask"/>.
    /// </summary>
    public static bool IsOutside(this Instant source, IEnumerable<Interval> mask) =>
        mask.All(p => !p.Contains(source));

    /// <summary>
    /// Determines whether <paramref name="source"/> is outside all the intervals in <paramref name="mask"/>.
    /// </summary>
    public static bool IsOutside(this Instant source, params Interval[] mask) =>
        mask.All(p => !p.Contains(source));

    /// <summary>
    /// Determines whether <paramref name="source"/> is outside all the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsOutside(this Instant source, IPeriodTimeline mask) =>
        mask.All(p => !p.ContainsInstant(source));

    /// <summary>
    /// Determines whether <paramref name="source"/> is outside all the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsOutside(this Instant source, IEnumerable<IPeriodTimeline> mask) =>
        mask.All(p => !p.ContainsInstant(source));

    /// <summary>
    /// Determines whether <paramref name="source"/> is outside all the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsOutside(this Instant source, params IPeriodTimeline[] mask) =>
        mask.All(p => !p.ContainsInstant(source));

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on <paramref name="timeline"/>.
    /// </summary>
    public static bool IsOnTimeline(this Instant instant, ITimeline timeline) => timeline.IsInstant(instant);

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of <paramref name="timelines"/>.
    /// </summary>
    public static bool IsOnAnyTimeline(this Instant instant, IEnumerable<ITimeline> timelines) => 
        timelines.Any(tl => tl.IsInstant(instant));

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on any of <paramref name="timelines"/>.
    /// </summary>
    public static bool IsOnAnyTimeline(this Instant instant, params ITimeline[] timelines) =>
        timelines.Any(tl => tl.IsInstant(instant));
}