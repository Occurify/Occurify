using Cronos;
using Occurify.Extensions;

namespace Occurify.TimeZones
{
    public class TimeZonePeriods
    {
        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting at hour <paramref name="startHour"/> and ending at hour <paramref name="endHour"/> in <see cref="TimeZoneInfo.Local"/>.
        /// These periods can span more than one day.
        /// </summary>
        public static IPeriodTimeline Between(int startHour, int endHour) =>
            Between(startHour, endHour, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting at hour <paramref name="startHour"/> and ending at hour <paramref name="endHour"/> in provided timezone <paramref name="timeZone"/>.
        /// These periods can span more than one day.
        /// </summary>
        public static IPeriodTimeline Between(int startHour, int endHour, TimeZoneInfo timeZone)
        {
            return TimeZoneInstants.DailyAt(startHour, timeZone).To(TimeZoneInstants.DailyAt(endHour, timeZone));
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="startTime"/> and ending at <paramref name="endTime"/> in <see cref="TimeZoneInfo.Local"/>.
        /// These periods can span more than one day.
        /// </summary>
        public static IPeriodTimeline Between(TimeOnly startTime, TimeOnly endTime) =>
            Between(startTime, endTime, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="startTime"/> and ending at <paramref name="endTime"/> in provided timezone <paramref name="timeZone"/>.
        /// These periods can span more than one day.
        /// </summary>
        public static IPeriodTimeline Between(TimeOnly startTime, TimeOnly endTime, TimeZoneInfo timeZone)
        {
            return TimeZoneInstants.DailyAt(startTime, timeZone).To(TimeZoneInstants.DailyAt(endTime, timeZone));
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="periodStartTimeline"/> and ending with <paramref name="periodEndTimeline"/> within the same day in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline DailyBetween(ITimeline periodStartTimeline, ITimeline periodEndTimeline) =>
            DailyBetween(periodStartTimeline, periodEndTimeline, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting at <paramref name="periodStartTimeline"/> and ending with <paramref name="periodEndTimeline"/> within the same day in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline DailyBetween(ITimeline periodStartTimeline, ITimeline periodEndTimeline, TimeZoneInfo timeZone)
        {
            return periodStartTimeline.To(periodEndTimeline).Within(Days(timeZone));
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with consecutive whole hours in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Hours() => Hours(TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with consecutive whole hours in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Hours(TimeZoneInfo timeZone)
        {
            return TimeZoneInstants.Hourly(timeZone).AsConsecutivePeriodTimeline();
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with hours that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Hours(string cronExpression) => Hours(cronExpression, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with hours that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Hours(string cronExpression, TimeZoneInfo timeZone)
        {
            return Hours(TimeZoneInstants.FromCron(cronExpression, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with hours that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Hours(string cronExpression, CronFormat cronFormat) => Hours(cronExpression, cronFormat, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with hours that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Hours(string cronExpression, CronFormat cronFormat, TimeZoneInfo timeZone)
        {
            return Hours(TimeZoneInstants.FromCron(cronExpression, cronFormat, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with hours that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Hours(ITimeline instantsToContain)
        {
            return Hours().Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with hours that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Hours(ITimeline instantsToContain, TimeZoneInfo timeZone)
        {
            return Hours(timeZone).Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with consecutive days in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Days() => Days(TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with consecutive days in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Days(TimeZoneInfo timeZone)
        {
            return TimeZoneInstants.Daily(timeZone).AsConsecutivePeriodTimeline();
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with days <paramref name="dayOfWeek"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Days(DayOfWeek dayOfWeek) => Days(dayOfWeek, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with days <paramref name="dayOfWeek"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Days(DayOfWeek dayOfWeek, TimeZoneInfo timeZone) => Days(new[] { dayOfWeek }, timeZone);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with days <paramref name="daysOfWeek"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Days(IEnumerable<DayOfWeek> daysOfWeek) => Days(daysOfWeek, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with days <paramref name="daysOfWeek"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Days(IEnumerable<DayOfWeek> daysOfWeek, TimeZoneInfo timeZone)
        {
            daysOfWeek = daysOfWeek.ToArray();
            return TimeZoneInstants.StartOfDays(daysOfWeek, timeZone).To(TimeZoneInstants.EndOfDays(daysOfWeek, timeZone));
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with days <paramref name="daysOfWeek"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Days(params DayOfWeek[] daysOfWeek) => Days(daysOfWeek, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with days that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Days(string cronExpression) => Days(cronExpression, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with days that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Days(string cronExpression, TimeZoneInfo timeZone)
        {
            return Days(TimeZoneInstants.FromCron(cronExpression, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with days that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Days(string cronExpression, CronFormat cronFormat) => Days(cronExpression, cronFormat, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with days that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Days(string cronExpression, CronFormat cronFormat, TimeZoneInfo timeZone)
        {
            return Days(TimeZoneInstants.FromCron(cronExpression, cronFormat, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with days that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Days(ITimeline instantsToContain)
        {
            return Days().Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with days that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Days(ITimeline instantsToContain, TimeZoneInfo timeZone)
        {
            return Days(timeZone).Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with consecutive weeks in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Weeks() => Weeks(TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with consecutive weeks in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Weeks(TimeZoneInfo timeZone)
        {
            return TimeZoneInstants.Weekly(timeZone).AsConsecutivePeriodTimeline();
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weeks that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Weeks(string cronExpression) => Weeks(cronExpression, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weeks that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Weeks(string cronExpression, TimeZoneInfo timeZone)
        {
            return Weeks(TimeZoneInstants.FromCron(cronExpression, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weeks that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Weeks(string cronExpression, CronFormat cronFormat) => Weeks(cronExpression, cronFormat, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weeks that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Weeks(string cronExpression, CronFormat cronFormat, TimeZoneInfo timeZone)
        {
            return Weeks(TimeZoneInstants.FromCron(cronExpression, cronFormat, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weeks that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Weeks(ITimeline instantsToContain)
        {
            return Weeks().Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weeks that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Weeks(ITimeline instantsToContain, TimeZoneInfo timeZone)
        {
            return Weeks(timeZone).Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with consecutive months in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Months() => Months(TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with consecutive months in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Months(TimeZoneInfo timeZone)
        {
            return TimeZoneInstants.Monthly(timeZone).AsConsecutivePeriodTimeline();
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with months <paramref name="month"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Months(int month, TimeZoneInfo timeZone) => Months(new[] { month }, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with months <paramref name="months"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Months(IEnumerable<int> months) => Months(months, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with months <paramref name="months"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Months(IEnumerable<int> months, TimeZoneInfo timeZone)
        {
            months = months.ToArray();
            return TimeZoneInstants.StartOfMonths(months, timeZone).To(TimeZoneInstants.EndOfMonths(months, timeZone));
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with months <paramref name="months"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Months(params int[] months) => Months(months, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with months that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Months(string cronExpression) => Months(cronExpression, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with months that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Months(string cronExpression, TimeZoneInfo timeZone)
        {
            return Months(TimeZoneInstants.FromCron(cronExpression, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with months that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Months(string cronExpression, CronFormat cronFormat) => Months(cronExpression, cronFormat, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with months that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Months(string cronExpression, CronFormat cronFormat, TimeZoneInfo timeZone)
        {
            return Months(TimeZoneInstants.FromCron(cronExpression, cronFormat, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with months that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Months(ITimeline instantsToContain)
        {
            return Months().Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with months that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Months(ITimeline instantsToContain, TimeZoneInfo timeZone)
        {
            return Months(timeZone).Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with consecutive years in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Years() => Years(TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with consecutive years in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Years(TimeZoneInfo timeZone)
        {
            return TimeZoneInstants.Annually(timeZone).AsConsecutivePeriodTimeline();
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with years that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Years(ITimeline instantsToContain)
        {
            return Years().Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with years that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Years(ITimeline instantsToContain, TimeZoneInfo timeZone)
        {
            return Years(timeZone).Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with all weekends in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Weekends() => Weekends(TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with all weekends in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Weekends(TimeZoneInfo timeZone)
        {
            return Days(new[] { DayOfWeek.Saturday, DayOfWeek.Sunday }, timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weekends that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Weekends(string cronExpression) => Weekends(cronExpression, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weekends that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Weekends(string cronExpression, TimeZoneInfo timeZone)
        {
            return Weekends(TimeZoneInstants.FromCron(cronExpression, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weekends that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Weekends(string cronExpression, CronFormat cronFormat) => Weekends(cronExpression, cronFormat, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weekends that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Weekends(string cronExpression, CronFormat cronFormat, TimeZoneInfo timeZone)
        {
            return Weekends(TimeZoneInstants.FromCron(cronExpression, cronFormat, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weekends that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Weekends(ITimeline instantsToContain)
        {
            return Weekends().Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with weekends that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Weekends(ITimeline instantsToContain, TimeZoneInfo timeZone)
        {
            return Weekends(timeZone).Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with all workdays in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Workdays() => Workdays(TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with all workdays in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Workdays(TimeZoneInfo timeZone)
        {
            return Days(new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }, timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with workdays that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Workdays(string cronExpression) => Workdays(cronExpression, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with workdays that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Workdays(string cronExpression, TimeZoneInfo timeZone)
        {
            return Workdays(TimeZoneInstants.FromCron(cronExpression, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with workdays that contain instants resolved from cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static IPeriodTimeline Workdays(string cronExpression, CronFormat cronFormat) => Workdays(cronExpression, cronFormat, TimeZoneInfo.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with workdays that contain instants resolved from cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Workdays(string cronExpression, CronFormat cronFormat, TimeZoneInfo timeZone)
        {
            return Workdays(TimeZoneInstants.FromCron(cronExpression, cronFormat, timeZone), timeZone);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with workdays that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Workdays(ITimeline instantsToContain)
        {
            return Weekends().Containing(instantsToContain);
        }

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with workdays that contain instants in <paramref name="instantsToContain"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static IPeriodTimeline Workdays(ITimeline instantsToContain, TimeZoneInfo timeZone)
        {
            return Weekends(timeZone).Containing(instantsToContain);
        }

        /// <summary>
        /// Returns today as a <see cref="Period"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static Period Today() => DayByUtc(DateTime.UtcNow, TimeZoneInfo.Local);

        /// <summary>
        /// Returns today as a <see cref="Period"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static Period Today(TimeZoneInfo timeZone) => DayByUtc(DateTime.UtcNow, timeZone);

        /// <summary>
        /// Returns the requested day as a <see cref="Period"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static Period Day(int day, int month, int year) => Day(day, month, year, TimeZoneInfo.Local);

        /// <summary>
        /// Returns the requested day as a <see cref="Period"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static Period Day(int day, int month, int year, TimeZoneInfo timeZone) => Day(new DateTime(year, month, day, 12, 0, 0), TimeZoneInfo.Local);

        /// <summary>
        /// Samples the day in <see cref="TimeZoneInfo.Local"/> on <paramref name="localDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period Day(DateTime localDateTime) => Day(localDateTime, TimeZoneInfo.Local);

        /// <summary>
        /// Samples the day in provided timezone <paramref name="timeZone"/> on <paramref name="timeZoneDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period Day(DateTime timeZoneDateTime, TimeZoneInfo timeZone)
        {
            return Days(timeZone).SampleAt(TimeZoneInfo.ConvertTimeToUtc(timeZoneDateTime.Date, timeZone)).Period ??
                throw new InvalidOperationException($"No day was found for local date {timeZoneDateTime} in timezone {timeZone}");
        }

        /// <summary>
        /// Samples the day in <see cref="TimeZoneInfo.Local"/> on <paramref name="utcDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period DayByUtc(DateTime utcDateTime) => DayByUtc(utcDateTime, TimeZoneInfo.Local);

        /// <summary>
        /// Samples the day in provided timezone <paramref name="timeZone"/> on <paramref name="utcDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period DayByUtc(DateTime utcDateTime, TimeZoneInfo timeZone)
        {
            return Days(timeZone).SampleAt(utcDateTime).Period ??
                   throw new InvalidOperationException($"No day was found for utc date {utcDateTime} in timezone {timeZone}");
        }

        /// <summary>
        /// Returns the current week as a <see cref="Period"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static Period CurrentWeek() => WeekByUtc(DateTime.UtcNow, TimeZoneInfo.Local);

        /// <summary>
        /// Returns the current week as a <see cref="Period"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static Period CurrentWeek(TimeZoneInfo timeZone) => WeekByUtc(DateTime.UtcNow, timeZone);

        /// <summary>
        /// Samples the week in <see cref="TimeZoneInfo.Local"/> on <paramref name="localDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period Week(DateTime localDateTime) => Week(localDateTime, TimeZoneInfo.Local);

        /// <summary>
        /// Samples the week in provided timezone <paramref name="timeZone"/> on <paramref name="timeZoneDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period Week(DateTime timeZoneDateTime, TimeZoneInfo timeZone)
        {
            return Weeks(timeZone).SampleAt(TimeZoneInfo.ConvertTimeToUtc(timeZoneDateTime.Date, timeZone)).Period ??
                   throw new InvalidOperationException(
                       $"No week was found for local date {timeZoneDateTime} in timezone {timeZone}");
        }

        /// <summary>
        /// Samples the week in <see cref="TimeZoneInfo.Local"/> on <paramref name="utcDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period WeekByUtc(DateTime utcDateTime) => WeekByUtc(utcDateTime, TimeZoneInfo.Local);

        /// <summary>
        /// Samples the week in provided timezone <paramref name="timeZone"/> on <paramref name="utcDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period WeekByUtc(DateTime utcDateTime, TimeZoneInfo timeZone)
        {
            return Weeks(timeZone).SampleAt(utcDateTime.Date).Period ??
                   throw new InvalidOperationException(
                       $"No week was found for UTC date {utcDateTime} in timezone {timeZone}");
        }

        /// <summary>
        /// Returns the current month as a <see cref="Period"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static Period CurrentMonth() => MonthByUtc(DateTime.UtcNow, TimeZoneInfo.Local);

        /// <summary>
        /// Returns the current month as a <see cref="Period"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static Period CurrentMonth(TimeZoneInfo timeZone) => MonthByUtc(DateTime.UtcNow, timeZone);

        /// <summary>
        /// Returns the requested month as a <see cref="Period"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static Period Month(int month, int year) => Month(month, year, TimeZoneInfo.Local);

        /// <summary>
        /// Returns the requested month as a <see cref="Period"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static Period Month(int month, int year, TimeZoneInfo timeZone) => Month(new DateTime(year, month, 15), TimeZoneInfo.Local);

        /// <summary>
        /// Samples the month in <see cref="TimeZoneInfo.Local"/> on <paramref name="localDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period Month(DateTime localDateTime) => Month(localDateTime, TimeZoneInfo.Local);

        /// <summary>
        /// Samples the month in provided timezone <paramref name="timeZone"/> on <paramref name="timeZoneDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period Month(DateTime timeZoneDateTime, TimeZoneInfo timeZone)
        {
            return Months(timeZone).SampleAt(TimeZoneInfo.ConvertTimeToUtc(timeZoneDateTime.Date, timeZone)).Period ??
                   throw new InvalidOperationException(
                       $"No month was found for local date {timeZoneDateTime} in timezone {timeZone}");
        }

        /// <summary>
        /// Samples the month in <see cref="TimeZoneInfo.Local"/> on <paramref name="utcDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period MonthByUtc(DateTime utcDateTime) => Month(utcDateTime, TimeZoneInfo.Local);

        /// <summary>
        /// Samples the month in provided timezone <paramref name="timeZone"/> on <paramref name="utcDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period MonthByUtc(DateTime utcDateTime, TimeZoneInfo timeZone)
        {
            return Months(timeZone).SampleAt(utcDateTime.Date).Period ??
                   throw new InvalidOperationException(
                       $"No month was found for UTC date {utcDateTime} in timezone {timeZone}");
        }

        /// <summary>
        /// Returns the current year as a <see cref="Period"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static Period CurrentYear() => YearByUtc(DateTime.UtcNow, TimeZoneInfo.Local);

        /// <summary>
        /// Returns the current year as a <see cref="Period"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static Period CurrentYear(TimeZoneInfo timeZone) => YearByUtc(DateTime.UtcNow, timeZone);

        /// <summary>
        /// Returns the requested year as a <see cref="Period"/> in <see cref="TimeZoneInfo.Local"/>.
        /// </summary>
        public static Period Year(int year) => Year(year, TimeZoneInfo.Local);

        /// <summary>
        /// Returns the requested year as a <see cref="Period"/> in provided timezone <paramref name="timeZone"/>.
        /// </summary>
        public static Period Year(int year, TimeZoneInfo timeZone) => Year(new DateTime(year, 6, 1), TimeZoneInfo.Local);

        /// <summary>
        /// Samples the year in <see cref="TimeZoneInfo.Local"/> on <paramref name="localDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period Year(DateTime localDateTime) => Year(localDateTime, TimeZoneInfo.Local);

        /// <summary>
        /// Samples the year in provided timezone <paramref name="timeZone"/> on <paramref name="timeZoneDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period Year(DateTime timeZoneDateTime, TimeZoneInfo timeZone)
        {
            return Years(timeZone).SampleAt(TimeZoneInfo.ConvertTimeToUtc(timeZoneDateTime.Date, timeZone)).Period ??
                   throw new InvalidOperationException(
                       $"No year was found for local date {timeZoneDateTime} in timezone {timeZone}");
        }

        /// <summary>
        /// Samples the year in <see cref="TimeZoneInfo.Local"/> on <paramref name="utcDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period YearByUtc(DateTime utcDateTime) => Year(utcDateTime, TimeZoneInfo.Local);

        /// <summary>
        /// Samples the year in provided timezone <paramref name="timeZone"/> on <paramref name="utcDateTime"/> and returns as a <see cref="Period"/> .
        /// </summary>
        public static Period YearByUtc(DateTime utcDateTime, TimeZoneInfo timeZone)
        {
            return Years(timeZone).SampleAt(utcDateTime.Date).Period ??
                   throw new InvalidOperationException(
                       $"No year was found for local date {utcDateTime} in timezone {timeZone}");
        }
    }
}
