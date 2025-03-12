# Occurify

A comprehensive and intuitive .NET library for defining, filtering, transforming, and scheduling time periods.

## 📖 Table of Contents  
- [Overview](#Overview)
- [Installation](#installation)
- [Usage](#usage)
- [Potential Use Cases](#potential-use-cases)
    - [Between 7 AM and 15 Minutes Past Sunrise](#between-7-am-and-15-minutes-past-sunrise)
    - [Use Crons to Create Periods](#use-crons-to-create-periods)
    - [Working With Different Periods](#working-with-different-periods)
    - [Searching Dates](#searching-dates)
    - [Solar Phases](#solar-phases)
- [Design](#design)
    - [Instant](#instant)
    - [Period](#period)
    - [Instant Timeline](#instant-timeline)
    - [Period Timeline](#period-timeline)
- [ASCII Representation of Timelines](#ascii-representation-of-timelines)
- [Extension Methods](#extension-methods)
- [Unit Tests](#unit-tests)
- [Important Considerations](#important-considerations)
- [Multi-Language Support](#multi-language-support)
- [License](#license)

## Overview

**Occurify** *(core library, this repository)*

- Supports instants, periods, timelines and period timelines.
- Implements collection and periodic timelines.
- Supports an extensive set of fluent extension methods to filter and transform instants, periods, timelines and period timelines.
- Includes 4500+ unit tests to ensure reliability.

**Occurify.TimeZones** ([Github page](https://github.com/Occurify/Occurify.TimeZones))

- Supports time zone based instants and periods (e.g. time of day, day, week, etc).
- Supports both forwards and backwards iteration through Cron instants and periods.
- Uses the *Cronos* library to enable Cron functionality that:
- Supports standard Cron format with optional seconds.
- Supports time zones, and performs all the date/time conversions for you.
- Does not skip occurrences, when the clock jumps forward to daylight saving time (known as Summer time).
- Does not skip interval-based occurrences, when the clock jumps backward from Summer time.
- Does not retry non-interval based occurrences, when the clock jumps backward from Summer time.

**Occurify.Astro** ([Github page](https://github.com/Occurify/Occurify.Astro))

- Uses the *SunCalcNet* library to enable functionality that:
- Supports location (coordinate) based instants and periods (e.g. dawn, daytime, etc).
- Supports multiple solar phases (sunrise, sunset, end of sunrise, start of sunset, (nautical) dawn, (nautical) dusk, (end of) night, (end of) golden hour, solar noon and nadir).

**Occurify.Reactive** ([Github page](https://github.com/Occurify/Occurify.Reactive))

- Uses ReactiveX to enable scheduling for both timelines and periods.

## Installation

Occurify is distributed as a [NuGet package](https://www.nuget.org/packages/Occurify), you can install it from the official NuGet Gallery. Please use the following command to install it using the NuGet Package Manager Console window.
```
PM> Install-Package Occurify
```

## Usage

Rather than working with concrete instants and periods in time, Occurify allows for conceptual representation of time using intstant and period timelines.

For example, rather than listing all workdays of a year to work with, you can define the concept of "all workdays", apply transformations or filters, and extract the relevant periods as needed.

The following example demonstrates how to define a period timeline, `workingHours` that includes all periods between **8 AM and 6 PM**. By subtracting weekends, we obtain a new period timeline, `workingTime`, that represents all workdays within that range:
```cs
IPeriodTimeline workingHours = TimeZonePeriods.Between(startHour: 8, endHour: 18);
IPeriodTimeline weekends = TimeZonePeriods.Days(DayOfWeek.Saturday, DayOfWeek.Sunday);
IPeriodTimeline workingTime = workingHours - weekends;
```
Now, `workingTime` represents all workdays from **8 AM and 6 PM**.

```cs
Console.WriteLine(workingTime.IsNow() ? "You should still be working" : "You can go home!");
```
Or
```cs
Console.WriteLine($"The year 1025 contained {workingTime.EnumeratePeriod(TimeZonePeriods.Year(1025)).Count()} workdays.");
Console.WriteLine($"The year 3025 will contain {workingTime.EnumeratePeriod(TimeZonePeriods.Year(3025)).Count()} workdays.");
```
The period timeline **only resolves the necessary periods when enumerated**, ensuring efficiency.

This approach allows developers to focus on what they need—such as "workdays"—without manually managing time calculations. As a result, coding becomes more intuitive and use case-driven.

## Potential Use Cases

This section presents various use cases that demonstrate Occurify’s capabilities and provide a clearer understanding of its functionality.

These examples incorporate additional modules such as `Occurify.TimeZones`, `Occurify.Astro`, and `Occurify.Reactive`.

>**Note: Instead of using `var`, variable types are explicitly defined in the examples for improved clarity.**

### Between 7 AM and 15 Minutes Past Sunrise

The following example demonstrates how to turn on a light between **7 AM and 15 minutes after sunrise**.

> **Note:** `AstroInstants` is provided by `Occurify.Astro`, while `TimeZoneInstants` comes from `Occurify.TimeZones`.

#### Defining the Period
```cs
ITimeline fifteenMinAfterSunRise = AstroInstants.LocalSunRise + TimeSpan.FromMinutes(15);
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

Now we can use `ToBooleanObservableIncludingCurrent` from `Occurify.Reactive` to integrate with `ReactiveX` for event-driven scheduling:
```cs
between7AndSunRiseInTheMorning.ToBooleanObservableIncludingCurrent(scheduler).Subscribe(inPeriod =>
{
    if (inPeriod)
    {
        lightEntity.TurnOn();
        return;
    }
    lightEntity.TurnOff();
});
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

IPeriodTimeline holidays = holidayCrons.Select(TimeZonePeriods.Days).Merge(); // TimeZonePeriods.Days allows us to turn a cron expression into a period of a day in one step.
IPeriodTimeline workingDays = TimeZonePeriods.Workdays();
IPeriodTimeline workingDaysWithoutHolidays = workingDays - holidays;

Console.WriteLine("Work days this year:");
foreach (Period period in workingDaysWithoutHolidays.EnumeratePeriod(TimeZonePeriods.CurrentYear()))
{
    Console.WriteLine(period.ToString(TimeZoneInfo.Local));
}
```

### Working With Different Periods

Occurify allows us to define periods however we want. In this example we use `TimeZoneInstants.StartOfMonth(10)` to get a timeline with the start of every October. Using `AsConsecutivePeriodTimeline` turns those instants into consecutive periods that we can use to represent fiscal years.

```cs
IPeriodTimeline calendarYears = TimeZonePeriods.Years();
IPeriodTimeline fiscalYears = TimeZoneInstants.StartOfMonth(10).AsConsecutivePeriodTimeline();

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

### Solar Phases

In this example, we calculate how many days in the current year in the Arctic region don't experience either a sunset or a sunrise:

```cs
Coordinates arcticCoordinates = new Coordinates(80.45302, 54.77918, Height: 37);
ITimeline sunsetsAndRises = AstroInstants.SunPhases(arcticCoordinates, SunPhases.Sunrise | SunPhases.Sunset);
IPeriodTimeline daysOfCurrentYear = TimeZonePeriods.Days().Within(TimeZonePeriods.CurrentYear());
IPeriodTimeline daysWithoutSunsetsOrRises = daysOfCurrentYear - daysOfCurrentYear.Containing(sunsetsAndRises);

Console.WriteLine($"This year on the arctic the sun doesn't rise or set on {daysWithoutSunsetsOrRises.Count()} days.");
```

## Design

Occurify uses 4 main concepts:

Concept | Represented by | Description
--- | --- | ----
Instant | UTC `DateTime` | A single instant in time.
Period | `Period` | A period of time, defined by a start and end instant.
Instant timeline | `ITimeline` | A timeline containing instants.
period timeline | `IPeriodTimeline` | A timeline containing periods.

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

#### Period:
```cs
record Period(DateTime? Start, DateTime? End) : IComparable<Period>
```

#### Different ways of creating a period:
```cs
var utcNow = DateTime.UtcNow;

// Using extension methods
var nowToOneHoursFromNow = utcNow.ToPeriodWithDuration(TimeSpan.FromHours(1));
var nowToTwoHoursFromNow = utcNow.To(utcNow + TimeSpan.FromHours(2));
var nowToNeverEnding = utcNow.To(null);

// Using static construction
var nowToThreeHoursFromNow = Period.Create(utcNow, utcNow + TimeSpan.FromHours(3));
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
var utcNow = DateTime.UtcNow;

// Using extension methods
var timelineWithSingleInstant = utcNow.AsTimeline();
var timelineWithTwoInstants = new[] { utcNow, utcNow + TimeSpan.FromHours(1) }.AsTimeline();

// Using static construction
var timelineWithThreeInstants = Timeline.FromInstants(utcNow, utcNow + TimeSpan.FromHours(1), utcNow, utcNow + TimeSpan.FromHours(3));
var timelineWithPeriodicInstants = Timeline.Periodic(TimeSpan.FromHours(1));
```
Note that `timelineWithPeriodInstants` is not a timeline with concrete instants. Only when reading it, will the instants be resolved. Simular to `Linq` methods, filtering only wraps the timeline in a filter class. Instants will only be resolved by reading.

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
var utcNow = DateTime.UtcNow;
var period = utcNow.To(utcNow + TimeSpan.FromHours(2));

// Using extension methods
var periodTimelineWithSinglePeriod = period.AsPeriodTimeline();
var periodTimelineWithTwoPeriods = new[] { period, period + TimeSpan.FromHours(2) }.AsPeriodTimeline();

// Using static construction
var periodTimelineWithThreePeriods = PeriodTimeline.FromPeriods(period, period + TimeSpan.FromHours(2), period + TimeSpan.FromHours(4));
var periodTimelineWithPeriodicPeriodsOf10Min = Timeline.Periodic(TimeSpan.FromHours(1)).To(Timeline.Periodic(TimeSpan.FromHours(1)) + TimeSpan.FromMinutes(10));
```

## ASCII Representation of Timelines

For both documentation (examples in this README) and testing purposes (discussed further in the [Unit Tests](#unit-tests) chapter), an ASCII notation is used to describe instants, periods, instant timelines and period timelines.

The following characters are used in the notation:
Character | Meaning
---|---
*space* | No instant
\|| Instant on a instant timeline
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

## Multi-Language Support

The concepts behind Occurify are language-agnostic.

This repository currently contains the .NET implementation of Occurify. If you're interested in implementing this concept in another language (e.g., Java, Python, JavaScript), feel free to open an issue or reach out!

If a new implementation is created, this repo may be renamed to better organize multi-language versions.

📢 Want to contribute a new language version? Let’s discuss it in the [discussions section](https://github.com/Occurify/Occurify/discussions)!

## License

Copyright © 2025 Jasper Lammers. Occurify is licensed under The MIT License (MIT).