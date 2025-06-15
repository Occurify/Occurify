using NodaTime;
using Occurify.Extensions;

namespace Occurify.NodaTime.Extensions;

public static partial class DateTimeExtensions
{
    internal static Instant? ToInstant(this DateTime? dateTime)
    {
        if (dateTime == null)
        {
            return null;
        }

        if (dateTime.Value.Kind == DateTimeKind.Utc)
        {
            return Instant.FromDateTimeUtc(dateTime.Value);
        }

        throw new ArgumentException($"{nameof(dateTime)} should be UTC time.", nameof(dateTime));
    }
}