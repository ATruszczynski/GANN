using GANN.MathAT.Distributions;
using GANN.MathAT.Ranges;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
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
            //TODO - B - fix tests debuggin with exceptions
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

        [TestMethod]
        public void SetRangeRangeTest()
        {
            PseudoRandom pr = new PseudoRandom(0, 1);
            var relu = new Relu();
            var sigma = new Sigma();
            SetRange<ActivationFunction> set = new SetRange<ActivationFunction>(new ActivationFunction[] { relu, sigma }, new UniformDiscreteRangeDistribution(pr, 0,2));

            Assert.AreEqual(0, relu.CompareTo(set.GetNext()));
            Assert.AreEqual(0, sigma.CompareTo(set.GetNext()));
            Assert.IsTrue(set.IsInRange(new Relu()));
            Assert.IsTrue(set.IsInRange(new Sigma()));

            var g1 = new ConstantGradientStep(0.5);
            var g2 = new ConstantGradientStep(1);
            var gSet = new SetRange<GradientStepStrategy>(new GradientStepStrategy[] { g1, g2 }, new UniformDiscreteRangeDistribution(pr, 0, 2));
            
            Assert.AreEqual(0, g1.CompareTo(gSet.GetNext()));
            Assert.AreEqual(0, g2.CompareTo(gSet.GetNext()));
            Assert.IsTrue(gSet.IsInRange(new ConstantGradientStep(0.5)));
            Assert.IsFalse(gSet.IsInRange(new ConstantGradientStep(0.75)));
            Assert.IsFalse(set.IsInRange(new ConstantGradientStep(0.75)));
        }
    }
}
