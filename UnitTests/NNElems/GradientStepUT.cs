using GANN.MathAT;
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

            var wu = new MatrixAT1[2];
            var bu = new MatrixAT1[2];

            wu[1] = new MatrixAT1(new double[] { 10, 20 });
            bu[1] = new MatrixAT1(new double[] { 1, 2 });

            (var wg, var bg) =  gds.GetStepSize(-231232123, wu, bu);

            Assert.IsTrue(MatrixAT1.Compare(wg[1], new MatrixAT1(new double[] { 2, 4 })));
            Assert.IsTrue(MatrixAT1.Compare(bg[1], new MatrixAT1(new double[] { 0.2, 0.4 })));
            Assert.AreEqual(2, wg.Length);
            Assert.AreEqual(2, bg.Length);
            Assert.AreEqual(0, gds.CompareTo(new ConstantGradientStep(0.2)));
            Assert.AreEqual(-1, gds.CompareTo(new ConstantGradientStep(0.1)));
            Assert.AreEqual(0, gds.CompareTo(gds.DeepCopy()));

            Assert.IsTrue(MatrixAT1.Compare(wg[1], gds.stepSize * wu[1]));
        }
    }
}
