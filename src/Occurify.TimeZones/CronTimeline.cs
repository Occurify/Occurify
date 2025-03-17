using Cronos;
using Occurify.Extensions;
using Occurify.TimeZones.Extensions;
using Occurify.TimeZones.Helpers;

namespace Occurify.TimeZones
{
    internal class CronTimeline : Timeline
    {
        private const int MaxYear = 2499; // This is a limitation of Cronos

        private readonly CronExpression _cronExpression;
        private readonly TimeZoneInfo _timeZoneInfo;
        private readonly TimeSpan? _firstPeriodDuration;
        private readonly DateTime _maxCronosTime = DateTime.SpecifyKind(new DateTime(MaxYear + 1, 1, 1), DateTimeKind.Utc)
            .Subtract(TimeSpan.FromTicks(1));

        internal CronTimeline(
            string cronExpression,
            TimeZoneInfo timeZoneInfo)
        {
            _cronExpression = CronExpression.Parse(cronExpression, CronHelper.ResolveCronFormat(cronExpression));
            _timeZoneInfo = timeZoneInfo;

            _firstPeriodDuration = _cronExpression.GetFirstPeriodDuration(timeZoneInfo);
        }

        internal CronTimeline(
            string cronExpression,
            CronFormat cronFormat,
            TimeZoneInfo timeZoneInfo)
        {
            _cronExpression = CronExpression.Parse(cronExpression, cronFormat);
            _timeZoneInfo = timeZoneInfo;

            _firstPeriodDuration = _cronExpression.GetFirstPeriodDuration(timeZoneInfo);
        }

        public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
        {
            if (utcRelativeTo.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
            }

            if (utcRelativeTo.Year > MaxYear)
            {
                utcRelativeTo = _maxCronosTime;
            }

            if (_firstPeriodDuration == null)
            {
                // In this case the cron expression returns maximum one result.
                var result = _cronExpression.GetNextOccurrence(utcRelativeTo, _timeZoneInfo, inclusive: true);
                if (result == null || result.Value >= utcRelativeTo)
                {
                    return null;
                }

                return result.Value;
            }

            var periodEstimate = _firstPeriodDuration.Value * 1.5;
            DateTime[] occurrences;
            var attempt = 0;
            do
            {
                var fromUtc = utcRelativeTo.AddOrNullOnOverflow(-periodEstimate * (attempt + 1));
                var toUtc = utcRelativeTo.AddOrNullOnOverflow(-periodEstimate * attempt);
                if (fromUtc == null && toUtc == null)
                {
                    return null;
                }

                if (fromUtc == null)
                {
                    occurrences = _cronExpression.GetOccurrences(new(0L, DateTimeKind.Utc), toUtc!.Value,
                        _timeZoneInfo, fromInclusive: true, toInclusive: false).ToArray();
                    if (!occurrences.Any())
                    {
                        return null;
                    }
                }
                else
                {
                    occurrences = _cronExpression.GetOccurrences(fromUtc.Value, toUtc!.Value, _timeZoneInfo,
                        fromInclusive: true, toInclusive: false).ToArray();
                }

                attempt++;
            } while (!occurrences.Any());

            return DateTime.SpecifyKind(occurrences.Last(), DateTimeKind.Utc);
        }

        public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
        {
            if (utcRelativeTo.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
            }
            if (utcRelativeTo.Year > MaxYear)
            {
                return null;
            }

            return _cronExpression.GetNextOccurrence(utcRelativeTo, _timeZoneInfo);
        }

        public override bool IsInstant(DateTime utcDateTime)
        {
            if (utcDateTime.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
            }
            if (utcDateTime.Year > MaxYear)
            {
                return false;
            }

            var nextIncludingCurrent = _cronExpression.GetNextOccurrence(utcDateTime, _timeZoneInfo, true);
            return nextIncludingCurrent == utcDateTime;
        }
    }
}