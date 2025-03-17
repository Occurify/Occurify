namespace Occurify.Extensions;

public static partial class DateTimeExtensions
{
    /// <summary>
    /// Creates a new <see cref="System.DateTime" /> object that has the same number of ticks as the specified <see cref="System.DateTime" /> <paramref name="dateTime"/>, but is designated as Coordinated Universal Time (UTC)
    /// </summary>
    public static DateTime AsUtcInstant(this DateTime dateTime) => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);

    /// <summary>
    /// Determines whether <paramref name="source"/> is contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsWithin(this DateTime source, IEnumerable<Period> mask) =>
        mask.ContainsInstant(source);

    /// <summary>
    /// Determines whether <paramref name="source"/> is contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsWithin(this DateTime source, IPeriodTimeline mask) =>
        mask.ContainsInstant(source);

    /// <summary>
    /// Determines whether <paramref name="source"/> is outside all the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsOutside(this DateTime source, IEnumerable<Period> mask) =>
        mask.All(p => !p.ContainsInstant(source));

    /// <summary>
    /// Determines whether <paramref name="source"/> is outside all the periods in <paramref name="mask"/>.
    /// </summary>
    public static bool IsOutside(this DateTime source, IPeriodTimeline mask) =>
        mask.All(p => !p.ContainsInstant(source));

    /// <summary>
    /// Determines whether <paramref name="instant"/> is on <paramref name="timeline"/>.
    /// </summary>
    public static bool IsOnTimeline(this DateTime instant, ITimeline timeline) => timeline.IsInstant(instant);
}