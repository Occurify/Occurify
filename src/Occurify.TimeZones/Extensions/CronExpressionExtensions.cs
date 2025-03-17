using Cronos;

namespace Occurify.TimeZones.Extensions
{
    internal static class CronExpressionExtensions
    {
        internal static TimeSpan? GetFirstPeriodDuration(this CronExpression cronExpression, TimeZoneInfo timeZone)
        {
            // Note: there is a bug in Cronos causing it to think there is no result if the first result is 0 ticks. Therefor we start at tick 1.
            var first = cronExpression.GetNextOccurrence(new (1, DateTimeKind.Utc), timeZone, inclusive: true);
            if (first == null)
            {
                return null;
            }

            var second = cronExpression.GetNextOccurrence(first.Value, timeZone, inclusive: false);
            if (second == null)
            {
                return null;
            }

            return second.Value - first.Value;
        }
    }
}
