using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
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

            Assert.AreEqual(0, relu.Compute(-1));
            Assert.AreEqual(0, relu.Compute(0));
            Assert.AreEqual(2, relu.Compute(2));

            Assert.AreEqual(0, relu.ComputeDerivative(-1));
            Assert.AreEqual(0, relu.ComputeDerivative(0));
            Assert.AreEqual(1, relu.ComputeDerivative(2));

            Assert.AreEqual(0, relu.CompareTo(new Relu()));
        }
        [TestMethod]
        public void NNSigma()
        {
            //TODO - A - NN should change all act functions at once in mutation?
            Sigma sigma = new Sigma();

            double eps = 1e-7;
            Assert.IsTrue(NNUT.CloseCompare(0.5, sigma.Compute(0), eps));
            Assert.IsTrue(NNUT.CloseCompare(0.268941421, sigma.Compute(-1), eps));
            Assert.IsTrue(NNUT.CloseCompare(0.880797077, sigma.Compute(2), eps));

            Assert.IsTrue(NNUT.CloseCompare(0.25, sigma.ComputeDerivative(0), eps));
            Assert.IsTrue(NNUT.CloseCompare(0.196611933, sigma.ComputeDerivative(-1), eps));
            Assert.IsTrue(NNUT.CloseCompare(0.104993585, sigma.ComputeDerivative(2), eps));

            Assert.AreEqual(0, sigma.CompareTo(new Sigma()));
        }
    }
}
