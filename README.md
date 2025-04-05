# Occurify

[![GitHub license](https://img.shields.io/github/license/Occurify/Occurify?label=License)](https://github.com/Occurify/Occurify?tab=MIT-1-ov-file)
[![GitHub release](https://img.shields.io/github/v/release/Occurify/Occurify?label=Release)](https://github.com/Occurify/Occurify/releases/latest)
[![Build Status](https://github.com/Occurify/Occurify/actions/workflows/ci-build-and-test.yml/badge.svg)](https://github.com/Occurify/Occurify/actions/workflows/ci-build-and-test.yml)

A powerful and intuitive .NET library for defining, filtering, transforming, and scheduling instant and period timelines.

## 📖 Table of Contents  
- [Overview](#overview)
- [Installation](#installation)
- [Usage](#usage)
    - [Defining Timelines](#defining-timelines)
    - [Transforming Timelines](#transforming-timelines)
    - [Combining Timelines Into Periods](#combining-timelines-into-periods)
    - [Filtering & Randomization](#filtering--randomization)
    - [Sampling](#checking-if-the-lights-should-be-on-right-now)
    - [Enumerating](#enumerating-future-or-past-events)
    - [Scheduling](#scheduling-automatic-actions)
- [Potential Use Cases](#potential-use-cases)
    - [Morning Light](#morning-light)
    - [Use Crons to Create Periods](#use-crons-to-create-periods)
    - [Working with Different Periods](#working-with-different-periods)
    - [Searching Dates](#searching-dates)
    - [Finding Available Periods Between Reservations](#finding-available-periods-between-reservations)
    - [Solar Phases](#solar-phases)
    - [Complicated Requirements](#complicated-requirements)
    - [Working with Multiple Periods](#working-with-multiple-periods)
- [Design](#design)
    - [Instant](#instant)
    - [Period](#period)
    - [Instant Timeline](#instant-timeline)
    - [Period Timeline](#period-timeline)
    - [Collections](#collections)
- [Coordinates](#coordinates)
- [ASCII Representation of Timelines](#ascii-representation-of-timelines)
- [Extension Methods](#extension-methods)
- [Unit Tests](#unit-tests)
- [Important Considerations](#important-considerations)
- [Versioning and Stability](#versioning-and-stability)
- [Release Notes](#release-notes)
- [Multi-Language Support](#multi-language-support)
- [License](#license)

## Overview

### [Occurify](https://www.nuget.org/packages/Occurify)

A powerful and intuitive .NET library for defining, filtering, transforming, and scheduling instant and period timelines.

- Supports instants, periods, timelines and period timelines.
- Implements collection and periodic timelines.
- Supports an extensive set of fluent extension methods to filter and transform instants, periods, timelines and period timelines.
- Includes 4500+ unit tests to ensure reliability.

### [Occurify.TimeZones](https://www.nuget.org/packages/Occurify.TimeZones)

Time zone and cron expression support for Occurify: Filter, manipulate, and schedule instants and periods across time zones.

- Supports time zone based instants and periods (e.g. time of day, day, week, etc).
- Supports both forwards and backwards iteration through Cron instants and periods.
- Uses the *Cronos* library to enable Cron functionality that:
- Supports standard Cron format with optional seconds.
- Supports time zones, and performs all the date/time conversions for you.
- Does not skip occurrences, when the clock jumps forward to daylight saving time (known as Summer time).
- Does not skip interval-based occurrences, when the clock jumps backward from Summer time.
- Does not retry non-interval based occurrences, when the clock jumps backward from Summer time.

> Note: Rather than using **Cronos**, Occurify currently uses **Cronos.Unlimited** to remove the year 2499 limitation and ensure optimal functionality. For more information, please check the [README of Cronos.Unlimited](https://github.com/DevJasperNL/Cronos.Unlimited).

### [Occurify.Astro](https://www.nuget.org/packages/Occurify.Astro)

Astronomical instants and periods for Occurify: Track sun states, perform calculations, and manage events.

- Uses the *SunCalcNet* library to enable functionality that:
- Supports location (coordinate) based instants and periods (e.g. dawn, daytime, etc).
- Supports multiple solar phases (sunrise, sunset, end of sunrise, start of sunset, (nautical) dawn, (nautical) dusk, (end of) night, (end of) golden hour, solar noon and nadir).

### [Occurify.Reactive](https://www.nuget.org/packages/Occurify.Reactive)

Reactive Extensions for Occurify: Enabling seamless scheduling of instant and period-based timelines.

- Uses ReactiveX to enable scheduling for both timelines and periods.

## Installation

Occurify is distributed as the following NuGet packages:

Package | Description
--- |---
[Occurify](https://www.nuget.org/packages/Occurify) | A powerful and intuitive .NET library for defining, filtering, transforming, and scheduling instant and period timelines.
[Occurify.TimeZones](https://www.nuget.org/packages/Occurify.TimeZones) | Time zone and cron expression support for Occurify: Filter, manipulate, and schedule instants and periods across time zones.
[Occurify.Astro](https://www.nuget.org/packages/Occurify.Astro) | Astronomical instants and periods for Occurify: Track sun states, perform calculations, and manage events.
[Occurify.Reactive](https://www.nuget.org/packages/Occurify.Reactive) | Reactive Extensions for Occurify: Enabling seamless scheduling of instant and period-based timelines.

To install the core Occurify package, use the NuGet Package Manager Console:

```powershell
PM> Install-Package Occurify
```

Alternatively, you can install it using the .NET CLI:

```powershell
dotnet add package Occurify
```

For other packages, replace `Occurify` with the desired package name.

## Usage

Instead of dealing with fixed timestamps, Occurify lets you think about time in a more **human-friendly** way. You don't need to precompute every possible event—just define the concept of an event, like *"all sunsets,"* and let Occurify handle the rest.

As an example, let’s imagine we want to automate our lights to turn on in the evening.

### Defining Timelines

Instead of manually maintaining a list of sunset times, we can simply use:

```cs
ITimeline sunsets = AstroInstants.LocalSunset;
```

This timeline now represents every sunset dynamically—no need for hardcoded schedules.

### Transforming Timelines

Want to schedule events **20 minutes after sunset**? Just shift the timeline:

```cs
ITimeline twentyMinAfterSunset = sunsets + TimeSpan.FromMinutes(20);
```

Now, `twentyMinAfterSunset` dynamically represents every sunset, plus 20 minutes—no manual calculations needed.

### Combining Timelines Into Periods

Now, let’s define a time when the lights should **turn off** and create a period from **20 minutes after sunset** until **11 PM**:

```cs
ITimeline elevenPm = TimeZoneInstants.DailyAt(hour: 23);
IPeriodTimeline lightOnPeriods = twentyMinAfterSunset.To(elevenPm);
```

With this, lightOnPeriods now represents **all the evening periods** when the lights should be on.

### Filtering & Randomization

If you want the lights to turn on **only on weekdays**, you can filter the periods like this:

```cs
lightOnPeriods = lightOnPeriods.Within(TimeZonePeriods.Workdays());
```

To make the timing feel more natural, we can **randomize** the periods slightly by adding a variation of up to 10 minutes:

```cs
lightOnPeriods = lightOnPeriods.Randomize(TimeSpan.FromMinutes(10));
```

### Using the Timeline

#### Checking if the Lights Should Be On Right Now

To check if the lights should be on at the current moment, you can simply use `IsNow()` on the `lightOnPeriods` timeline:

```cs
if (lightOnPeriods.IsNow()) {
    // Turn lights on.
}
else {
    // Turn lights off.
}
```

#### Enumerating Future (or Past) Events

You can easily enumerate future or past periods to check when the lights will go on. For example, let’s find out when the lights will turn on during the **rest of the current month**:

```cs
Console.WriteLine("The rest of the current month the lights will go on at:");
foreach (Period period in lightOnPeriods.EnumerateRange(DateTime.UtcNow, TimeZonePeriods.CurrentMonth().End!.Value)){
    Console.WriteLine(period.Start);
}
```

But due to the dynamic nature of timelines we can just as easily see when the lights will turn on in **February 2050**:

```cs
Console.WriteLine("In February 2050 the lights will go on at:");
foreach (Period period in lightOnPeriods.EnumeratePeriod(TimeZonePeriods.Month(2, 2050))){
    Console.WriteLine(period.Start);
}
```
> Note that the period timeline **only resolves the necessary periods when enumerated**, ensuring efficiency.

#### Scheduling Automatic Actions

To automate actions based on the timeline, you can use **ReactiveX**, which provides a powerful way to handle event-driven programming. The `SubscribeStartEnd` method internally utilizes an `IObservable`, allowing you to schedule events reactively.

By default, this method even evaluates the current state of the timeline, invoking the applicable method on startup.

```cs
lightOnPeriods.SubscribeStartEnd(() => TurnLightsOn(), () => TurnLightsOff(), scheduler);
```

This approach allows you to focus on what matters—like defining when you want your lights to turn on—without manually handling the timing and scheduling. As a result, your code becomes more **intuitive**, **dynamic**, and **use case-driven**.

## Potential Use Cases

This section presents various use cases that demonstrate Occurify’s capabilities and provide a clearer understanding of its functionality.

These examples incorporate additional modules such as `Occurify.TimeZones`, `Occurify.Astro`, and `Occurify.Reactive`.

>**Note: Instead of using `var`, variable types are explicitly defined in the examples for improved clarity.**

### Morning Light

The following example demonstrates how to turn on a light between **7 AM and 15 minutes after sunrise**.

> **Note:** `AstroInstants` is provided by `Occurify.Astro`, while `TimeZoneInstants` comes from `Occurify.TimeZones`.

#### Defining the Period
```cs
ITimeline fifteenMinAfterSunRise = AstroInstants.LocalSunrise + TimeSpan.FromMinutes(15);
ITimeline sevenAm = TimeZoneInstants.DailyAt(hour: 7);
IPeriodTimeline between7AndSunRise = sevenAm.To(fifteenMinAfterSunRise); // Creates timeline that represents periods starting at 7am and ending 15 minutes after sunrise.
```

#### Handling the Edge Case: Sunrise Before 7 AM
A potential issue arises: what if sunrise occurs **before** 7 AM? In this case, our period would incorrectly extend into the previous day.

To ensure our period stays within the intended day, we can apply a filter:
```cs
IPeriodTimeline between7AndSunRiseInTheMorning = between7AndSunRise.Within(TimeZonePeriods.Days());
```
> Alternatively, we could have used the helper method `TimeZonePeriods.DailyBetween`

#### Using ReactiveX for Scheduling

Now we can use `SubscribeStartEnd` from `Occurify.Reactive` to integrate with `ReactiveX` for event-driven scheduling:
```cs
between7AndSunRiseInTheMorning.SubscribeStartEnd(() => lightEntity.TurnOn(), () => lightEntity.TurnOff(), scheduler);
```

This ensures the light automatically turns **on or off** based on whether the current time falls within the defined period. Additionally, it will set the light to the correct state initially, matching the period's condition at the time the program starts.

### Use Crons to Create Periods

The following example demonstrates the use of cron expressions using `Occurify.TimeZones`. Not only does it allow you to create a timeline with instants, but it can also convert a cron expression directly into a period-based timeline (e.g., hours, days).

This example calculates how many working days there are if we exclude public holidays.
```cs
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
```

### Working with Different Periods

Occurify allows us to define periods however we want. In this example we use `TimeZoneInstants.StartOfMonth(10)` to get a timeline with the start of every October. Using `AsConsecutivePeriodTimeline` turns those instants into consecutive periods that we can use to represent fiscal years.

```cs
IPeriodTimeline calendarYears = TimeZonePeriods.Years();
IPeriodTimeline fiscalYears = TimeZoneInstants.StartOfMonths(10).AsConsecutivePeriodTimeline();

Period currentCalendarYear = calendarYears.SampleAt(DateTime.UtcNow).Period!;
Period currentFiscalYear = fiscalYears.SampleAt(DateTime.UtcNow).Period!;
```

Next, we can use `EnumerateRange` to count how many workdays have passed in both the calendar and fiscal years:

```cs
IPeriodTimeline workdays = TimeZonePeriods.Workdays();
int amountOfCalendarDaysWorked = workdays.EnumerateRange(currentCalendarYear.Start!.Value, DateTime.UtcNow).Count();
int amountOfFiscalYearDaysWorked = workdays.EnumerateRange(currentFiscalYear.Start!.Value, DateTime.UtcNow).Count();
```

### Searching Dates

Here's an example of how you can find out how many Fridays there were in February of the years 1200 and 1201:

```cs
IPeriodTimeline fridaysOfFebruary = TimeZonePeriods.Months(2, TimeZoneInfo.Utc) & TimeZonePeriods.Days(DayOfWeek.Friday, TimeZoneInfo.Utc);
Period twoYears = TimeZoneInstants.StartOfYear(1200, TimeZoneInfo.Utc).To(TimeZoneInstants.EndOfYear(1201, TimeZoneInfo.Utc));

Console.WriteLine("The years 1200 and 1201 have the following fridays in february:");
foreach (var date in fridaysOfFebruary.EnumeratePeriod(twoYears))
{
    Console.WriteLine(date.Start!.Value.ToShortDateString());
}
```

### Finding Available Periods Between Reservations

This example demonstrates how to efficiently identify gaps of a minimum duration within a set of reservations, constrained to a specific search range.
```cs
public Period[] FindAvailableFreePeriods(Period searchRange, Period[] reservations, TimeSpan minimumDuration)
{
    return reservations
        .Invert() // Identify free periods by inverting the reserved ones
        .WherePeriods(p => p.Duration >= minimumDuration) // Filter periods that meet the minimum duration requirement
        .EnumeratePeriod(searchRange) // Restrict results to the given search range
        .ToArray();
}
```

### Solar Phases

In this example, we calculate how many days in the current year in the Arctic region don't experience either a sunset or a sunrise:

```cs
Coordinates arcticCoordinates = new Coordinates(80.45302, 54.77918, Height: 37);
ITimeline sunsetsAndRises = AstroInstants.SunPhases(arcticCoordinates, SunPhases.Sunrise | SunPhases.Sunset);
IPeriodTimeline daysOfCurrentYear = TimeZonePeriods.Days().Within(TimeZonePeriods.CurrentYear());
IPeriodTimeline daysWithoutSunsetsOrRises = daysOfCurrentYear - daysOfCurrentYear.Containing(sunsetsAndRises);

Console.WriteLine($"This year on the arctic the sun doesn't rise or set on {daysWithoutSunsetsOrRises.Count()} days.");
```

### Complicated Requirements

Some scenarios require more complex scheduling logic. In this example, we want to turn on the living room lights in the evening when we're on vacation, but with the following conditions:

- Turn on **10 minutes after sunset**, with a **random deviation of 5 minutes before and 10 minutes after** to make it look natural.
- **Never turn on before 5:15 PM** and **never after 8 PM**.
- **Turn off at 11:45 PM**, with a **random deviation of ±20 minutes**.
- Ensure consistent behavior across restarts by using a fixed random seed.

Here's how we can achieve this using Occurify:

```cs
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
lightOnPeriods.SubscribeStartEnd(() => lightEntity.TurnOn(), () => lightEntity.TurnOff(), scheduler);
```

#### Explanation

- `Randomize(seed, minOffset, maxOffset)` ensures that randomness is applied consistently, even across restarts.
- `LastWithin(TimeZonePeriods.Days())` makes sure we use the last valid time after the 5:15 PM cutoff.
- `FirstWithin(TimeZonePeriods.Days())` finds the earliest valid moment within the defined constraints.
- `SubscribeStartEnd(() => lightEntity.TurnOn(), () => lightEntity.TurnOff(), scheduler)` immediately updates the light to the correct state when the program starts and schedules future on/off transitions based on the defined period.

This logic ensures the light turns on and off naturally based on the specified conditions, making automation less predictable and more human-like.

### Working with Multiple Periods

Occurify also makes it easy to work with multiple schedules and availability periods. This example demonstrates how to determine when employees are free for a meeting and how many people were in appointments at a given time.

#### Defining Working Hours and Availability

```cs
IPeriodTimeline workingHours = TimeZonePeriods.Between(startHour: 8, endHour: 18) - TimeZonePeriods.Weekends();

List<Period[]> employeeAppointments = CustomLogic.LoadAppointments();
IPeriodTimeline[] appointmentTimelines = employeeAppointments.Select(p => p.AsPeriodTimeline()).ToArray();
IPeriodTimeline[] invertedTimelines = appointmentTimelines.Select(tl => tl.Invert()).ToArray();

IPeriodTimeline availableSlotsTimelines = invertedTimelines.IntersectPeriods() & workingHours;
```

#### Finding Common Availability

To check which periods all employees are available for a meeting in August 2025:

```cs
Period august = TimeZonePeriods.Month(8, 2025);
IEnumerable<Period> periodsEveryoneIsAvailable = availableSlotsTimelines
    .EnumeratePeriod(august).Where(p => p.Duration > TimeSpan.FromHours(1));

Console.WriteLine("Everyone is available on:");
foreach (Period period in periodsEveryoneIsAvailable)
{
    Console.WriteLine(period.ToString(TimeZoneInfo.Local));
}
```

#### Checking How Many People Had an Appointment

To see how many employees had a meeting at a specific time:

```cs
DateTime timeOfInterest = new DateTime(2025, 3, 7).AsUtcInstant();
PeriodTimelineSample[] samples = appointmentTimelines.Select(tl => tl.SampleAt(timeOfInterest)).ToArray();

int appointmentPeriods = samples.Count(s => s.IsPeriod);
int freeTimePeriods = samples.Count(s => s.IsGap);

Console.WriteLine($"{appointmentPeriods} people had an appointment on {timeOfInterest}.");
Console.WriteLine($"{freeTimePeriods} people had were free on {timeOfInterest}.");
```

## Design

Occurify uses 4 main concepts:

Concept | Represented by | Description
--- | --- | ----
[Instant](#instant) | UTC `DateTime` | A single instant in time.
[Period](#period) | `Period` | A period of time, defined by a start and end instant.
[Instant timeline](#instant-timeline) | `ITimeline` | A timeline containing instants.
[Period timeline](#period-timeline) | `IPeriodTimeline` | A timeline containing periods.

### Instant

An instant is represented using a `DateTime` with `Kind` set to `DateTimeKind.Utc`.

The valid range for an instant is from `01-01-0000` to `31-12-9999`.

### Period

A period is defined by two instants: a start and an end.

- If the start is null, it means the period has no defined beginning (i.e., it started at the beginning of time).
- If the end is null, it means the period has no defined end (i.e., it lasts indefinitely).
- If both start and end are null, the period is infinite in both directions.

A period contains all instants that are greater than or equal to the start instant and smaller than the end instant:
`(Start == null || instant >= Start) &&
(End == null || instant < End)`

**Key Concept:** Consecutive periods (a period with the same start as the end of another) do not overlap, ensuring that each instant belongs to only one period.

#### Period Record:
```cs
record Period(DateTime? Start, DateTime? End) : IComparable<Period>
```

#### Different ways of creating a period:
```cs
DateTime utcNow = DateTime.UtcNow;

// Using extension methods
Period nowToOneHoursFromNow = utcNow.ToPeriodWithDuration(TimeSpan.FromHours(1));
Period nowToTwoHoursFromNow = utcNow.To(utcNow + TimeSpan.FromHours(2));
Period nowToNeverEnding = utcNow.To(null);

// Using static methods
Period nowToThreeHoursFromNow = Period.Create(utcNow, utcNow + TimeSpan.FromHours(3));
```

### Instant Timeline

An instant timeline represents a timeline of instants.

Although an instant timeline implements `IEnumerable<DateTime>`, it does not necessarily represent a collection of instants. Instead, it can represent the concept of a specific timeline. For example, an instant timeline can represent the concept of "all sunsets," without having to calculate or store all sunset times in memory. Instants in the timeline are only calculated when the timeline is enumerated or iterated over.

Within Occurify, a timeline has the following properties:
- **Immutable:** Once created, an instant timeline cannot be modified.
- **Deterministic:** The timeline will always yield the same instants given the same parameters.
- **Works with UTC:** All instants in an instant timeline are represented as `DateTime` values in UTC (`DateTimeKind.Utc`).

An instant timeline implements `IEnumerable<DateTime>`, meaning that you can enumerate through it to access all instants, starting from the earliest to the latest.

#### Timeline Interface:
```cs
public interface ITimeline : IEnumerable<DateTime>
{
    DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo);
    DateTime? GetNextUtcInstant(DateTime utcRelativeTo);
    bool IsInstant(DateTime utcDateTime);
}
```

#### Different ways of creating an instant pipeline:
```cs
DateTime utcNow = DateTime.UtcNow;

// Using extension methods
ITimeline timeline1 = utcNow.AsTimeline();
ITimeline timeline2 = new[] { utcNow, utcNow + TimeSpan.FromHours(1) }.AsTimeline();

// Using static methods
ITimeline timeline3 = Timeline.FromInstants(utcNow, utcNow + TimeSpan.FromHours(1), utcNow, utcNow + TimeSpan.FromHours(3));
ITimeline timeline4 = Timeline.Periodic(TimeSpan.FromHours(1));
```
Note that `timeline4` is not a timeline with concrete instants. Only when reading it, will the instants be resolved. Simular to `Linq` methods, filtering only wraps the timeline in a filter class. Instants will only be resolved by reading.

### Period Timeline

A period timeline is defined by a start timeline and an end timeline and represents a timeline of periods.

Periods on the period timeline start at any instant on the start timeline and end by the next first instant on the end timeline. Periods cannot overlap.

Due to the nature of instant timelines, period timelines can also represent a concept, but in the form of a period. For example, a period timeline can represent a concept like "all periods between sunrise and sunset", by constructing it from an instant timeline that represents "all sunrises" and one that represents "all sunsets".

If the earliest instant on both the start and end timelines is an end instant, the first period is assumed to have always started (start = `null`). Simularly, if the last instant is a start instant, the last instant is assumed to never end (end = `null`). If a period has no start and no end, it is empty. A period timeline cannot contain an infinite period.

If there are more consecutive start instants, the earliest one defines the start of a period. In case there are consecutive end instants, the earliest one defines the end of a period.

A period timeline also implements `IEnumerable<Period>`. Enumerating a timeline will iterate all instants in a timeline from earliest to latest.

#### Period Timeline Interface:
```cs
public interface IPeriodTimeline : IEnumerable<Period>
{
    ITimeline StartTimeline { get; }
    ITimeline EndTimeline { get; }
}
```

#### Different ways of creating an period pipeline:
```cs
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
```

### Collections

In Occurify, collections of `ITimeline` (and soon `IPeriodTimeline` as well) are treated as first-class citizens. This means that all extension methods available for `ITimeline` can also be used on `IEnumerable<ITimeline>`, `IEnumerable<KeyValuePair<TKey, ITimeline>>` and `IEnumerable<KeyValuePair<ITimeline, TValue>>`.

This is particularly powerful when you want to associate additional state or metadata (such as booleans, labels, or categories) with each timeline while still applying timeline operations across the collection.

For example:

```cs
Dictionary<ITimeline, bool> sunStates = new Dictionary<ITimeline, bool>
{
    { AstroInstants.LocalSunrise, true },
    { AstroInstants.LocalSunset, false }
};
foreach (KeyValuePair<DateTime, bool[]> state in sunStates.EnumeratePeriod(TimeZonePeriods.CurrentMonth()))
{
    bool sunIsRising = state.Value.First(); // Since we're combining multiple timelines, a single instant may correspond to multiple values. However, for this example, we assume sunrise and sunset don't occur simultaneously, so we just take the first value.
    Console.WriteLine(sunIsRising ? 
        $"It is currently {state.Key} and the sun is rising!" : 
        $"It is currently {state.Key} and the sun is setting!");
}
```

## Coordinates

All methods in `Occurify.Astro` have a signature with and without `Coordinates` object. If no `Coordinates` object is provided, the method will use `Coordinates.Local` by default. Note that this static property needs to be set before use:

```cs
Coordinates.Local = new Coordinates(78.2384, 15.4463, height: 126);
```

## ASCII Representation of Timelines

For both documentation (examples in this README) and testing purposes (discussed further in the [Unit Tests](#unit-tests) chapter), an ASCII notation is used to describe instants, periods, instant timelines and period timelines.

The following characters are used in the notation:
Character | Meaning
---|---
*space* | No instant
\|| Instant on an instant timeline
< |Instant on start of a period timeline
\> |Instant on end of a period timeline
X|Instant on the exact same moment on both the start and end of a period timeline

This notation is used to describe timelines relative to each other. To ensure clarity, the ASCII lines should be properly aligned.

The following example shows how instants in a start and end timeline result in a period timeline:

```
Start instant timeline:    "|  |    |  "
End instant timeline:      "   |  |   |"
Resulting period timeline: "<  X  > < >"
```

## Extension Methods

Occurify provides a wide range of (fluent) extension methods for working with instants, periods, instant timelines, and period timelines. This chapter summarizes the most important methods and illustrates their functionality using ASCII notation. While not an exhaustive list, it highlights the methods that require the most explanation.

### Enumeration

Both `ITimeline` and `IPeriodTimeline` derive from `IEnumerable<DateTime>` and `IEnumerable<Period>`. While iterating over these classes directly can be useful—especially when they contain a limited number of elements—we often need a subset of a timeline. To facilitate this, the following extension methods are implemented for both interfaces:

Method | Meaning
--- |---
Enumerate | Enumerates all instants/periods on the source timeline from earliest to latest.
EnumerateBackwards | Enumerates all instants/periods on the source timeline from latest to earliest.
EnumerateFrom | Enumerates all instants/periods on the source timeline that occur on or after a provided start instant from earliest to latest.
EnumerateBackwardsTo | Enumerates all instants/periods on the source timeline that occur on or after a provided end instant from latest to earliest.
EnumerateTo | Enumerates all instants/periods on the source timeline that occur earlier than a provided end instant from earliest to latest.
EnumerateBackwardsFrom | Enumerates all instants/periods on the source timeline that occur earlier than a provided start instant from latest to earliest.
EnumerateRange | Enumerates all instants/periods on the source timeline that occur between a provided start and end instant from earliest to latest.
EnumerateRangeBackwards | Enumerates all instants/periods on the source timeline that occur between a provided start and end instant from latest to earliest.
EnumeratePeriod | Enumerates all instants/periods on the source timeline that occur in a provided period from earliest to latest.
EnumeratePeriodBackwards | Enumerates all instants/periods on the source timeline that occur in a provided period from latest to earliest.

For all extension methods on `IPeriodTimeline`, you can specify whether a period should be completely inside a range or if touching one or both sides of the range/period is also acceptable. For example, with `EnumerateTo`, there is an alternative method called `EnumerateToIncludingPartial`. Both the range and period methods accept an optional `PeriodIncludeOptions` parameter, which can be set to one of the following values: `CompleteOnly`, `StartPartialAllowed`, `EndPartialAllowed`, or `PartialAllowed`.

### Filter ITimeline

#### SkipWithin

Returns a `ITimeline` in which the first x number instants within every period in a provided mask are bypassed.

```
"source": "| | || | | "
"mask  ": "<    X    >"
"skip  ": 1
"result": "  | |  | | "
```

#### SkipLast

Returns a `ITimeline` in which the last x number instants within every period in a provided mask are bypassed.

```
"source": "| | || | | "
"mask  ": "<    X    >"
"skip  ": 1
"result": "| |  | |   "
```

#### Take

Returns a `ITimeline` that contains the first x number instants within every period in a provided mask.

```
"source": "| | || | | "
"mask  ": "<    X    >"
"take  ": 1
"result": "|    |     "
```

#### TakeLast

Returns a `ITimeline` that contains the last x number instants within every period in a provided mask.

```
"source": "| | || | | "
"mask  ": "<    X    >"
"take  ": 1
"result": "    |    | "
```

#### Containing

Filters the source timeline based on which instants are also present in a provided instants/instant timeline.

```
"source  ": "| | | | | |"
"instants": "  ||    || "
"result  ": "  |     |  "
```

> Containing is also implemented for the `&` operator.

#### Without

Filters instants from a provided instants/instant timeline.

```
"source  ": "| | | | | |"
"instants": "  ||    || "
"result  ": "|   | |   |"
```

> Without is also implemented for the `-` operator.

#### Within

Filters `ITimeline` based on which instants are inside any of the periods in a provided mask.

```
"source": "| | || | | "
"mask  ": "<   > <   >"
"result": "| |    | | "
```

#### Outside

Filters `ITimeline` based on which instants are not inside any of the periods in a provided mask.

```
"source": "| | || | | "
"mask  ": "<   > <   >"
"result": "    ||     "
```

### Filter IPeriodTimeline

#### Within

Filters `IPeriodTimeline` based on which periods are inside any of the periods in a provided mask.

```
"source": "< >  < > <>"
"mask  ": "<   > <   >"
"result": "< >      <>"
```

#### Outside

Filters `IPeriodTimeline` based on which periods are not inside any of the periods in a provided mask.

```
"source": "< >  < > <>"
"mask  ": "<   > <   >"
"result": "     < >   "
```

#### Containing

Filters `IPeriodTimeline` based on which periods contain any of the provided periods.

```
"source": "<   > <   >"
"other ": "< >  < >   "
"result": "<   >      "
```

#### Without

Filters `IPeriodTimeline` based on which periods do not contain any of the periods.

```
"source": "<   > <   >"
"other ": "< >  < >   "
"result": "      <   >"
```

### Transform ITimeline

#### Combine

Returns a `ITimeline` with the instants from both the source timeline and the provided instants/instant timeline.

```
"source  ": "| | | | | |"
"instants": " |  |  |  | "
"result  ": "||| | ||| | "
```

> Combine is also implemented for the `+` and `|` operators.

#### Offset

Offsets the source timeline with a datetime.

```
"source": "|   | |    "
"offset": 3
"result": "   |   | | "
```

> Offset is also implemented for the `+` and `-` operators in combination with a `TimeSpan`.

#### Randomize

Randomizes the source instants.
This method will never result in a change of instant count or in overlapping instants.
Identical inputs with the same seed, will result in the same output.

#### To

Returns a `IPeriodTimeline` with periods starting at instants on the source timeline and ending with instants on the provided second timeline. The result is Normalized.

```
"source  ": "|  |    |  "
"instants": "   |  |   |"
"result  ": "<  X  > < >"
```

Example with normalization:

```
"source  ": "|| |    |  "
"instants": "   |  | | |"
"result  ": "<  X  > < >"
```

#### AsConsecutivePeriodTimeline

Returns a `IPeriodTimeline` with consecutive periods starting and ending with instants on the provided source timeline.

```
"source ": "|  |    |  "
"result ": "X  X    X  "
```

### Transform IPeriodTimeline

#### Cut

Returns a `IPeriodTimeline` in which periods from the source are cut at provided instants.

```
"source  ": "<    >  < >"
"instants": "|   |    | "
"result  ": "<   X>  <X>"
```

#### Stitch

Returns a `IPeriodTimeline` in which all periods in the source with equal end and start instants are combined into a single period.

```
"source  ": "<   X>  <X>"
"result  ": "<    >  < >"
```

#### IntersectPeriods

Returns a `IPeriodTimeline` with the intersections of the source with another `IPeriodTimeline`.

```
"source": "<   >  <  >"
"other ": "  <  ><  > "
"result": "  < >  < > "
```

> IntersectPeriods is also implemented for the `&` operator.

#### Invert

Returns a `IPeriodTimeline` that is inverted of the provided source.

```
"source": " <  > <  > "
"result": " >  < >  < "
```

#### Merge

Merges all periods in from the source with all periods in another `IPeriodTimeline`. Overlapping periods are combined.

```
"source": "< >   < ><>"
"other ": " <  >  <  >"
"result": "<   > <   >"
```

> Merge is also implemented for the `+` and `|` operators.

#### Subtract

Subtracts all periods in a provided `IPeriodTimeline` from all periods in the source.

```
"source    ": "<   >  <  >"
"subtrahend": "  <   > < >"
"result    ": "< >    <>  "
```

> Subtract is also implemented for the `-` operator.

#### Offset

Offsets the source with a given timespan.

```
"source": "<  >  <>   "
"offset": 3
"result": "   <  >  <>"
```

> Offset is also implemented for the `+` and `-` operators in combination with a `TimeSpan`.

#### Randomize

Randomizes the source periods.
This method will never result in a change of period count or in overlapping periods.
Identical inputs with the same seed, will result in the same output.

## Important Considerations

When applying filters to a timeline, the source timeline still needs to be evaluated upon enumeration. This is an important factor to consider when using a filter.

For example: If you apply a `Within` filter to a period timeline containing Mondays and use Fridays as a mask, every call to the filtered timeline will loop through both timelines when requesting an instant.

```cs
IPeriodTimeline mondays = TimeZonePeriods.Days(DayOfWeek.Monday);
IPeriodTimeline fridays = TimeZonePeriods.Days(DayOfWeek.Friday);

IPeriodTimeline mondaysWithinFridays = mondays.Within(fridays);

DateTime? firstInstant = mondays.StartInstantProvider.GetNextUtcInstant(DateTime.UtcNow); // This method will eventually return null, but won't perform.
```

## Unit Tests

This library is extensively tested using unit tests.

Since most tests follow a similar pattern, the ASCII notation is also utilized for consistency. The tests are dynamically loaded from JSON files containing test cases.

Here’s an example snippet from `PeriodTimeline.Merge.json`:

```json
{
  "source  ": "<><>",
  "periods ": "<   ",
  "expected": "<   "
},
{
  "source  ": "<><>",
  "periods ": " <  ",
  "expected": "<X  "
},
{
  "source  ": "<><>",
  "periods ": "  < ",
  "expected": "<>< "
},
```

Each of these tests is executed at least three times: once for `GetPreviousUtcInstant`, once for `GetNextUtcInstant` and once for `IsInstant`.

## Versioning and Stability

This library follows Semantic Versioning (SemVer), using the format MAJOR.MINOR.PATCH (x.y.z):

- MAJOR (X) – Increases when there are breaking changes (starting from 1.0.0).
- MINOR (Y) – Increases when new features are introduced (or breaking changes while still in 0.x).
- PATCH (Z) – Increases when making backward-compatible bug fixes.

⚠️ Since this library is still in version 0.x, breaking changes may occur even in minor version updates (e.g., 0.2.0 → 0.3.0). Until we reach a stable 1.0.0, updates may require adjustments to your code.

For ongoing discussions and future changes, check out [this GitHub discussion](https://github.com/Occurify/Occurify/discussions/4).

## Release Notes

See the latest changes in [GitHub Releases](https://github.com/Occurify/Occurify/releases).

## Multi-Language Support

The concepts behind Occurify are language-agnostic.

This repository currently contains the .NET implementation of Occurify. If you're interested in implementing this concept in another language (e.g. Java, Python, JavaScript), feel free to open an issue or reach out!

If a new implementation is created, this repo may be renamed to better organize multi-language versions.

📢 Want to contribute a new language version? Let’s discuss it in the [discussions section](https://github.com/Occurify/Occurify/discussions)!

## License

Copyright © 2025 Jasper Lammers. Occurify is licensed under The MIT License (MIT).