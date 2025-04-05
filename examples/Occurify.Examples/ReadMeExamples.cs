using System.Reactive.Concurrency;
using Occurify.Astro;
using Occurify.Extensions;
using Occurify.Reactive.Extensions;
using Occurify.TimeZones;
using Occurify.TimeZones.Extensions;

namespace Occurify.Examples;

[TestClass]
public class ReadMeExamples
{
    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        Coordinates.Local = new Coordinates(48.8584, 2.2945, 330); // Top point of the Eiffel Tower
    }

    [TestMethod]
    public void Usage()
    {
        ITimeline sunsets = AstroInstants.LocalSunset;

        ITimeline twentyMinAfterSunset = sunsets + TimeSpan.FromMinutes(20);

        ITimeline elevenPm = TimeZoneInstants.DailyAt(hour: 23);
        IPeriodTimeline lightOnPeriods = twentyMinAfterSunset.To(elevenPm);

        lightOnPeriods = lightOnPeriods.Within(TimeZonePeriods.Workdays());
        lightOnPeriods = lightOnPeriods.Randomize(TimeSpan.FromMinutes(10));

        if (lightOnPeriods.IsNow())
        {
            Console.WriteLine("Turning the lights on!"); // Turn lights on.
        }
        else
        {
            Console.WriteLine("Turning the lights off!"); // Turn lights off.
        }

        Console.WriteLine("The rest of the current month the lights will go on at:");
        foreach (Period period in lightOnPeriods.EnumerateRange(DateTime.UtcNow, TimeZonePeriods.CurrentMonth().End!.Value))
        {
            Console.WriteLine(period.Start);
        }

        Console.WriteLine("In February 2050 the lights will go on at:");
        foreach (Period period in lightOnPeriods.EnumeratePeriod(TimeZonePeriods.Month(2, 2050)))
        {
            Console.WriteLine(period.Start);
        }

        var scheduler = Scheduler.Default;
        lightOnPeriods.SubscribeStartEnd(
            () => Console.WriteLine("Turning the lights on!"), 
            () => Console.WriteLine("Turning the lights off!"), scheduler);

        // Note: This demo now exits, to demonstrate the scheduling aspect, keep the thread alive for a while.
    }

    [TestMethod]
    public void MorningLight()
    {
        ITimeline fifteenMinAfterSunRise = AstroInstants.LocalSunrise + TimeSpan.FromMinutes(15);
        ITimeline sevenAm = TimeZoneInstants.DailyAt(hour: 7);
        IPeriodTimeline between7AndSunRise = sevenAm.To(fifteenMinAfterSunRise); // Creates timeline that represents periods starting at 7am and ending 15 minutes after sunrise.

        IPeriodTimeline between7AndSunRiseInTheMorning = between7AndSunRise.Within(TimeZonePeriods.Days());
        //Alternative: IPeriodTimeline between7AndSunRiseInTheMorning = TimeZonePeriods.DailyBetween(fifteenMinAfterSunRise, sevenAm);

        var scheduler = Scheduler.Default;
        between7AndSunRiseInTheMorning.SubscribeStartEnd(
            () => Console.WriteLine("Turning the lights on!"),
            () => Console.WriteLine("Turning the lights off!"), scheduler);

        // Note: This demo now exits, to demonstrate the scheduling aspect, keep the thread alive for a while.
    }

    [TestMethod]
    public void UseCronsToCreatePeriods()
    {
        string[] holidayCrons = [
            "0 0 0 1 1 ?", // New Year Day
            "0 0 0 ? 5 MON#4", //Memorial Day
            "0 0 0 4 7 ?", //Independence Day
            "0 0 0 ? 9 MON#1", //Labor Day
            "0 0 0 ? 11 THU#4", //Thanksgiving
            "0 0 0 25 12 ?" //Christmas
        ];

        IPeriodTimeline holidays = TimeZonePeriods.Days(holidayCrons.Select(TimeZoneInstants.FromCron).Combine());
        IPeriodTimeline workingDays = TimeZonePeriods.Workdays();
        IPeriodTimeline workingDaysWithoutHolidays = workingDays - holidays;

        Console.WriteLine("Work days this year:");
        foreach (Period period in workingDaysWithoutHolidays.EnumeratePeriod(TimeZonePeriods.CurrentYear()))
        {
            Console.WriteLine(period.ToString(TimeZoneInfo.Local));
        }
    }

    [TestMethod]
    public void FiscalYear()
    {
        IPeriodTimeline calendarYears = TimeZonePeriods.Years();
        IPeriodTimeline fiscalYears = TimeZoneInstants.StartOfMonths(10).AsConsecutivePeriodTimeline();

        Period currentCalendarYear = calendarYears.SampleAt(DateTime.UtcNow).Period!;
        Period currentFiscalYear = fiscalYears.SampleAt(DateTime.UtcNow).Period!;

        IPeriodTimeline workdays = TimeZonePeriods.Workdays();
        int amountOfCalendarDaysWorked = workdays.EnumerateRange(currentCalendarYear.Start!.Value, DateTime.UtcNow).Count();
        int amountOfFiscalYearDaysWorked = workdays.EnumerateRange(currentFiscalYear.Start!.Value, DateTime.UtcNow).Count();

        Console.WriteLine($"Currently we have worked {amountOfCalendarDaysWorked} days this calendar year!");
        Console.WriteLine($"Currently we have worked {amountOfFiscalYearDaysWorked} days this fiscal year!");
    }

    [TestMethod]
    public void SearchingDates()
    {
        IPeriodTimeline fridaysOfFebruary = TimeZonePeriods.Months(2, TimeZoneInfo.Utc) & TimeZonePeriods.Days(DayOfWeek.Friday, TimeZoneInfo.Utc);
        Period twoYears = TimeZoneInstants.StartOfYear(1200, TimeZoneInfo.Utc).To(TimeZoneInstants.EndOfYear(1201, TimeZoneInfo.Utc));

        Console.WriteLine("The years 1200 and 1201 have the following fridays in february:");
        foreach (var date in fridaysOfFebruary.EnumeratePeriod(twoYears))
        {
            Console.WriteLine(date.Start!.Value.ToShortDateString());
        }
    }

    [TestMethod]
    public void FindingAvailableReservations()
    {
        var reservationsIn2025 = CreateRandomAppointments(TimeZonePeriods.Year(2025));
        var availableFreePeriodsInAugust =
            FindAvailableFreePeriods(TimeZonePeriods.Month(8, 2025), reservationsIn2025, TimeSpan.FromMinutes(90));

        Console.WriteLine("Available free 90 min timeslots in august:");
        foreach (var period in availableFreePeriodsInAugust)
        {
            Console.WriteLine(period.ToString(TimeZoneInfo.Local));
        }

        Period[] FindAvailableFreePeriods(Period searchRange, Period[] reservations, TimeSpan minimumDuration)
        {
            return reservations
                .Invert() // Identify free periods by inverting the reserved ones
                .WherePeriods(p =>
                    p.Duration >= minimumDuration) // Filter periods that meet the minimum duration requirement
                .EnumeratePeriod(searchRange) // Restrict results to the given search range
                .ToArray();
        }
    }

    [TestMethod]
    public void SolarPhases()
    {
        Coordinates arcticCoordinates = new Coordinates(80.45302, 54.77918, Height: 37);
        ITimeline sunsetsAndRises = AstroInstants.SunPhases(arcticCoordinates, SunPhases.Sunrise | SunPhases.Sunset);
        IPeriodTimeline daysOfCurrentYear = TimeZonePeriods.Days().Within(TimeZonePeriods.CurrentYear());
        IPeriodTimeline daysWithoutSunsetsOrRises = daysOfCurrentYear - daysOfCurrentYear.Containing(sunsetsAndRises);

        Console.WriteLine($"This year on the arctic the sun doesn't rise or set on {daysWithoutSunsetsOrRises.Count()} days.");
    }

    [TestMethod]
    public void ComplicatedRequirements()
    {
        int seed = 1337;

        // Determine start instants
        ITimeline tenMinAfterSunset = AstroInstants.LocalSunset + TimeSpan.FromMinutes(10);
        ITimeline randomizedSunset = tenMinAfterSunset.Randomize(seed, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(10));
        // Ensure the light doesn't turn on before 5:15 PM
        ITimeline after515Pm = (randomizedSunset + TimeZoneInstants.DailyAt(hour: 17, minute: 15)).LastWithin(TimeZonePeriods.Days());
        // Ensure the light doesn't turn on after 8 PM
        ITimeline turnOnAt = (after515Pm + TimeZoneInstants.DailyAt(hour: 20)).FirstWithin(TimeZonePeriods.Days());

        // Determine end instants
        ITimeline turnOffAt = TimeZoneInstants.DailyAt(hour: 23, minute: 15).Randomize(seed, TimeSpan.FromMinutes(20));

        // Create period
        IPeriodTimeline lightOnPeriods = turnOnAt.To(turnOffAt);

        // Schedule
        var scheduler = Scheduler.Default;
        lightOnPeriods.SubscribeStartEnd(
            () => Console.WriteLine("Turning the lights on!"),
            () => Console.WriteLine("Turning the lights off!"), scheduler);

        // Note: This demo now exits, to demonstrate the scheduling aspect, keep the thread alive for a while.
    }

    [TestMethod]
    public void MultiplePeriods()
    {
        IPeriodTimeline workingHours = TimeZonePeriods.Between(startHour: 8, endHour: 18) - TimeZonePeriods.Weekends();

        List<Period[]> employeeAppointments = Enumerable.Range(0, 5).Select(_ => CreateRandomAppointments(TimeZonePeriods.Year(2025))).ToList();
        IPeriodTimeline[] appointmentTimelines = employeeAppointments.Select(p => p.AsPeriodTimeline()).ToArray();
        IPeriodTimeline[] invertedTimelines = appointmentTimelines.Select(tl => tl.Invert()).ToArray();

        IPeriodTimeline availableSlotsTimelines = invertedTimelines.IntersectPeriods() & workingHours;

        Period august = TimeZonePeriods.Month(8, 2025);
        IEnumerable<Period> periodsEveryoneIsAvailable = availableSlotsTimelines
            .EnumeratePeriod(august).Where(p => p.Duration > TimeSpan.FromHours(1));

        Console.WriteLine("Everyone is available on:");
        foreach (Period period in periodsEveryoneIsAvailable)
        {
            Console.WriteLine(period.ToString(TimeZoneInfo.Local));
        }

        DateTime timeOfInterest = new DateTime(2025, 3, 7).AsUtcInstant();
        PeriodTimelineSample[] samples = appointmentTimelines.Select(tl => tl.SampleAt(timeOfInterest)).ToArray();

        int appointmentPeriods = samples.Count(s => s.IsPeriod);
        int freeTimePeriods = samples.Count(s => s.IsGap);

        Console.WriteLine($"{appointmentPeriods} people had an appointment on {timeOfInterest}.");
        Console.WriteLine($"{freeTimePeriods} people had were free on {timeOfInterest}.");
    }

    [TestMethod]
    public void PeriodCreation()
    {
        DateTime utcNow = DateTime.UtcNow;

        // Using extension methods
        Period nowToOneHoursFromNow = utcNow.ToPeriodWithDuration(TimeSpan.FromHours(1));
        Period nowToTwoHoursFromNow = utcNow.To(utcNow + TimeSpan.FromHours(2));
        Period nowToNeverEnding = utcNow.To(null);

        // Using static construction
        Period nowToThreeHoursFromNow = Period.Create(utcNow, utcNow + TimeSpan.FromHours(3));
    }

    [TestMethod]
    public void TimelineCreation()
    {
        DateTime utcNow = DateTime.UtcNow;

        // Using extension methods
        ITimeline timeline1 = utcNow.AsTimeline();
        ITimeline timeline2 = new[] { utcNow, utcNow + TimeSpan.FromHours(1) }.AsTimeline();

        // Using static methods
        ITimeline timeline3 = Timeline.FromInstants(utcNow, utcNow + TimeSpan.FromHours(1), utcNow, utcNow + TimeSpan.FromHours(3));
        ITimeline timeline4 = Timeline.Periodic(TimeSpan.FromHours(1));
    }

    [TestMethod]
    public void PeriodTimelineCreation()
    {
        DateTime utcNow = DateTime.UtcNow;
        Period period = utcNow.To(utcNow + TimeSpan.FromHours(2));

        // Using extension methods on periods
        IPeriodTimeline periodTimeline1 = period.AsPeriodTimeline();
        IPeriodTimeline periodTimeline2 = new[] { period, period + TimeSpan.FromHours(2) }.AsPeriodTimeline();

        // Using extension methods on instant timelines
        ITimeline periodStartTimeline = Timeline.Periodic(TimeSpan.FromHours(1));
        ITimeline periodEndTimeline = periodStartTimeline.OffsetMinutes(10);
        IPeriodTimeline periodTimeline3 = periodStartTimeline.To(periodEndTimeline);

        // Using static methods
        IPeriodTimeline periodTimeline4 = PeriodTimeline.FromPeriods(period, period + TimeSpan.FromHours(2), period + TimeSpan.FromHours(4));
        IPeriodTimeline periodTimeline5 = PeriodTimeline.Between(periodStartTimeline, periodEndTimeline);
    }

    /// <summary>
    /// Will generate some random appointments for a given period.
    /// Appointments can go from 6 AM to 9 PM.
    /// </summary>
    /// <param name="period"></param>
    /// <returns></returns>
    private Period[] CreateRandomAppointments(Period period)
    {
        var periods = new List<Period>();
        var random = new Random();
        var latestAppointment = new TimeOnly(21, 0);

        for (var dayOfYear = period.Start!.Value; dayOfYear < period.End; dayOfYear = dayOfYear.AddDays(1))
        {
            // Note1: We start a little early, as the demo demonstrates filtering on working time.
            // Note2: even though we don't convert this to UTC, this works as we add the time of day to the date which is in UTC (it's used relatively).
            var timeOfDay = new TimeOnly(6, 0);
            do
            {
                timeOfDay = timeOfDay.Add(TimeSpan.FromMinutes(15 * random.Next(0, 9)));
                if (timeOfDay > latestAppointment)
                {
                    break;
                }

                if (random.NextDouble() < 0.8)
                {
                    continue; // 80% chance to skip this appointment
                }

                // Random duration between 15 min and 2 hours
                var duration = TimeSpan.FromMinutes(15 * random.Next(1, 9));

                if (timeOfDay.Add(duration) > latestAppointment)
                {
                    break;
                }
                // Create the period and add it to the list
                periods.Add(Period.Create(dayOfYear + timeOfDay.ToTimeSpan(), duration));

                timeOfDay = timeOfDay.Add(duration);
            }
            while (timeOfDay < latestAppointment);
        }

        return periods.ToArray();
    }
}