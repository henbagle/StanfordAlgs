using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgs;
using System;

namespace Part1Tests
{
    [TestClass]
    public class KaratsubaTests
    {
        private string[] a = { "1", "3" };
        private string[] b = { "16", "12" };
        private string[] c = { "1", "12" };
        private string[] d = { "4852", "31333" };

        [TestMethod]
        public void SplitIntInHalf_Works()
        {
            CollectionAssert.AreEqual(a, Karatsuba.SplitIntInHalf("13"));
            CollectionAssert.AreEqual(b, Karatsuba.SplitIntInHalf("1612"));
            CollectionAssert.AreEqual(c, Karatsuba.SplitIntInHalf("112"));
            CollectionAssert.AreEqual(d, Karatsuba.SplitIntInHalf("485231333"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Karatsuba.SplitIntInHalf("1"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Karatsuba.SplitIntInHalf(""));
        }

        [TestMethod]
        public void Karatsuba_CornerCases()
        {
            Assert.AreEqual("1", Karatsuba.KaratsubaMultiply("1", "1"));
            Assert.AreEqual("5", Karatsuba.KaratsubaMultiply("5", "1"));
            Assert.AreEqual("30", Karatsuba.KaratsubaMultiply("3", "10"));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => Karatsuba.KaratsubaMultiply("", "1"));
        }

        [TestMethod]
        public void Karatsuba_Works()
        {
            Assert.AreEqual("192", Karatsuba.KaratsubaMultiply("16", "12"));
            Assert.AreEqual("1920", Karatsuba.KaratsubaMultiply("160", "12"));
            Assert.AreEqual("19200", Karatsuba.KaratsubaMultiply("120", "160"));
            Assert.AreEqual("24576", Karatsuba.KaratsubaMultiply("12", "2048"));
            Assert.AreEqual("27040464", Karatsuba.KaratsubaMultiply("132", "204852"));
            Assert.AreEqual("3301376", Karatsuba.KaratsubaMultiply("1612", "2048"));
        }
    }
}