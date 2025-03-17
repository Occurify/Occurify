
using Occurify.Extensions;

namespace Occurify.Astro
{
    public static class AstroPeriods
    {
        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting sunrise and ending at sunset on <see cref="Coordinates.Local"/>.
        /// These periods can span more than one day. For example in the polar region.
        /// </summary>
        public static IPeriodTimeline LocalDaytime => Daytime(Coordinates.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting sunset and ending at sunrise on <see cref="Coordinates.Local"/>.
        /// These periods can span more than one day. For example in the polar region.
        /// </summary>
        public static IPeriodTimeline LocalNighttime => NightTime(Coordinates.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting sunrise and ending at sunset on <paramref name="coordinates"/>.
        /// These periods can span more than one day. For example in the polar region.
        /// </summary>
        public static IPeriodTimeline Daytime(Coordinates coordinates) => AstroInstants.Sunrise(coordinates).To(AstroInstants.Sunset(coordinates));

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting sunset and ending at sunrise on <paramref name="coordinates"/>.
        /// These periods can span more than one day. For example in the polar region.
        /// </summary>
        public static IPeriodTimeline NightTime(Coordinates coordinates) => AstroInstants.Sunset(coordinates).To(AstroInstants.Sunrise(coordinates));
    }
}
