
namespace Occurify.Astro
{
    public static class AstroInstants
    {
        /// <summary>
        /// Returns a <see cref="ITimeline"/> with sunrises on <see cref="Coordinates.Local"/>.
        /// </summary>
        public static ITimeline LocalSunrise => Sunrise(Coordinates.Local);

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with sunsets on <see cref="Coordinates.Local"/>.
        /// </summary>
        public static ITimeline LocalSunset => Sunset(Coordinates.Local);

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with sunsets on <paramref name="coordinates"/>.
        /// </summary>
        public static ITimeline Sunset(Coordinates coordinates) => SunPhases(coordinates, Astro.SunPhases.Sunset);

        /// <summary>
        /// Returns a <see cref="ITimeline"/> with sunrises on <paramref name="coordinates"/>.
        /// </summary>
        public static ITimeline Sunrise(Coordinates coordinates) => SunPhases(coordinates, Astro.SunPhases.Sunrise);

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
