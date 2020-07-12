using GANN.MathAT;
using GANN.NN.LossFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.NNElems
{
    [TestClass]
    public class LossFUT
    {
        [TestMethod]
        public void QuadDiffTest()
        {
            QuadDiff qd = new QuadDiff(2);

            MatrixAT1 ar1 = new MatrixAT1( new double[] { 1, 0, 1 });
            MatrixAT1 ar2 = new MatrixAT1(new double[] { 1, 2, 0 });

            Assert.AreEqual(10, qd.Compute(ar1, ar2));
            Assert.AreEqual(4, qd.ComputeDerivative(2, 1));
            Assert.AreEqual(0, qd.CompareTo(qd.DeepCopy()));
            Assert.AreEqual(0, qd.CompareTo(new QuadDiff(2)));
        }

        [TestMethod]
        public void CrossEntropyTest()
        {
            CrossEntropy ce = new CrossEntropy();
            var expected = new MatrixAT1(new double[,] { { 0 }, { 1 }, { 0 } });
            var output = new MatrixAT1(new double[,] { { 0.15 }, { 0.6 }, { 0.25 } });
            var cee = ce.Compute(output, expected);
            var ceed = ce.ComputeDerivative(output[1,0], expected[1,0]);
            Assert.IsTrue(NNUT.CloseCompare(cee, 0.510826, 1e-4));
            Assert.IsTrue(NNUT.CloseCompare(ceed, -1.66666, 1e-4));

            expected = new MatrixAT1(new double[,] { { 0 }, { 0 }, { 1 } });
            output = new MatrixAT1(new double[,] { { 0.15 }, { 0.6 }, { 0.25 } });
            cee = ce.Compute(output, expected);
            ceed = ce.ComputeDerivative(output[2, 0], expected[2, 0]);
            Assert.IsTrue(NNUT.CloseCompare(cee, 1.38629, 1e-4));
            Assert.IsTrue(NNUT.CloseCompare(ceed, -4, 1e-4));

            Assert.AreEqual(0, ce.CompareTo(new CrossEntropy()));
            Assert.AreEqual(int.MinValue, ce.CompareTo(new QuadDiff()));
            Assert.AreEqual(0, ce.DeepCopy().CompareTo(new CrossEntropy()));
            Assert.AreEqual(0, ce.CompareTo(ce.DeepCopy()));

            Assert.AreEqual("CE", ce.ToString());
        }
    }
}
