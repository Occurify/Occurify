namespace Occurify.Astro;

[Flags]
public enum SunPhases
{
    None = 0,
    SolarNoon = 1 << 0,
    Nadir = 1 << 1,
    Sunrise = 1 << 2,
    Sunset = 1 << 3,
    SunriseEnd = 1 << 4,
    SunsetStart = 1 << 5,
    Dawn = 1 << 6,
    Dusk = 1 << 7,
    NauticalDawn = 1 << 8,
    NauticalDusk = 1 << 9,
    NightEnd = 1 << 10,
    Night = 1 << 11,
    GoldenHourEnd = 1 << 12,
    GoldenHour = 1 << 13
}