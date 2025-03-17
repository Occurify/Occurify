using Cronos;
using Occurify.Extensions;
using Occurify.TimeZones.Extensions;

namespace Occurify.TimeZones;

public static class TimeZoneInstants
{
    /// <summary>
    /// Returns a <see cref="ITimeline"/> with instants defined by cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
    /// It's supported expressions consisting of 5 or 6 fields:
    /// second (optional), minute, hour, day of month, month, day of week.
    /// </summary>
    public static ITimeline FromCron(string cronExpression)
    {
        return new CronTimeline(cronExpression, TimeZoneInfo.Local);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with instants defined by cron expression <paramref name="cronExpression"/> in <see cref="TimeZoneInfo.Local"/>.
    /// It's supported expressions consisting of 5 or 6 fields:
    /// second (optional), minute, hour, day of month, month, day of week.
    /// </summary>
    public static ITimeline FromCron(string cronExpression, CronFormat cronFormat)
    {
        return new CronTimeline(cronExpression, cronFormat, TimeZoneInfo.Local);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with instants defined by cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
    /// It's supported expressions consisting of 5 or 6 fields:
    /// second (optional), minute, hour, day of month, month, day of week.
    /// </summary>
    public static ITimeline FromCron(string cronExpression, TimeZoneInfo timeZone)
    {
        return new CronTimeline(cronExpression, timeZone);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with instants defined by cron expression <paramref name="cronExpression"/> in provided timezone <paramref name="timeZone"/>.
    /// It's supported expressions consisting of 5 or 6 fields:
    /// second (optional), minute, hour, day of month, month, day of week.
    /// </summary>
    public static ITimeline FromCron(string cronExpression, CronFormat cronFormat, TimeZoneInfo timeZone)
    {
        return new CronTimeline(cronExpression, cronFormat, timeZone);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at <paramref name="timeOfDay"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline DailyAt(TimeOnly timeOfDay) => DailyAt(timeOfDay, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at <paramref name="hour"/> o'clock in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline DailyAt(int hour) => DailyAt(new TimeOnly(hour, 0));

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at the provided time in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline DailyAt(int hour, int minute) => DailyAt(new TimeOnly(hour, minute));

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at the provided time in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline DailyAt(int hour, int minute, int second) => DailyAt(new TimeOnly(hour, minute, second));

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at the provided time in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline DailyAt(int hour, int minute, int second, int millisecond) => DailyAt(new TimeOnly(hour, minute, second, millisecond));

#if NET7_0 || NET8_0 || NET9_0
    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at the provided time in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline DailyAt(int hour, int minute, int second, int millisecond, int microsecond) => DailyAt(new TimeOnly(hour, minute, second, millisecond, microsecond));
#endif

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at <paramref name="timeOfDay"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline DailyAt(TimeOnly timeOfDay, TimeZoneInfo timeZone)
    {
#if NET7_0 || NET8_0 || NET9_0
        if (timeOfDay.Millisecond != 0 || timeOfDay.Microsecond != 0 || timeOfDay.Nanosecond != 0)
        {
            return FromCron(new TimeOnly(timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second).ToCronExpression())
                .Offset(
                    TimeSpan.FromMilliseconds(timeOfDay.Millisecond) +
                    TimeSpan.FromMicroseconds(timeOfDay.Microsecond) +
                    TimeSpan.FromTicks(timeOfDay.Nanosecond / 100))
                .Within(TimeZonePeriods.Days(timeZone));
        }
#else
        if (timeOfDay.Millisecond != 0)
        {
            return FromCron(new TimeOnly(timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second).ToCronExpression())
                .Offset(
                    TimeSpan.FromMilliseconds(timeOfDay.Millisecond))
                .Within(TimeZonePeriods.Days(timeZone));
        }
#endif

        return new CronTimeline(timeOfDay.ToCronExpression(), timeZone)
            .Within(TimeZonePeriods.Days(timeZone));
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at <paramref name="hour"/> o'clock in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline DailyAt(int hour, TimeZoneInfo timeZone) => DailyAt(new TimeOnly(hour, 0), timeZone);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at the provided time in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline DailyAt(int hour, int minute, TimeZoneInfo timeZone) => DailyAt(new TimeOnly(hour, minute), timeZone);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at the provided time in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline DailyAt(int hour, int minute, int second, TimeZoneInfo timeZone) => DailyAt(new TimeOnly(hour, minute, second), timeZone);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at the provided time in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline DailyAt(int hour, int minute, int second, int millisecond, TimeZoneInfo timeZone) => DailyAt(new TimeOnly(hour, minute, second, millisecond), timeZone);

#if NET7_0 || NET8_0 || NET9_0
    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every day at the provided time in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline DailyAt(int hour, int minute, int second, int millisecond, int microsecond, TimeZoneInfo timeZone) => DailyAt(new TimeOnly(hour, minute, second, millisecond, microsecond), timeZone);
#endif

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every second.
    /// </summary>
    public static ITimeline EverySecond()
    {
        return FromCron("* * * * * *");
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with one instant every minute.
    /// </summary>
    public static ITimeline EveryMinute()
    {
        return FromCron("* * * * *");
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every hour in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline Hourly() => Hourly(TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every hour in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline Hourly(TimeZoneInfo timeZone)
    {
        return FromCron("0 * * * *", timeZone);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every day in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline Daily() => Daily(TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every day in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline Daily(TimeZoneInfo timeZone)
    {
        return FromCron("0 0 * * *", timeZone);
    }

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the requested day in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime StartOfDay(int day, int month, int year) => StartOfDay(day, month, year, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the requested day in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime StartOfDay(int day, int month, int year, TimeZoneInfo timeZone) => StartOfDay(new DateTime(year, month, day, 12, 0, 0), TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the day at <paramref name="localDateTime"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime StartOfDay(DateTime localDateTime) => StartOfDay(localDateTime, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the day at <paramref name="localDateTime"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime StartOfDay(DateTime localDateTime, TimeZoneInfo timeZone) =>
        Daily(timeZone).GetPreviousUtcInstant(TimeZoneInfo.ConvertTimeToUtc(localDateTime.Date, timeZone)) ??
        throw new InvalidOperationException($"No day was found for local date {localDateTime} in timezone {timeZone}");

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of <paramref name="dayOfWeek"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline StartOfDays(DayOfWeek dayOfWeek) =>
        StartOfDays(new[] { dayOfWeek }, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of <paramref name="dayOfWeek"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline StartOfDays(DayOfWeek dayOfWeek, TimeZoneInfo timeZone) =>
        StartOfDays(new[] { dayOfWeek }, timeZone);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every day specified in <paramref name="daysOfWeek"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline StartOfDays(IEnumerable<DayOfWeek> daysOfWeek) =>
        StartOfDays(daysOfWeek, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every day specified in <paramref name="daysOfWeek"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline StartOfDays(IEnumerable<DayOfWeek> daysOfWeek, TimeZoneInfo timeZone)
    {
        return FromCron($"0 0 * * {string.Join(',', daysOfWeek.Select(d => (int)d))}", timeZone);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every day specified in <paramref name="daysOfWeek"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline StartOfDays(params DayOfWeek[] daysOfWeek) =>
        StartOfDays(daysOfWeek, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the requested day in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime EndOfDay(int day, int month, int year) => EndOfDay(day, month, year, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the requested day in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime EndOfDay(int day, int month, int year, TimeZoneInfo timeZone) => EndOfDay(new DateTime(year, month, day, 12, 0, 0), TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the day at <paramref name="localDateTime"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime EndOfDay(DateTime localDateTime) => EndOfDay(localDateTime, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the day at <paramref name="localDateTime"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime EndOfDay(DateTime localDateTime, TimeZoneInfo timeZone) =>
        Daily(timeZone).GetNextUtcInstant(TimeZoneInfo.ConvertTimeToUtc(localDateTime.Date, timeZone)) ??
        throw new InvalidOperationException($"No day was found for local date {localDateTime} in timezone {timeZone}");

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the end of <paramref name="dayOfWeek"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline EndOfDays(DayOfWeek dayOfWeek) =>
        EndOfDays(new[] { dayOfWeek }, TimeZoneInfo.Local);


    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the end of <paramref name="dayOfWeek"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline EndOfDays(DayOfWeek dayOfWeek, TimeZoneInfo timeZone) =>
        EndOfDays(new[] { dayOfWeek }, timeZone);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the end of every day specified in <paramref name="daysOfWeek"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline EndOfDays(IEnumerable<DayOfWeek> daysOfWeek) =>
        EndOfDays(daysOfWeek, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the end of every day specified in <paramref name="daysOfWeek"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline EndOfDays(IEnumerable<DayOfWeek> daysOfWeek, TimeZoneInfo timeZone) =>
        StartOfDays(daysOfWeek.Select(d => d.NextDay()), timeZone);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the end of every day specified in <paramref name="daysOfWeek"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline EndOfDays(params DayOfWeek[] daysOfWeek) =>
        EndOfDays(daysOfWeek, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every week in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline Weekly() => Weekly(TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every week in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline Weekly(TimeZoneInfo timeZone) => StartOfDays(DayOfWeek.Monday, timeZone);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the week at <paramref name="localDateTime"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime StartOfWeek(DateTime localDateTime) => StartOfWeek(localDateTime, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the week at <paramref name="localDateTime"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime StartOfWeek(DateTime localDateTime, TimeZoneInfo timeZone) =>
        Weekly(timeZone).GetPreviousUtcInstant(TimeZoneInfo.ConvertTimeToUtc(localDateTime.Date, timeZone)) ??
        throw new InvalidOperationException($"No week was found for local date {localDateTime} in timezone {timeZone}");

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the week at <paramref name="localDateTime"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime EndOfWeek(DateTime localDateTime) => EndOfWeek(localDateTime, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the week at <paramref name="localDateTime"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime EndOfWeek(DateTime localDateTime, TimeZoneInfo timeZone) =>
        Weekly(timeZone).GetNextUtcInstant(TimeZoneInfo.ConvertTimeToUtc(localDateTime.Date, timeZone)) ??
        throw new InvalidOperationException($"No week was found for local date {localDateTime} in timezone {timeZone}");

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every month in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline Monthly() => Monthly(TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every month in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline Monthly(TimeZoneInfo timeZone)
    {
        return FromCron("0 0 1 * *", timeZone);
    }

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the requested month in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime StartOfMonth(int month, int year) => StartOfMonth(month, year, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the requested month in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime StartOfMonth(int month, int year, TimeZoneInfo timeZone) => StartOfMonth(new DateTime(year, month, 15), TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the month at <paramref name="localDateTime"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime StartOfMonth(DateTime localDateTime) => StartOfMonth(localDateTime, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the month at <paramref name="localDateTime"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime StartOfMonth(DateTime localDateTime, TimeZoneInfo timeZone) =>
        Monthly(timeZone).GetPreviousUtcInstant(TimeZoneInfo.ConvertTimeToUtc(localDateTime.Date, timeZone)) ??
        throw new InvalidOperationException($"No month was found for local date {localDateTime} in timezone {timeZone}");

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of <paramref name="month"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline StartOfMonths(int month) =>
        StartOfMonths(new[] { month }, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of <paramref name="month"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline StartOfMonths(int month, TimeZoneInfo timeZone) =>
        StartOfMonths(new[] { month }, timeZone);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every month specified in <paramref name="months"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline StartOfMonths(IEnumerable<int> months) =>
        StartOfMonths(months, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every month specified in <paramref name="months"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline StartOfMonths(IEnumerable<int> months, TimeZoneInfo timeZone)
    {
        return FromCron($"0 0 1 {string.Join(',', months)} *", timeZone);
    }

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every month specified in <paramref name="months"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline StartOfMonths(params int[] months) =>
        StartOfMonths(months, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the requested month in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime EndOfMonth(int month, int year) => EndOfMonth(month, year, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the requested month in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime EndOfMonth(int month, int year, TimeZoneInfo timeZone) => EndOfMonth(new DateTime(year, month, 15), TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the month at <paramref name="localDateTime"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime EndOfMonth(DateTime localDateTime) => EndOfMonth(localDateTime, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the month at <paramref name="localDateTime"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime EndOfMonth(DateTime localDateTime, TimeZoneInfo timeZone) =>
        Monthly(timeZone).GetNextUtcInstant(TimeZoneInfo.ConvertTimeToUtc(localDateTime.Date, timeZone)) ??
        throw new InvalidOperationException($"No month was found for local date {localDateTime} in timezone {timeZone}");

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the end of <paramref name="month"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline EndOfMonths(int month) =>
        EndOfMonths(new[] { month }, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the end of <paramref name="month"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline EndOfMonths(int month, TimeZoneInfo timeZone) =>
        EndOfMonths(new[] { month }, timeZone);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the end of every month specified in <paramref name="months"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline EndOfMonths(IEnumerable<int> months) =>
        EndOfMonths(months, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the end of every month specified in <paramref name="months"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline EndOfMonths(IEnumerable<int> months, TimeZoneInfo timeZone) =>
        StartOfMonths(months.Select(d => (d + 1) % 12), timeZone);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the end of every month specified in <paramref name="months"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline EndOfMonths(params int[] months) =>
        EndOfMonths(months, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every year in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static ITimeline Annually() => Annually(TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="ITimeline"/> with an instant at the start of every year in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static ITimeline Annually(TimeZoneInfo timeZone)
    {
        return FromCron("0 0 1 1 *", timeZone);
    }

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the requested year in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime StartOfYear(int year) => StartOfYear(year, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the requested year in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime StartOfYear(int year, TimeZoneInfo timeZone) => StartOfYear(new DateTime(year, 6, 1), TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the year at <paramref name="localDateTime"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime StartOfYear(DateTime localDateTime) => StartOfYear(localDateTime, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the start of the year at <paramref name="localDateTime"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime StartOfYear(DateTime localDateTime, TimeZoneInfo timeZone) =>
        Annually(timeZone).GetPreviousUtcInstant(TimeZoneInfo.ConvertTimeToUtc(localDateTime.Date, timeZone)) ??
        throw new InvalidOperationException($"No year was found for local date {localDateTime} in timezone {timeZone}");

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the requested year in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime EndOfYear(int year) => EndOfYear(year, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the requested year in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime EndOfYear(int year, TimeZoneInfo timeZone) => EndOfYear(new DateTime(year, 6, 1), TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the year at <paramref name="localDateTime"/> in <see cref="TimeZoneInfo.Local"/>.
    /// </summary>
    public static DateTime EndOfYear(DateTime localDateTime) => EndOfYear(localDateTime, TimeZoneInfo.Local);

    /// <summary>
    /// Returns a <see cref="DateTime"/> representing the end of the year at <paramref name="localDateTime"/> in provided timezone <paramref name="timeZone"/>.
    /// </summary>
    public static DateTime EndOfYear(DateTime localDateTime, TimeZoneInfo timeZone) =>
        Annually(timeZone).GetNextUtcInstant(TimeZoneInfo.ConvertTimeToUtc(localDateTime.Date, timeZone)) ??
        throw new InvalidOperationException($"No year was found for local date {localDateTime} in timezone {timeZone}");
}