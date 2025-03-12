using Occurify.Timelines;

namespace Occurify;

public abstract partial class Timeline
{
    /// <summary>
    /// Returns a <see cref="ITimeline"/> with a single instant <paramref name="instant"/>.
    /// </summary>
    public static ITimeline FromInstant(DateTime instant) =>
        new CollectionTimeline(instant);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with a single instant <paramref name="instant"/>.
    /// If <paramref name="instant"/> is <c>null</c>, an empty timeline is returned.
    /// </summary>
    public static ITimeline FromInstant(DateTime? instant) => instant == null ? Empty() : FromInstant(instant.Value);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with all instants in <paramref name="utcDateTimes"/>.
    /// </summary>
    public static ITimeline FromInstants(IEnumerable<DateTime> utcDateTimes) =>
        new CollectionTimeline(utcDateTimes);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with all instants in <paramref name="utcDateTimes"/>.
    /// </summary>
    public static ITimeline FromInstants(params DateTime[] utcDateTimes) =>
        new CollectionTimeline(utcDateTimes);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with instants occuring every <paramref name="period"/>.
    /// <see cref="DateTime.UtcNow"/> will be set as the origin. Meaning there is an instant at that moment, and other instants are calculated using <paramref name="period"/> and that instant.
    /// </summary>
    public static ITimeline Periodic(TimeSpan period)
    {
        return new PeriodicTimeline(DateTime.UtcNow, period);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with instants occuring every <paramref name="period"/>.
    /// <paramref name="origin"/> will be set as the origin. Meaning there is an instant at that moment, and other instants are calculated using <paramref name="period"/> and that instant.
    /// </summary>
    public static ITimeline Periodic(DateTime origin, TimeSpan period)
    {
        return new PeriodicTimeline(origin, period);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> without any instants.
    /// </summary>
    public static ITimeline Empty() =>
        new EmptyTimeline();
}