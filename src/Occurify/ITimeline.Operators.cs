using Occurify.Extensions;

namespace Occurify;

public partial interface ITimeline
{
    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instant"/>.
    /// </summary>
    public static ITimeline operator +(ITimeline source, DateTime instant) => source.Combine(instant);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instants"/>.
    /// </summary>
    public static ITimeline operator +(ITimeline source, IEnumerable<DateTime> instants) => source.Combine(instants);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instants"/>.
    /// </summary>
    public static ITimeline operator +(ITimeline source, ITimeline instants) => source.Combine(instants);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instants"/>.
    /// </summary>
    public static ITimeline operator +(ITimeline source, IEnumerable<ITimeline> instants) => source.Combine(instants);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that does not contain <paramref name="instantToExclude"/>.
    /// </summary>
    public static ITimeline operator -(ITimeline source, DateTime instantToExclude) => source.Without(instantToExclude);

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static ITimeline operator -(ITimeline source, IEnumerable<DateTime> instantsToExclude) => source.Without(instantsToExclude);

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static ITimeline operator -(ITimeline source, ITimeline instantsToExclude) => source.Without(instantsToExclude);

    /// <summary>
    /// Filters <paramref name="instantsToExclude"/> from <paramref name="source"/>.
    /// </summary>
    public static ITimeline operator -(ITimeline source, IEnumerable<ITimeline> instantsToExclude) => source.Without(instantsToExclude);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instant"/>.
    /// </summary>
    public static ITimeline operator |(ITimeline source, DateTime instant) => source.Combine(instant);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instants"/>.
    /// </summary>
    public static ITimeline operator |(ITimeline source, IEnumerable<DateTime> instants) => source.Combine(instants);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instants"/>.
    /// </summary>
    public static ITimeline operator |(ITimeline source, ITimeline instants) => source.Combine(instants);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with the instants from <paramref name="source"/> and <paramref name="instants"/>.
    /// </summary>
    public static ITimeline operator |(ITimeline source, IEnumerable<ITimeline> instants) => source.Combine(instants);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> that contains <paramref name="instantToContain"/> if it is also present in <paramref name="source"/>.
    /// </summary>
    public static ITimeline operator &(ITimeline source, DateTime instantToContain) => source.Containing(instantToContain);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static ITimeline operator &(ITimeline source, IEnumerable<DateTime> instantsToContain) => source.Containing(instantsToContain);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static ITimeline operator &(ITimeline source, ITimeline instantsToContain) => source.Containing(instantsToContain);

    /// <summary>
    /// Filters <paramref name="source"/> based on which instants are also present in <paramref name="instantsToContain"/>.
    /// </summary>
    public static ITimeline operator &(ITimeline source, IEnumerable<ITimeline> instantsToContain) => source.Containing(instantsToContain);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline operator +(ITimeline source, TimeSpan offset) => source.Offset(offset);

    /// <summary>
    /// Offsets <paramref name="source"/> with -<paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline operator -(ITimeline source, TimeSpan offset) => source.Offset(-offset);

#if NET7_0 || NET8_0 || NET9_0
    /// <summary>
    /// Offsets <paramref name="source"/> with -<paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline operator <<(ITimeline source, TimeSpan offset) => source.Offset(-offset);

    /// <summary>
    /// Offsets <paramref name="source"/> with <paramref name="offset"/>. Overflow on <c>DateTime.MinValue</c> or <c>DateTime.MaxValue</c> results in <c>null</c>.
    /// </summary>
    public static ITimeline operator >>(ITimeline source, TimeSpan offset) => source.Offset(offset);
#endif
}