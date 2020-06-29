using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using GANN.MathAT.Distributions;

namespace UnitTests
{
    [TestClass]
    public class DistUT
    {
        //TODO - B test close enough function
        [TestMethod]
        public void UniContDistTest()
        {
            PseudoRandom pr = new PseudoRandom(0.5, 0);

            UniformContinuousDistribution ucd = new UniformContinuousDistribution(pr, -5, -2);

            Assert.AreEqual(-3.5, ucd.GetNext());
            Assert.AreEqual(-5, ucd.GetNext());

            ucd = new UniformContinuousDistribution(pr, -2, 4);

            Assert.AreEqual(1, ucd.GetNext());
            Assert.AreEqual(-2, ucd.GetNext());
        }

        [TestMethod]
        public void UniDisctDistTest()
        {
            PseudoRandom pr = new PseudoRandom(-4, -3);

            var ucd = new UniformDiscreteDistribution(pr, -5, -2);

            Assert.AreEqual(-4, ucd.GetNext());
            Assert.AreEqual(-3, ucd.GetNext());

            pr = new PseudoRandom(-2, 2);
            ucd = new UniformDiscreteDistribution(pr, -2, 4);

            Assert.AreEqual(-2, ucd.GetNext());
            Assert.AreEqual(2, ucd.GetNext());
        }

        [TestMethod]
        public void CustDiscDistTest()
        {
            PseudoRandom pr = new PseudoRandom(0.0, 0.25, 0.5, 0.75);

            var ucd = new CustomDiscreteDistribution(pr, new double[] { 1, 2, 3, 4 }, new double[] { 1, 2, 2, 1 });

            Assert.AreEqual(1, ucd.GetNext());
            Assert.AreEqual(2, ucd.GetNext());
            Assert.AreEqual(3, ucd.GetNext());
            Assert.AreEqual(3, ucd.GetNext());

            ucd = new CustomDiscreteDistribution(pr, new double[] { 1, 2, 3, 4 }, new double[] { 1, 3, 4, 5 });

            Assert.AreEqual(1, ucd.GetNext());
            Assert.AreEqual(2, ucd.GetNext());
            Assert.AreEqual(3, ucd.GetNext());
            Assert.AreEqual(4, ucd.GetNext());
        }
    }
}
