using Occurify.Extensions;

namespace Occurify.Tests
{
    [TestClass]
    public class PeriodTimelineCollectionEnumerateTests
    {
        [TestMethod]
        public void Enumerate()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;

            var period1 = Period.Create(utcNow, TimeSpan.FromDays(1));
            var period2 = Period.Create(utcNow.AddDays(1), TimeSpan.FromDays(1));
            var period3 = Period.Create(utcNow.AddDays(1), TimeSpan.FromDays(2));
            var period4 = Period.Create(utcNow.AddDays(2), TimeSpan.FromDays(1));
            var period5 = Period.Create(utcNow.AddDays(3), TimeSpan.FromDays(1));
            var period6 = Period.Create(utcNow.AddDays(3), TimeSpan.FromDays(1));
            var period7 = Period.Create(utcNow.AddDays(5), TimeSpan.FromDays(1));
            var period8 = Period.Create(utcNow.AddDays(6), TimeSpan.FromDays(1));
            var period9 = Period.Create(utcNow.AddDays(7), TimeSpan.FromDays(1));

            var list1 = PeriodTimeline.FromPeriods(period2, period4, period6);
            var list2 = PeriodTimeline.FromPeriods(period3, period5, period7);
            var list3 = PeriodTimeline.FromPeriods(period1, period8, period9);

            var sources = new [] { list1, list2, list3 };

            var expected = new List<Period> { period1, period2, period3, period4, period5, period7, period8, period9 };

            // Act
            var result = sources.Enumerate().ToList();

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EnumerateBackwards()
        {
            // Arrange
            var utcNow = DateTime.UtcNow;

            var period1 = Period.Create(utcNow, TimeSpan.FromDays(1));
            var period2 = Period.Create(utcNow.AddDays(1), TimeSpan.FromDays(1));
            var period3 = Period.Create(utcNow.AddDays(1), TimeSpan.FromDays(2));
            var period4 = Period.Create(utcNow.AddDays(2), TimeSpan.FromDays(1));
            var period5 = Period.Create(utcNow.AddDays(3), TimeSpan.FromDays(1));
            var period6 = Period.Create(utcNow.AddDays(3), TimeSpan.FromDays(1));
            var period7 = Period.Create(utcNow.AddDays(5), TimeSpan.FromDays(1));
            var period8 = Period.Create(utcNow.AddDays(6), TimeSpan.FromDays(1));
            var period9 = Period.Create(utcNow.AddDays(7), TimeSpan.FromDays(1));

            var list1 = PeriodTimeline.FromPeriods(period2, period4, period6);
            var list2 = PeriodTimeline.FromPeriods(period3, period5, period7);
            var list3 = PeriodTimeline.FromPeriods(period1, period8, period9);

            var sources = new[] { list1, list2, list3 };

            var expected = new List<Period> { period9, period8, period7, period5, period4, period3, period2, period1 };

            // Act
            var result = sources.EnumerateBackwards().ToList();

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }
    }
}
