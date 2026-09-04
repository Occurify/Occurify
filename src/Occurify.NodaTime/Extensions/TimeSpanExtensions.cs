using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

/// <summary>
/// Provides extension methods for converting <see cref="TimeSpan"/> to NodaTime types.
/// </summary>
public static class TimeSpanExtensions
{
    /// <summary>
    /// Converts <paramref name="timeSpan"/> to a <see cref="Duration"/>. <c>null</c> is converted to <c>null</c>.
    /// </summary>
    public static Duration? ToDuration(this TimeSpan? timeSpan) => timeSpan?.ToDuration();

    internal static Duration ToDuration(this TimeSpan timeSpan) => Duration.FromTimeSpan(timeSpan);
}
