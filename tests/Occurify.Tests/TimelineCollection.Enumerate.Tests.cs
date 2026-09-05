using Occurify.Extensions;

namespace Occurify.Tests;

[TestClass]
public class TimelineCollectionEnumerateTests
{
    private static DateTime Utc(int day) => new(2025, 1, day, 0, 0, 0, DateTimeKind.Utc);

    [TestMethod]
    public void EnumerateBackwardsFrom_ReturnsLatestFirst()
    {
        var timelines = new[]
        {
            Timeline.FromInstants(Utc(1), Utc(3), Utc(5)),
            Timeline.FromInstants(Utc(2), Utc(4))
        };

        var result = timelines.EnumerateBackwardsFrom(Utc(4)).ToArray();

        CollectionAssert.AreEqual(new[] { Utc(3), Utc(2), Utc(1) }, result);
    }
}
