using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class UtilityTests
    {
        [TestMethod]
        public void PseudoRandomGeneratorTest()
        {
            PseudoRandom pr = new PseudoRandom(0.2, 0.8, 1);

            Assert.AreEqual(0.2, pr.NextDouble());
            Assert.AreEqual(0.8, pr.NextDouble());
            Assert.AreEqual(1, pr.NextDouble());
            Assert.AreEqual(0.2, pr.NextDouble());
            Assert.AreEqual(0.8, pr.NextDouble());
            Assert.AreEqual(1, pr.NextDouble());
            Assert.AreEqual(0.2, pr.NextDouble());
            Assert.AreEqual(0.8, pr.NextDouble());
            Assert.AreEqual(1, pr.NextDouble());

            Assert.AreEqual(0, pr.Next());
            Assert.AreEqual(0, pr.Next());
            Assert.AreEqual(1, pr.Next());
            Assert.AreEqual(0, pr.Next());
            Assert.AreEqual(0, pr.Next());
            Assert.AreEqual(1, pr.Next());
        }
    }
}
