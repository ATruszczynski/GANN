using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.MathAT;
using GANN.NN;
using GANN.NN.ActivationFunctions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.UtilityClasses;

namespace UnitTests.NNElems
{
    [TestClass]
    public class ActiFuncUT
    {
        [TestMethod]
        public void NNRelu()
        {
            Relu relu = new Relu();

            var zs = new MatrixAT1(new double[,] { { -1 }, { 0 }, { 2 } });

            Assert.AreEqual(0, relu.Compute(0, zs));
            Assert.AreEqual(0, relu.Compute(1, zs));
            Assert.AreEqual(2, relu.Compute(2, zs));

            Assert.AreEqual(0, relu.ComputeDerivative(0, zs));
            Assert.AreEqual(0, relu.ComputeDerivative(1, zs));
            Assert.AreEqual(1, relu.ComputeDerivative(2, zs));

            Assert.AreEqual(0, relu.CompareTo(new Relu()));
        }
        [TestMethod]
        public void NNSigma()
        {
            //TODO - A - NN should change all act functions at once in mutation?

            var zs = new MatrixAT1(new double[,] { { 0 }, { -1 }, { 2 } });
            Sigma sigma = new Sigma();

            double eps = 1e-7;
            Assert.IsTrue(NNUT.CloseCompare(0.5, sigma.Compute(0, zs), eps));
            Assert.IsTrue(NNUT.CloseCompare(0.268941421, sigma.Compute(1, zs), eps));
            Assert.IsTrue(NNUT.CloseCompare(0.880797077, sigma.Compute(2, zs), eps));

            Assert.IsTrue(NNUT.CloseCompare(0.25, sigma.ComputeDerivative(0, zs), eps));
            Assert.IsTrue(NNUT.CloseCompare(0.196611933, sigma.ComputeDerivative(1, zs), eps));
            Assert.IsTrue(NNUT.CloseCompare(0.104993585, sigma.ComputeDerivative(2, zs), eps));

            Assert.AreEqual(0, sigma.CompareTo(new Sigma()));
        }
    }
}
