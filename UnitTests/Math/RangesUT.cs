using GANN.MathAT.Distributions;
using GANN.MathAT.Ranges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.UtilityClasses;

namespace UnitTests.Math
{
    [TestClass]
    public class RangesUT
    {
        [TestMethod]
        public void UniformContinuousRangeTest()
        {
            //TODO - C - randoms could consistnetlly appear as last arguemnt?
            var pr = new PseudoRandom(0.5, 0.75);
            ContinuousRange c = new ContinuousRange(new UniformContinuousRangeDistribution(pr, -1, 2));

            Assert.IsTrue(c.IsInRange(1.723343));
            Assert.IsTrue(c.IsInRange(-1d));
            Assert.IsTrue(c.IsInRange(-0.999999));
            Assert.IsTrue(c.IsInRange(1.999999));
            Assert.IsFalse(c.IsInRange(2d));
            Assert.AreEqual(0.5, c.GetNext());
            Assert.AreEqual(1.25, c.GetNext()); 
        }

        [TestMethod]
        public void UniformDiscreteRangeTest()
        {
            //TODO - C - randoms could consistnetlly appear as last arguemnt?
            var pr = new PseudoRandom(-1, 1);
            DiscreteRange c = new DiscreteRange(new UniformDiscreteRangeDistribution(pr, -1, 2));

            Assert.AreEqual(-1, c.GetNext());
            Assert.AreEqual(1, c.GetNext());
            Assert.IsFalse(c.IsInRange(2));
            Assert.IsTrue(c.IsInRange(-1));
            Assert.IsTrue(c.IsInRange(0));
            Assert.IsFalse(c.IsInRange(1.723343));
            Assert.IsTrue(c.IsInRange(-1d));
            Assert.IsFalse(c.IsInRange(2d));
            Assert.IsFalse(c.IsInRange(-0.999999));
            Assert.IsFalse(c.IsInRange(1.999999));
        }
    }
}
