
using Occurify.Extensions;

namespace Occurify.Astro
{
    /// <summary>
    /// Provides sun related periods and period timelines.
    /// </summary>
    public static class AstroPeriods
    {
        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting sunrise and ending at sunset on <see cref="Coordinates.Local"/>.
        /// These periods can span more than one day. For example in the polar region.
        /// </summary>
        public static IPeriodTimeline LocalDaytimes => Daytimes(Coordinates.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting sunset and ending at sunrise on <see cref="Coordinates.Local"/>.
        /// These periods can span more than one day. For example in the polar region.
        /// </summary>
        public static IPeriodTimeline LocalNighttimes => NightTimes(Coordinates.Local);

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting sunrise and ending at sunset on <paramref name="coordinates"/>.
        /// These periods can span more than one day. For example in the polar region.
        /// </summary>
        public static IPeriodTimeline Daytimes(Coordinates coordinates) => AstroInstants.Sunrises(coordinates).To(AstroInstants.Sunsets(coordinates));

        /// <summary>
        /// Returns a <see cref="IPeriodTimeline"/> with periods starting sunset and ending at sunrise on <paramref name="coordinates"/>.
        /// These periods can span more than one day. For example in the polar region.
        /// </summary>
        public static IPeriodTimeline NightTimes(Coordinates coordinates) => AstroInstants.Sunsets(coordinates).To(AstroInstants.Sunrises(coordinates));
    }
}
