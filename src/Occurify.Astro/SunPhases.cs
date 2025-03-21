namespace Occurify.Astro;

/// <summary>
/// Represents various phases of the sun throughout the day.
/// </summary>
[Flags]
public enum SunPhases
{
    /// <summary>
    /// No specific sun phase.
    /// </summary>
    None = 0,

    /// <summary>
    /// The moment when the sun is at its highest point in the sky.
    /// </summary>
    SolarNoon = 1 << 0,

    /// <summary>
    /// The moment when the sun is at its lowest point below the horizon.
    /// </summary>
    Nadir = 1 << 1,

    /// <summary>
    /// The time when the sun first appears above the horizon.
    /// </summary>
    Sunrise = 1 << 2,

    /// <summary>
    /// The time when the sun completely disappears below the horizon.
    /// </summary>
    Sunset = 1 << 3,

    /// <summary>
    /// The end of the sunrise phase.
    /// </summary>
    SunriseEnd = 1 << 4,

    /// <summary>
    /// The start of the sunset phase.
    /// </summary>
    SunsetStart = 1 << 5,

    /// <summary>
    /// The period of twilight before sunrise.
    /// </summary>
    Dawn = 1 << 6,

    /// <summary>
    /// The period of twilight after sunset.
    /// </summary>
    Dusk = 1 << 7,

    /// <summary>
    /// The start of the nautical twilight before sunrise.
    /// </summary>
    NauticalDawn = 1 << 8,

    /// <summary>
    /// The end of the nautical twilight after sunset.
    /// </summary>
    NauticalDusk = 1 << 9,

    /// <summary>
    /// The time when astronomical night ends before sunrise.
    /// </summary>
    NightEnd = 1 << 10,

    /// <summary>
    /// The time when astronomical night begins after dusk.
    /// </summary>
    Night = 1 << 11,

    /// <summary>
    /// The end of the golden hour after sunrise.
    /// </summary>
    GoldenHourEnd = 1 << 12,

    /// <summary>
    /// The golden hour before sunset, providing soft, warm lighting.
    /// </summary>
    GoldenHour = 1 << 13
}