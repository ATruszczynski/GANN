using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.UtilityClasses;
using static UnitTests.UtilityClasses.TestUtility;

namespace UnitTests.TestUtilityTests
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

        [TestMethod]
        public void ArrayCompareTest()
        {
            double[] a1 = new double[] { 0, 1, 2.5, 6.74 };
            double[] a2 = new double[] { 0, 1, 2.5, 6.74, 45 };
            double[] a3 = new double[] { 0, 1, 2.5, 6.74 };
            double[] a4 = new double[] { 0, 1, 2.5, 6 };

            Assert.IsFalse(CompareArrays(a1, a2));
            Assert.IsTrue(CompareArrays(a1, a3));
            Assert.IsFalse(CompareArrays(a1, a4));
        }

        [TestMethod]
        public void ArrayCompareTest2()
        {
            double[] a1 = new double[] { 0, 1, 2.5, 6.74 };

            Assert.IsFalse(CompareArrays(a1, 0, 1, 2.5, 6.74, 45));
            Assert.IsTrue(CompareArrays(a1, 0, 1, 2.5, 6.74));
            Assert.IsFalse(CompareArrays(a1, 0, 1, 2.5, 6));
        }
    }
}
