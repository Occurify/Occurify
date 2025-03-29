
namespace Occurify.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="TimeSpan"/>.
/// </summary>
public static class TimeSpanExtensions
{
    /// <summary>
    /// Attempts to add <paramref name="timeSpanToAdd"/> to <paramref name="timeSpan"/>. If <c>TimeSpan</c> overflows in either direction, <c>null</c> is returned.
    /// </summary>
    public static TimeSpan? AddOrNullOnOverflow(this TimeSpan timeSpan, TimeSpan timeSpanToAdd)
    {
        if (TimeSpan.MaxValue.Ticks - timeSpan.Ticks < timeSpanToAdd.Ticks ||
            timeSpan.Ticks < -timeSpanToAdd.Ticks)
        {
            return null;
        }

        return timeSpan + timeSpanToAdd;
    }
}