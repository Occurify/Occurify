using Occurify.TimeZones.Extensions;

namespace Occurify.TimeZones.Tests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        private static readonly TimeZoneInfo DutchTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

        [TestMethod]
        public void NullableToTimeZone_ConvertsFromUtc()
        {
            DateTime? utc = new DateTime(2024, 6, 15, 12, 0, 0, DateTimeKind.Utc);

            Assert.AreEqual<DateTime?>(new DateTime(2024, 6, 15, 14, 0, 0), utc.ToTimeZone(DutchTimeZone));
            Assert.AreEqual<DateTime?>(utc.Value.ToLocalTime(), utc.ToLocalTime());
        }

        [TestMethod]
        public void NullableToTimeZone_NullStaysNull()
        {
            DateTime? utc = null;

            Assert.IsNull(utc.ToTimeZone(DutchTimeZone));
            Assert.IsNull(utc.ToLocalTime());
        }
    }
}
