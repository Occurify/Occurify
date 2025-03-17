namespace Occurify.Tests.Helpers;

internal class DateTimeHelper
{
    internal static DateTime MinValueUtc = new(0L, DateTimeKind.Utc);
    internal static DateTime MaxValueUtc = DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);
}