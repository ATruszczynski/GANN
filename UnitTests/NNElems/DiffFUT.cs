using GANN.MathAT;
using GANN.NN.LossFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.NNElems
{
    [TestClass]
    public class DiffFUT
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
    }
}
