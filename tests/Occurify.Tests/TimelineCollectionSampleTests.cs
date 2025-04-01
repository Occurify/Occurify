using Occurify.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Occurify.Tests
{
    [TestClass]
    public class TimelineCollectionSampleTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            // Arrange
            const int timeGap1 = 42;
            const int timeGap2 = 1337;
            const int timeGap3 = timeGap1 + timeGap2;
            const int timeGap4 = timeGap2 - timeGap1;

            var now = DateTime.UtcNow;

            var time1 = now + TimeSpan.FromTicks(timeGap1);
            var time2 = now + TimeSpan.FromTicks(timeGap1 + timeGap2);
            var time3 = now + TimeSpan.FromTicks(timeGap1 + timeGap2 + timeGap3);
            var time4 = now + TimeSpan.FromTicks(timeGap1 + timeGap2 + timeGap3 + timeGap4);

            var timeline1 = Timeline.FromInstants(time1, time3);
            var timeline2 = Timeline.FromInstants(time2, time4);

            // Act
            var sample = new TimelineCollectionSample(time3, new Dictionary<ITimeline, TimelineSample>
            {
                {timeline1, new TimelineSample(time3, true, time1, null)},
                {timeline2, new TimelineSample(time3, false, time2, time4)}
            });

            // Assert
            Assert.AreEqual(time3, sample.UtcSampleInstant);
            Assert.AreEqual(time2, sample.Previous);
            Assert.AreEqual(time4, sample.Next);
            CollectionAssert.AreEqual(new[] { timeline1 }, sample.TimelinesWithInstantOnSampleLocation.ToArray());
            CollectionAssert.AreEqual(new[] { timeline2 }, sample.TimelinesOnPrevious.ToArray());
            CollectionAssert.AreEqual(new[] { timeline2 }, sample.TimelinesOnNext.ToArray());
        }
    }
}
