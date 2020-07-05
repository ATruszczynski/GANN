using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;
using GANN.NN.Parameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.NNElems
{
    [TestClass]
    public class HyperparametersUT
    {
        [TestMethod]
        public void HPConstructorDefault()
        {
            Hyperparameters hp = new Hyperparameters(2, 3);

            Assert.AreEqual(2, hp.inputSize);
            Assert.AreEqual(3, hp.outputSize);
            Assert.AreEqual(0, hp.internalNeuronCounts.Length);
            Assert.AreEqual(0, hp.meanW);
            Assert.AreEqual(1, hp.stdW);
            Assert.AreEqual(2, hp.LayerCount);
            Assert.AreEqual(0, hp.InternalActivationFunctions.Length);
            Assert.AreEqual(0, hp.AggFunc.CompareTo(new Sigma()));
            Assert.AreEqual(0, hp.LossFunction.CompareTo(new QuadDiff()));
            Assert.AreEqual(0, hp.GradientStepStrategy.CompareTo(new ConstantGradientStep()));
        }

        [TestMethod]
        public void HPFullConstructor()
        {
            Hyperparameters hp = new Hyperparameters
                (
                    2, 3, new int[] { 2 }, 2, 0.5,
                    new ActivationFunction[] { new Sigma() },
                    new Relu(), new QuadDiff(2),
                    new ConstantGradientStep(0.666)
                );

            Assert.AreEqual(2, hp.inputSize);
            Assert.AreEqual(3, hp.outputSize);
            Assert.AreEqual(1, hp.internalNeuronCounts.Length);
            Assert.AreEqual(2, hp.internalNeuronCounts[0]);
            Assert.AreEqual(2, hp.meanW);
            Assert.AreEqual(0.5, hp.stdW);
            Assert.AreEqual(3, hp.LayerCount);
            Assert.AreEqual(1, hp.InternalActivationFunctions.Length);
            Assert.AreEqual(0, hp.InternalActivationFunctions[0].CompareTo(new Sigma()));
            Assert.AreEqual(0, hp.AggFunc.CompareTo(new Relu()));
            Assert.AreEqual(0, hp.LossFunction.CompareTo(new QuadDiff(2)));
            Assert.AreEqual(0, hp.GradientStepStrategy.CompareTo(new ConstantGradientStep(0.666)));
        }

        [TestMethod]
        public void HPPartConstructor()
        {
            Hyperparameters hp = new Hyperparameters
                (
                    2, 3, new int[] { 2 }
                );

            Assert.AreEqual(2, hp.inputSize);
            Assert.AreEqual(3, hp.outputSize);
            Assert.AreEqual(1, hp.internalNeuronCounts.Length);
            Assert.AreEqual(2, hp.internalNeuronCounts[0]);
            Assert.AreEqual(0, hp.meanW);
            Assert.AreEqual(1, hp.stdW);
            Assert.AreEqual(3, hp.LayerCount);
            Assert.AreEqual(1, hp.InternalActivationFunctions.Length);
            Assert.AreEqual(0, hp.InternalActivationFunctions[0].CompareTo(new Relu()));
            Assert.AreEqual(0, hp.AggFunc.CompareTo(new Sigma()));
            Assert.AreEqual(0, hp.LossFunction.CompareTo(new QuadDiff()));
            Assert.AreEqual(0, hp.GradientStepStrategy.CompareTo(new ConstantGradientStep()));
        }
    }
}
