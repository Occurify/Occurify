using Occurify.Extensions;
using SunCalcNet;
using SunCalcNet.Model;

namespace Occurify.Astro;

internal class SunPhaseTimeline : Timeline
{
    private readonly Coordinates _coordinates;
    private readonly string[] _phases;

    private readonly Dictionary<SunPhases, SunPhaseName> _sunPhaseLookup = new()
    {
        { SunPhases.SolarNoon, SunPhaseName.SolarNoon },
        { SunPhases.Nadir, SunPhaseName.Nadir },
        { SunPhases.Sunrise, SunPhaseName.Sunrise },
        { SunPhases.Sunset, SunPhaseName.Sunset },
        { SunPhases.SunriseEnd, SunPhaseName.SunriseEnd },
        { SunPhases.SunsetStart, SunPhaseName.SunsetStart },
        { SunPhases.Dawn, SunPhaseName.Dawn },
        { SunPhases.Dusk, SunPhaseName.Dusk },
        { SunPhases.NauticalDawn, SunPhaseName.NauticalDawn },
        { SunPhases.NauticalDusk, SunPhaseName.NauticalDusk },
        { SunPhases.NightEnd, SunPhaseName.NightEnd },
        { SunPhases.Night, SunPhaseName.Night },
        { SunPhases.GoldenHourEnd, SunPhaseName.GoldenHourEnd },
        { SunPhases.GoldenHour, SunPhaseName.GoldenHour }
    };

    internal SunPhaseTimeline(Coordinates coordinates, SunPhases phases)
    {
        _coordinates = coordinates;
        _phases = _sunPhaseLookup.Where(kvp => phases.HasFlag(kvp.Key)).Select(kvp => kvp.Value.Value).ToArray();
    }

    internal SunPhaseTimeline(Coordinates coordinates, IEnumerable<SunPhases> phases)
    {
        _coordinates = coordinates;
        _phases = _sunPhaseLookup.Where(kvp => phases.Any(p => p.HasFlag(kvp.Key))).Select(kvp => kvp.Value.Value).ToArray();
    }

    internal SunPhaseTimeline(Coordinates coordinates, params SunPhases[] phases)
    {
        _coordinates = coordinates;
        _phases = _sunPhaseLookup.Where(kvp => phases.Any(p => p.HasFlag(kvp.Key))).Select(kvp => kvp.Value.Value).ToArray();
    }
    
    public override DateTime? GetPreviousUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        if (!_phases.Any())
        {
            return null;
        }

        SunPhase? best = null;
        var bestGap = TimeSpan.Zero;
        var continueFor = 1; // After finding a result, we continue for one more day.
        var currentDate = utcRelativeTo;

        while (continueFor >= 0)
        {
            var sunPhases = GetSunPhases(currentDate)?.ToArray();
            if (sunPhases == null)
            {
                return null;
            }

            foreach (var phase in sunPhases)
            {
                if (phase.PhaseTime >= utcRelativeTo)
                {
                    continue;
                }

                var currentGap = utcRelativeTo - phase.PhaseTime;
                if (best == null || currentGap < bestGap)
                {
                    best = phase;
                    bestGap = currentGap;
                }
            }

            if (best != null)
            {
                continueFor--;
            }

            var previousDate = currentDate.AddOrNullOnOverflow(-TimeSpan.FromDays(1));
            if (previousDate == null)
            {
                break;
            }

            currentDate = previousDate.Value;
        }

        return best?.PhaseTime;
    }

    public override DateTime? GetNextUtcInstant(DateTime utcRelativeTo)
    {
        if (utcRelativeTo.Kind != DateTimeKind.Utc)
        {
            throw new ArgumentException($"{nameof(utcRelativeTo)} should be UTC time.");
        }

        if (!_phases.Any())
        {
            return null;
        }

        SunPhase? best = null;
        var bestGap = TimeSpan.Zero;
        var continueFor = 1; // After finding a result, we continue for one more day.
        var currentDate = utcRelativeTo;

        while (continueFor >= 0)
        {
            var sunPhases = GetSunPhases(currentDate)?.ToArray();
            if (sunPhases == null)
            {
                return null;
            }

            foreach (var phase in sunPhases)
            {
                if (phase.PhaseTime <= utcRelativeTo)
                {
                    continue;
                }

                var currentGap = phase.PhaseTime - utcRelativeTo;
                if (best == null || currentGap < bestGap)
                {
                    best = phase;
                    bestGap = currentGap;
                }
            }

            if (best != null)
            {
                continueFor--;
            }

            var nextDate = currentDate.AddOrNullOnOverflow(TimeSpan.FromDays(1));
            if (nextDate == null)
            {
                break;
            }

            currentDate = nextDate.Value;
        }

        return best?.PhaseTime;
    }

    public override bool IsInstant(DateTime utcDateTime)
    {
        if (!_phases.Any())
        {
            return false;
        }
        var sunPhases = GetSunPhases(utcDateTime)?.ToArray();
        return sunPhases != null && sunPhases.Any(sp => sp.PhaseTime == utcDateTime);
    }

    private IEnumerable<SunPhase>? GetSunPhases(DateTime date)
    {
        try
        {
            return SunCalc.GetSunPhases(date, _coordinates.Latitude, _coordinates.Longitude, height: _coordinates.Height)
                .Where(sp => _phases.Contains(sp.Name.Value, StringComparer.InvariantCultureIgnoreCase));
        }
        catch (ArgumentOutOfRangeException)
        {
            // This tends to happen in SunCalcNet when nearing DateTime.MinValue or DateTime.MaxValue.
            return null;
        }
        catch (OverflowException)
        {
            // This tends to happen in SunCalcNet when nearing DateTime.MinValue or DateTime.MaxValue.
            return null;
        }
    }
}