using Microsoft.VisualStudio.TestTools.UnitTesting;
using StanfordAlgsPart1;

namespace Tests
{
    [TestClass]
    public class BeegNumberTests
    {
        [TestMethod]
        public void BeegNumber_Adds()
        {
            BeegNumber test = new BeegNumber(12);
            BeegNumber add = new BeegNumber("12");
            Assert.AreEqual("12", test.String);
            Assert.AreEqual("12", add.String);
            test.AddTo(add);
            Assert.AreEqual("24", test.String);
            test.AddTo(new BeegNumber("4000"));
            Assert.AreEqual("4024", test.String);
            test.AddTo(new BeegNumber(3));
            Assert.AreEqual("4027", test.String);
        }

        [TestMethod]
        public void BeegNumber_Subtracts()
        {
            BeegNumber test = new BeegNumber(5);

            test.SubtractFrom(new BeegNumber(2));
            Assert.AreEqual("3", test.String);


            test = new BeegNumber(321);
            test.SubtractFrom(new BeegNumber(23));
            Assert.AreEqual("298", test.String);

        }

        [TestMethod]
        public void BeegNumber_Pads()
        {
            BeegNumber test = new BeegNumber(12);

            test.Pad(1);
            Assert.AreEqual("120", test.String);

            test.Pad(0);
            Assert.AreEqual("120", test.String);


            test.Pad(4);
            Assert.AreEqual("1200000", test.String);
        }
    }
}
