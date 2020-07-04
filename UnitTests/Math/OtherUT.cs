using GANN.MathAT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.UtilityClasses;

namespace UnitTests.Math
{
    [TestClass]
    public class OtherUT
    {
        [TestMethod]
        public void SampleWithoutReplacementTest()
        {
            PseudoRandom pr = new PseudoRandom(1,1,0, 0);
            var swr = new SampleWithoutReplacement<double>(new double[] { 1, 2.4, 10, 5}, pr);

            Assert.IsTrue(swr.GetNext(out double d));
            Assert.AreEqual(2.4, d);
            Assert.IsTrue(swr.GetNext(out d));
            Assert.AreEqual(10, d);
            Assert.IsTrue(swr.GetNext(out d));
            Assert.AreEqual(1, d);
            Assert.IsTrue(swr.GetNext(out d));
            Assert.AreEqual(5, d);
            Assert.IsFalse(swr.GetNext(out d));

        }
    }
}
