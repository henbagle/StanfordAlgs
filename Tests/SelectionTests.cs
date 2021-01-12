using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgs;

namespace Part1Tests
{
    [TestClass]
    public class SelectionTests
    {
        private int[] sortedArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        private int[] reverseArr = { 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        private int[] notSortedArr = { 15, 8, 5, 12, 10, 1, 16, 9, 11, 7, 20, 3, 2, 6, 17, 18, 4, 13, 14, 19 };
        private int[] equalArr = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        private int[] negativeArr = { -1, 0, 5, -10, 20, 13, -7, 3, 2, -3 };
        private int[] negativeArrSorted = { -10, -7, -3, -1, 0, 2, 3, 5, 13, 20 };

        [TestMethod]
        public void RandomizedSelection_Works()
        {
            Assert.AreEqual(13, RandomizedSelection.Select(sortedArr, 13));
            Assert.AreEqual(13, RandomizedSelection.Select(reverseArr, 13));
            Assert.AreEqual(20, RandomizedSelection.Select(reverseArr, 20));
            Assert.AreEqual(20, RandomizedSelection.Select(sortedArr, 20));
            Assert.AreEqual(1, RandomizedSelection.Select(sortedArr, 1));
            Assert.AreEqual(0, RandomizedSelection.Select(negativeArr, 5));
        }

        [TestMethod]
        public void DeterministicSelection_Works()
        {
            Assert.AreEqual(13, DeterministicSelection.Select(reverseArr, 13));
            Assert.AreEqual(13, DeterministicSelection.Select(sortedArr, 13));
            Assert.AreEqual(20, DeterministicSelection.Select(reverseArr, 20));
            Assert.AreEqual(20, DeterministicSelection.Select(sortedArr, 20));
            Assert.AreEqual(1, DeterministicSelection.Select(sortedArr, 1));
            Assert.AreEqual(0, DeterministicSelection.Select(negativeArr, 5));
        }
    }
}