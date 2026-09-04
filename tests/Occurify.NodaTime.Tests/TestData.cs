using NodaTime;

namespace Occurify.NodaTime.Tests;

internal static class TestData
{
    public static DateTime Utc(int year, int month, int day, int hour = 0, int minute = 0) =>
        new(year, month, day, hour, minute, 0, DateTimeKind.Utc);

    public static Instant At(int year, int month, int day, int hour = 0, int minute = 0) =>
        Instant.FromDateTimeUtc(Utc(year, month, day, hour, minute));

    public static Interval Between(Instant? start, Instant? end) => new(start, end);

    public static Period PeriodOf(DateTime? start, DateTime? end) => new(start, end);
}
