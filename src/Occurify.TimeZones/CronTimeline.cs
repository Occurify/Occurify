using Cronos;
using Occurify.TimeZones.Helpers;

namespace Occurify.TimeZones
{
    internal class CronTimeline : Timeline
    {
        private readonly CronExpression _cronExpression;
        private readonly TimeZoneInfo _timeZoneInfo;

        internal CronTimeline(
            string cronExpression,
            TimeZoneInfo timeZoneInfo)
        {
            _cronExpression = CronExpression.Parse(cronExpression, CronHelper.ResolveCronFormat(cronExpression));
            _timeZoneInfo = timeZoneInfo;
        }

        internal CronTimeline(
            string cronExpression,
            CronFormat cronFormat,
            TimeZoneInfo timeZoneInfo)
        {
            _cronExpression = CronExpression.Parse(cronExpression, cronFormat);
            _timeZoneInfo = timeZoneInfo;
        }

        public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
        {
            if (utcRelativeTo.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
            }

            return _cronExpression.GetPreviousOccurrence(utcRelativeTo, _timeZoneInfo);
        }

        public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
        {
            if (utcRelativeTo.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
            }

            return _cronExpression.GetNextOccurrence(utcRelativeTo, _timeZoneInfo);
        }

        public override bool IsInstant(DateTime utcDateTime)
        {
            if (utcDateTime.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"{nameof(utcDateTime)} should be UTC time.");
            }

            var nextIncludingCurrent = _cronExpression.GetNextOccurrence(utcDateTime, _timeZoneInfo, true);
            return nextIncludingCurrent == utcDateTime;
        }
    }
}