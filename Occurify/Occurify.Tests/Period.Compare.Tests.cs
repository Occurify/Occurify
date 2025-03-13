
using Occurify.Tests.Helpers;

namespace Occurify.Tests
{
    [TestClass]
    public class PeriodCompareTests
    {
        [TestMethod]
        public void CompareTo_NullStartComesFirst()
        {
            var utcNow = DateTime.UtcNow;
            var earlier = Period.Create(null, utcNow);
            var later = Period.Create(DateTimeHelper.MinValueUtc, utcNow + TimeSpan.FromDays(1));

            Assert.IsTrue(earlier.CompareTo(later) < 0);
            Assert.IsTrue(later.CompareTo(earlier) > 0);

            Assert.IsTrue(earlier > later);
            Assert.IsTrue(later < earlier);
        }

        [TestMethod]
        public void CompareTo_StartDateComparison()
        {
            var utcNow = DateTime.UtcNow;
            var earlier = Period.Create(utcNow, utcNow + TimeSpan.FromDays(1));
            var later = Period.Create(utcNow + TimeSpan.FromHours(1), utcNow + TimeSpan.FromDays(1));

            Assert.IsTrue(earlier.CompareTo(later) < 0);
            Assert.IsTrue(later.CompareTo(earlier) > 0);

            Assert.IsTrue(earlier > later);
            Assert.IsTrue(later < earlier);
        }

        [TestMethod]
        public void CompareTo_NullEndComesLast()
        {
            var utcNow = DateTime.UtcNow;
            var earlier = Period.Create(utcNow, utcNow + TimeSpan.FromDays(1));
            var later = Period.Create(utcNow, null);

            Assert.IsTrue(earlier.CompareTo(later) < 0);
            Assert.IsTrue(later.CompareTo(earlier) > 0);

            Assert.IsTrue(earlier < later);
            Assert.IsTrue(later > earlier);
        }

        [TestMethod]
        public void CompareTo_SameStartDifferentEnds()
        {
            var utcNow = DateTime.UtcNow;
            var earlier = Period.Create(utcNow, utcNow + TimeSpan.FromHours(1));
            var later = Period.Create(utcNow, utcNow + TimeSpan.FromDays(1));

            Assert.IsTrue(earlier.CompareTo(later) < 0);
            Assert.IsTrue(later.CompareTo(earlier) > 0);

            Assert.IsTrue(earlier < later);
            Assert.IsTrue(later > earlier);
        }

        [TestMethod]
        public void CompareTo_EqualPeriods()
        {
            var utcNow = DateTime.UtcNow;
            var period1 = Period.Create(utcNow, utcNow + TimeSpan.FromHours(1));
            var period2 = Period.Create(utcNow, utcNow + TimeSpan.FromHours(1));

            Assert.AreEqual(0, period1.CompareTo(period2));

            Assert.AreEqual(period1, period2);
            Assert.IsTrue(period1 == period2);
        }

        [TestMethod]
        public void CompareTo_NullComparison()
        {
            var utcNow = DateTime.UtcNow;
            var period = Period.Create(utcNow, utcNow + TimeSpan.FromHours(1));

            Assert.IsTrue(period.CompareTo(null) > 0);
        }

        [TestMethod]
        public void CompareTo_SortingTest()
        {
            var utcNow = DateTime.UtcNow;
            var periods = new List<Period>
        {
            Period.Create(utcNow, utcNow + TimeSpan.FromHours(1)),
            Period.Create(null, utcNow),
            Period.Create(utcNow, null),
            Period.Create(utcNow - TimeSpan.FromDays(1), utcNow)
        };

            periods.Sort();

            Assert.IsNull(periods[0].Start);
            Assert.AreEqual(utcNow - TimeSpan.FromDays(1), periods[1].Start);
            Assert.AreEqual(utcNow, periods[2].Start);
            Assert.AreEqual(utcNow, periods[3].Start);
            Assert.IsNull(periods[3].End);
        }
    }
}
