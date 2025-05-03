
namespace Occurify.Astro
{
    /// <summary>
    /// Provides sun related instants and instant timelines.
    /// </summary>
    public static class AstroInstants
    {
        /// <summary>
        /// Returns a <see cref="ITimeline"/> with sunrises on <see cref="Coordinates.Local"/>.
        /// </summary>
        public static ITimeline LocalSunrises => Sunrises(Coordinates.Local);

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with sunsets on <see cref="Coordinates.Local"/>.
        /// </summary>
        public static ITimeline LocalSunsets => Sunsets(Coordinates.Local);

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with sunsets on <paramref name="coordinates"/>.
        /// </summary>
        public static ITimeline Sunsets(Coordinates coordinates) => SunPhases(coordinates, Astro.SunPhases.Sunset);

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with sunrises on <paramref name="coordinates"/>.
        /// </summary>
        public static ITimeline Sunrises(Coordinates coordinates) => SunPhases(coordinates, Astro.SunPhases.Sunrise);

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with the instants of sun phases <paramref name="phases"/> on <paramref name="coordinates"/>.
        /// </summary>
        public static ITimeline SunPhases(Coordinates coordinates, SunPhases phases)
        {
            return new SunPhaseTimeline(coordinates, phases);
        }

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with the instants of sun phases <paramref name="phases"/> on <see cref="Coordinates.Local"/>.
        /// </summary>
        public static ITimeline SunPhases(SunPhases phases)
        {
            return new SunPhaseTimeline(Coordinates.Local, phases);
        }

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with the instants of sun phases <paramref name="phases"/> on <paramref name="coordinates"/>.
        /// </summary>
        public static ITimeline SunPhases(Coordinates coordinates, IEnumerable<SunPhases> phases)
        {
            return new SunPhaseTimeline(coordinates, phases);
        }

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with the instants of sun phases <paramref name="phases"/> on <see cref="Coordinates.Local"/>.
        /// </summary>
        public static ITimeline SunPhases(IEnumerable<SunPhases> phases)
        {
            return new SunPhaseTimeline(Coordinates.Local, phases);
        }

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with the instants of sun phases <paramref name="phases"/> on <paramref name="coordinates"/>.
        /// </summary>
        public static ITimeline SunPhases(Coordinates coordinates, params SunPhases[] phases)
        {
            return new SunPhaseTimeline(coordinates, phases);
        }

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with the instants of sun phases <paramref name="phases"/> on <see cref="Coordinates.Local"/>.
        /// </summary>
        public static ITimeline SunPhases(params SunPhases[] phases)
        {
            return new SunPhaseTimeline(Coordinates.Local, phases);
        }
    }
}
