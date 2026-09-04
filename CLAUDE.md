# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Test

All source projects multi-target `net8.0;net9.0;net10.0`. The examples app targets `net9.0` only.

```powershell
# CI parity build (warnings are errors — missing XML docs on public members will fail this)
dotnet build --configuration Release -p:TreatWarningsAsErrors=true

# Run everything (CI runs the five test projects individually)
dotnet test Occurify.sln

# One test project, one target framework (much faster while iterating)
dotnet test tests/Occurify.Tests/Occurify.Tests.csproj --framework net10.0

# A single test class or method
dotnet test tests/Occurify.Tests/Occurify.Tests.csproj --framework net10.0 --no-build --filter "FullyQualifiedName~PeriodTimelineMergeTests"

# Run the example app (examples are System.CommandLine subcommands)
dotnet run --project examples/Occurify.Examples -- --help
```

Packages are published on GitHub release via `.github/workflows/nuget-publish.yml`; version comes from the git tag, not from the csproj.

## Architecture

Five packages, all layered on `Occurify`:

| Project | Depends on | Purpose |
| --- | --- | --- |
| `src/Occurify` | — | Core: `ITimeline`, `IPeriodTimeline`, `Period`, and the fluent extension surface |
| `src/Occurify.TimeZones` | Cronos | `TimeZoneInstants` / `TimeZonePeriods`, cron-backed timelines |
| `src/Occurify.Astro` | SunCalcNet | `AstroInstants` / `AstroPeriods`, solar-phase timelines, `Coordinates.Local` |
| `src/Occurify.Reactive` | System.Reactive | `Subscribe*` / `ToObservable` / `Schedule*` extensions |
| `src/Occurify.NodaTime` | NodaTime | `Instant` / `Duration` / `Interval` overloads of the core extensions |

### The central idea: timelines are lazy concepts, not collections

`ITimeline` has exactly three members — `GetPreviousUtcInstant`, `GetNextUtcInstant`, `IsInstant` — plus `IEnumerable<DateTime>`. Nothing is precomputed. "All sunsets" is an object that answers those three questions on demand, and every filter/transform wraps the source in another `Timeline` subclass (LINQ-style). This is why almost all work in this repo happens in small internal `Timeline` subclasses rather than in the extension methods themselves.

`IPeriodTimeline` is **a pair of instant timelines** (`StartTimeline`, `EndTimeline`). Periods are derived: a period starts at a start instant and ends at the next end instant. Consequently every period-level operation is implemented as *two* timeline classes — see `PeriodTimelineTransformations/MergeStartTimeline.cs` and `MergeEndTimeline.cs`, `CutStartTimeline`/`CutEndTimeline`, and so on, under:

- `src/Occurify/TimelineFilters/`, `TimelineTransformations/`, `Timelines/`, `TimelineUtils/`
- `src/Occurify/PeriodTimelineFilters/`, `PeriodTimelineTransformations/`, `PeriodTimelineCollectionTransformations/`

The extension method (e.g. `PeriodTimelineExtensions.Cut`) is normally a thin factory: `new PeriodTimeline(new CutStartTimeline(...), new CutEndTimeline(...))`.

### Extension method surface

`src/Occurify/Extensions/` is partitioned by *receiver type* × *category*. The receiver types are:

`TimelineExtensions`, `TimelineCollectionExtensions`, `TimelineKeyCollectionExtensions` (`IEnumerable<KeyValuePair<TKey, ITimeline>>`), `TimelineValueCollectionExtensions` (`IEnumerable<KeyValuePair<ITimeline, TValue>>`) — and the same four for `IPeriodTimeline`.

The categories are the file suffixes: `.Filter`, `.Transform`, `.Enumerate`, `.Utils`.

**Adding a new operation usually means touching all eight receiver files, not one.** Collections and dictionaries of timelines are first-class in this library; a method that only exists on the single-timeline overload is considered incomplete.

### Occurify.NodaTime mirrors, it does not reimplement

Every method in `src/Occurify.NodaTime/Extensions/` converts and delegates to the core method (`source.Cut(instant.ToDateTimeUtc())`). Naming rules, applied consistently:

- Keep the core name; the argument type selects the overload (`Within(Interval)`, `Offset(Duration)`).
- Drop `Utc` — an `Instant` is always UTC (`GetNextInstant`, not `GetNextUtcInstant`).
- `Instant` / `Interval` in a *name* means that type is **returned** (`EnumerateInstants*`, `GetNextCompleteInterval`).

Both Occurify and NodaTime define `Period`; files in this project alias with `using Period = Occurify.Period;` where needed.

## Conventions

- **Instants are UTC `DateTime`.** Every public entry point that accepts a `DateTime` validates `Kind != DateTimeKind.Utc` and throws `ArgumentException`. New `Timeline` subclasses must do this in all three overridden methods.
- **Use `DateTimeHelper.MinValueUtc` / `MaxValueUtc`**, never `DateTime.MinValue` / `MaxValue` (wrong `Kind`). Use `AddOrNullOnOverflow` instead of raw `+`/`-` on boundary arithmetic.
- **`null` means infinity** in `Period(DateTime? Start, DateTime? End)` — null start = started at the beginning of time, null end = never ends. A period contains `instant >= Start && instant < End`, so consecutive periods never overlap.
- Implementation timeline classes are `internal`; `Occurify.csproj` grants `InternalsVisibleTo` to `Occurify.Tests` and `Occurify.Reactive.Tests`.
- Public members need XML docs (`GenerateDocumentationFile=True` + CI's `TreatWarningsAsErrors`).
- Some timelines cache derived state across calls (e.g. `MergeStartTimeline._isResultFull`). If you add caching, add the "called twice" test variants described below.

## Testing

Tests are MSTest, and most are data-driven from JSON using an ASCII timeline notation documented at length in `README.md`:

```
| = instant     < = period start     > = period end     X = start and end at the same instant
```

The pipeline for a typical operation test:

1. `tests/Occurify.Tests/TestCases/<Operation>.json` — array of arrays of cases, keys are **space-padded** (`"source  "`, `"expected"`).
2. `tests/Occurify.Tests/TestCases/Poco/<Operation>.cs` — POCO with matching `[JsonProperty("source  ")]` padding.
3. `tests/Occurify.Tests/<Operation>.Tests.cs` — `TestCaseSource()` reads the JSON with Newtonsoft and feeds `[DynamicData]`.
4. **Register the JSON in `Occurify.Tests.csproj`** with `<CopyToOutputDirectory>Always</CopyToOutputDirectory>`, or the test will fail at runtime with a missing-file error.

`tests/Occurify.Tests/StringHelper/StringTimelineHelper.cs` converts the ASCII strings to and from real timelines; `InstantProviderMethods` (`TimelineMethods.GetPreviousUtcInstant` / `GetNextUtcInstant` / `IsInstant`) selects which of the three `ITimeline` members a case exercises. **Every case should be run against all three**, and against a `methodCalls: 2` variant when the timeline holds state.
