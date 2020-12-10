using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgsPart1;

namespace Tests
{
    [TestClass]
    public class SearchTests
    {
        private int[] sortedArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16, 17, 18, 19, 20 };

        [TestMethod]
        public void BinarySearch_Works()
        {
            Assert.AreEqual(0, Search.BinarySearch(1, sortedArr));
            Assert.AreEqual(4, Search.BinarySearch(5, sortedArr));
            Assert.AreEqual(5, Search.BinarySearch(6, sortedArr));
            Assert.AreEqual(13, Search.BinarySearch(14, sortedArr));
            Assert.AreEqual(-1, Search.BinarySearch(0, sortedArr));
            Assert.AreEqual(-1, Search.BinarySearch(22, sortedArr));
            Assert.AreEqual(-1, Search.BinarySearch(15, sortedArr));
        }
    }
}
