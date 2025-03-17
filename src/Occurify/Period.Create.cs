using Occurify.Extensions;

namespace Occurify;

public partial record Period
{
    /// <summary>
    /// Returns a <c>Period</c> starting at <paramref name="start"/> and ending with <paramref name="end"/>.
    /// <c>null</c> as <paramref name="start"/> means the period has always started.
    /// <c>null</c> as <paramref name="end"/> means the period never ends.
    /// </summary>
    public static Period Create(DateTime? start, DateTime? end)
    {
        if (end < start)
            throw new ArgumentException("End must be greater than or equal to Start.");
        return new Period(start, end);
    }

    /// <summary>
    /// Returns a <c>Period</c> starting at <paramref name="start"/> with duration <paramref name="duration"/>.
    /// If <paramref name="start"/> + <paramref name="duration"/> overflows <c>DateTime.MaxValue</c>, period end will be set to <c>null</c>, meaning the period never ends.
    /// </summary>
    public static Period Create(DateTime start, TimeSpan duration)
    {
        if (duration < TimeSpan.Zero)
            throw new ArgumentException("Duration must be zero or positive.");
        return new Period(start, start.AddOrNullOnOverflow(duration));
    }
}