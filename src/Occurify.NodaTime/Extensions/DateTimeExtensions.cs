using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

/// <summary>
/// Provides extension methods for converting <see cref="DateTime"/> to NodaTime types.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Converts <paramref name="utcDateTime"/> to an <see cref="Instant"/>. <paramref name="utcDateTime"/> is required to be of kind <see cref="DateTimeKind.Utc"/>.
    /// <c>null</c> is converted to <c>null</c>.
    /// </summary>
    public static Instant? ToInstant(this DateTime? utcDateTime) => utcDateTime?.ToInstant();

    internal static Instant ToInstant(this DateTime utcDateTime)
    {
        if (utcDateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.", nameof(utcDateTime));
        }

        return Instant.FromDateTimeUtc(utcDateTime);
    }
}
