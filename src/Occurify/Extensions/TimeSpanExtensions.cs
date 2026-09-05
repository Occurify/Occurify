
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
        if ((timeSpanToAdd.Ticks > 0 && timeSpan.Ticks > TimeSpan.MaxValue.Ticks - timeSpanToAdd.Ticks) ||
            (timeSpanToAdd.Ticks < 0 && timeSpan.Ticks < TimeSpan.MinValue.Ticks - timeSpanToAdd.Ticks))
        {
            return null;
        }

        return timeSpan + timeSpanToAdd;
    }
}
