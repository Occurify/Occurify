
using NodaTime;

namespace Occurify.Extensions;

public static partial class PeriodExtensions
{
    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instant"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Period source, Instant instant) => source.AsPeriodTimeline().Cut(instant);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Period source, IEnumerable<Instant> instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Returns a <see cref="IPeriodTimeline"/> in which <paramref name="source"/> is cut at <paramref name="instants"/>.
    /// </summary>
    public static IPeriodTimeline Cut(this Period source, params Instant[] instants) => source.AsPeriodTimeline().Cut(instants);

    /// <summary>
    /// Offsets <paramref name="period"/> with <paramref name="offset"/>. Overflow on <c>Instant.MinValue</c> or <c>Instant.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static Period Offset(this Period period, Duration offset)
    {
        var start = period.Start;
        var end = period.End;
        if (start == null && end == null)
        {
            return period;
        }
        if (start != null)
        {
            start = start.Value.AddOrNullOnOverflow(offset);
            if (offset > Duration.Zero && start == null)
            {
                throw new OverflowException("Start is not allowed to overflow Instant.MaxValue.");
            }
        }
        if (end != null)
        {
            end = end.Value.AddOrNullOnOverflow(offset);
            if (offset < Duration.Zero && end == null)
            {
                throw new OverflowException("End is not allowed to overflow Instant.MinValue.");
            }
        }
        return new Period(start, end);
    }
}