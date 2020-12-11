using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgsPart1;

namespace Tests
{
    [TestClass]
    public class InversionTests
    {
        private int[] inv1 = { 5, 4, 3, 2, 1 };
        private int[] inv2 = { 5, 1, 2, 3, 0 };
        private int[] inv3 = { 1, 3, 2, 4};

        [TestMethod]
        public void Inversions_Work()
        {
            Assert.AreEqual(10, new Inversions(inv1).CountInversions());
            Assert.AreEqual(7, new Inversions(inv2).CountInversions());
            Assert.AreEqual(1, new Inversions(inv3).CountInversions());
        }
    }
}
