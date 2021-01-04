using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgsPart1;

namespace Part1Tests
{
    [TestClass]
    public class InversionTests
    {
        private int[] tenOdd = { 5, 4, 3, 2, 1 };
        private int[] sevOdd = { 5, 1, 2, 3, 0 };
        private int[] oneEven = { 1, 3, 2, 4 };
        private int[] oneEl = { 1 };
        private int[] noEl = { };
        private int[] noInvs = { 1, 2, 3, 4 };

        [TestMethod]
        public void Inversions_Work()
        {
            Assert.AreEqual(0, new Inversions(tenOdd).invCount.CompareTo(10));
            Assert.AreEqual(0, new Inversions(sevOdd).invCount.CompareTo(7));
            Assert.AreEqual(0, new Inversions(oneEven).invCount.CompareTo(1));
        }

        [TestMethod]
        public void Inversions_CornerCases()
        {
            Assert.AreEqual(0, new Inversions(oneEl).invCount.CompareTo(0));
            Assert.AreEqual(0, new Inversions(noEl).invCount.CompareTo(0));
            Assert.AreEqual(0, new Inversions(noInvs).invCount.CompareTo(0));
        }
    }
}