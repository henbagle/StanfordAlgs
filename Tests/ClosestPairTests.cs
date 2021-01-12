using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgs;

namespace Part1Tests
{
    [TestClass]
    public class ClosestPairTests
    {
        [TestMethod]
        public void SortPairsByXAndY()
        {
            Point[] pairs = { new Point(2, 4), new Point(4, 1), new Point(8, 0), new Point(1, 5) };
            Point[] sortY = { pairs[2], pairs[1], pairs[0], pairs[3] };
            Point[] sortX = { pairs[3], pairs[0], pairs[1], pairs[2] };

            CollectionAssert.AreEqual(sortX, new ClosestPair(pairs).XRanked);
            CollectionAssert.AreEqual(sortY, new ClosestPair(pairs).YRanked);
        }

        [TestMethod]
        public void ClosestPairBaseCase()
        {
            Point[] pairs = { new Point(10, 10), new Point(1, -1), new Point(9, 11) };
            (Point, Point) expected = (pairs[0], pairs[2]);
            (Point, Point) actual = ClosestPair.BaseCase(pairs);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindsClosestPairWhenNotSplit()
        {
            Point[] pairs = { new Point(1, 5), new Point(2, 4), new Point(4, 1), new Point(8, 0) };
            Point[] pairs2 = { new Point(4, 1), new Point(8, 0), pairs[0], pairs[1], new Point(100, 120), new Point(200, 250), new Point(400, 500) };
            (Point, Point) solution = (pairs[0], pairs[1]);
            Assert.AreEqual(solution, new ClosestPair(pairs).FindClosestPair());
            Assert.AreEqual(solution, new ClosestPair(pairs2).FindClosestPair());
        }

        [TestMethod]
        public void FindsClosestPairWhenSplit()
        {
            Point[] pairs = { new Point(2, 4), new Point(4, 1), new Point(8, 0), new Point(1, 5) };
            Point[] pairs2 = { new Point(4, 1), new Point(8, 0), pairs[0], new Point(100, 120), new Point(200, 250), pairs[3], new Point(400, 600), new Point(400, 900), new Point(400, 1000) };
            (Point, Point) solution = (pairs[3], pairs[0]);
            Assert.AreEqual(solution, new ClosestPair(pairs).FindClosestPair());
            Assert.AreEqual(solution, new ClosestPair(pairs2).FindClosestPair());
        }
    }
}