namespace Occurify.Astro;

/// <summary>
/// Represents a geographic coordinate with latitude, longitude, and optional height.
/// </summary>
/// <param name="Latitude">The latitude in degrees.</param>
/// <param name="Longitude">The longitude in degrees.</param>
/// <param name="Height">Height in meter relative to the horizon (default is 0).</param>
public record Coordinates(double Latitude, double Longitude, double Height = 0)
{
    private static Coordinates? _local;

    /// <summary>
    /// The local <see cref="Coordinates"/> object used by methods that don't take a <see cref="Coordinates"/> object.
    /// Note that this is not set by default.
    /// </summary>
    public static Coordinates Local
    {
        get
        {
            if (_local == null)
            {
                throw new InvalidOperationException("Local Coordinates not set.");
            }
            return _local;
        }
        set => _local = value;
    }
}