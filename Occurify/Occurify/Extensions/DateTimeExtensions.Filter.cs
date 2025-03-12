namespace Occurify.Extensions;

public static partial class DateTimeExtensions
{
    /// <summary>
    /// Filters a sequence of DateTime based on whether they are contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<DateTime> Within(this IEnumerable<DateTime> source, IEnumerable<Period> mask) =>
        source.Where(mask.ContainsInstant);

    /// <summary>
    /// Filters a sequence of DateTime based on whether they are contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<DateTime> Within(this IEnumerable<DateTime> source, IPeriodTimeline mask) =>
        source.Where(mask.ContainsInstant);

    /// <summary>
    /// Filters a sequence of DateTime based on whether they are not contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<DateTime> Outside(this IEnumerable<DateTime> source, IEnumerable<Period> mask) =>
        source.Where(i => !mask.ContainsInstant(i));

    /// <summary>
    /// Filters a sequence of DateTime based on whether they are not contained in any of the periods in <paramref name="mask"/>.
    /// </summary>
    public static IEnumerable<DateTime> Outside(this IEnumerable<DateTime> source, IPeriodTimeline mask) =>
        source.Where(i => !mask.ContainsInstant(i));
}