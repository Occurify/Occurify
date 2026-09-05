namespace Occurify.Reactive.Helpers;

internal static class DateTimeGuard
{
    internal static void EnsureUtc(DateTime dateTime, string paramName)
    {
        if (dateTime.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{paramName} should be UTC time.", paramName);
        }
    }
}
