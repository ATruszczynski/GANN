using GANN.NN.GradientStepStrategies;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.NNElems
{
    [TestClass]
    public class GradientStepUT
    {
        [TestMethod]
        public void ConstatnGradientTest()
        {
            ConstantGradientStep gds = new ConstantGradientStep(0.2);

            Assert.AreEqual(0.2, gds.GetStepSize(-231232123));
            Assert.AreEqual(0, gds.CompareTo(new ConstantGradientStep(0.2)));
            Assert.AreEqual(-1, gds.CompareTo(new ConstantGradientStep(0.1)));
            Assert.AreEqual(0, gds.CompareTo(gds.DeepCopy()));
        }
    }
}
