using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgsPart1;

namespace Tests
{
    [TestClass]
    public class ClosestPairTests
    {
        [TestMethod]
        public void SortPairsByXAndY()
        {
            Point[] pairs = { new Point(1, 5), new Point(2, 3), new Point(4, 2), new Point(7, 1) };
            Point[] sortY = { pairs[3], pairs[2], pairs[1], pairs[0] };

            CollectionAssert.AreEqual(pairs, new FindClosestPair(pairs).XRanked);
            CollectionAssert.AreEqual(sortY, new FindClosestPair(pairs).YRanked);
        }

        [TestMethod]
        public void ClosestPairBaseCase()
        {
            Point[] pairs = { new Point(10, 10), new Point(1, -1), new Point(9, 11) };
            (Point, Point) expected = (pairs[0], pairs[2]);
            (Point, Point) actual = new FindClosestPair(pairs).BaseCase(pairs);
            Assert.AreEqual(expected, actual);
        }
    }
}
