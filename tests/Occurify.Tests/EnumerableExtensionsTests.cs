using Occurify.Extensions;

namespace Occurify.Tests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void CombineOrderedEnumerables()
        {
            // Arrange
            var list1 = new List<int> { 1, 3, 5 };
            var list2 = new List<int> { 2, 3, 6 };
            var list3 = new List<int> { 0, 7, 8 };

            var sources = new List<List<int>> { list1, list2, list3 };

            var expected = new List<int> { 0, 1, 2, 3, 5, 6, 7, 8 };

            // Act
            var result = sources.CombineOrderedEnumerables().ToList();

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CombineOrderedEnumerables_Descending()
        {
            // Arrange
            var list1 = new List<int> { 5, 3, 1 };
            var list2 = new List<int> { 6, 3, 2 };
            var list3 = new List<int> { 8, 7, 0 };

            var sources = new List<List<int>> { list1, list2, list3 };

            var expected = new List<int> { 8, 7, 6, 5, 3, 2, 1, 0 };

            // Act
            var result = sources.CombineOrderedEnumerables(descending: true).ToList();

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CombineEnumerables_OnlyCallsMoveNextWhenNeeded()
        {
            var source1 = new TrackingEnumerable<int>([1, 4, 7]);
            var source2 = new TrackingEnumerable<int>([2, 5, 8]);
            var source3 = new TrackingEnumerable<int>([3, 6, 9]);

            var sources = new IEnumerable<int>[] { source1, source2, source3 };

            var enumerator = sources.CombineOrderedEnumerables().GetEnumerator();

            Assert.AreEqual(0, source1.MoveNextCount);
            Assert.AreEqual(0, source2.MoveNextCount);
            Assert.AreEqual(0, source3.MoveNextCount);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(1, enumerator.Current);

            // We expect MoveNext to have been called 3 times — once per source
            Assert.AreEqual(1, source1.MoveNextCount);
            Assert.AreEqual(1, source2.MoveNextCount);
            Assert.AreEqual(1, source3.MoveNextCount);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(2, enumerator.Current);

            // Only the enumerator with the previously smallest value should have called MoveNext again
            Assert.AreEqual(2, source1.MoveNextCount);
            Assert.AreEqual(1, source2.MoveNextCount);
            Assert.AreEqual(1, source3.MoveNextCount);

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(3, enumerator.Current);

            // Only the enumerator with the previously smallest value should have called MoveNext again
            Assert.AreEqual(2, source1.MoveNextCount);
            Assert.AreEqual(2, source2.MoveNextCount);
            Assert.AreEqual(1, source3.MoveNextCount);

            enumerator.Dispose();
        }

        private class TrackingEnumerable<T> : IEnumerable<T>
        {
            private readonly List<T> _values;
            public int MoveNextCount { get; private set; } = 0;

            public TrackingEnumerable(IEnumerable<T> values)
            {
                _values = values.ToList();
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new TrackingEnumerator<T>(_values, this);
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

            private class TrackingEnumerator<T> : IEnumerator<T>
            {
                private readonly List<T> _values;
                private readonly TrackingEnumerable<T> _parent;
                private int _index = -1;

                public TrackingEnumerator(List<T> values, TrackingEnumerable<T> parent)
                {
                    _values = values;
                    _parent = parent;
                }

                public T Current => _values[_index];

                object System.Collections.IEnumerator.Current => Current;

                public bool MoveNext()
                {
                    _parent.MoveNextCount++;
                    _index++;
                    return _index < _values.Count;
                }

                public void Reset() => _index = -1;
                public void Dispose() { }
            }
        }
    }
}
