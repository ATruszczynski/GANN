using GANN.GA.GA_Elements;
using GANN.GA.Operators.CrossoverOperators;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;
using GANN.NN.Parameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.UtilityClasses;

namespace UnitTests.GANNElements
{
    //TODO - A - GA should take care of copying
    //TODO - A - test for deep copying in GA
    [TestClass]
    public class COOPUT
    {
        [TestMethod]
        public void Test()
        {
            NNChromosome nn1 = new NNChromosome(new Hyperparameters(3, 2, new int[] { 10, 12 },
                new ActivationFunction[] { new Relu(), new Sigma() }, new Sigma(), new QuadDiff(666),
                new ConstantGradientStep(777)));

            NNChromosome nn2 = new NNChromosome(new Hyperparameters(3, 2, new int[] { 13, 14, 15 },
                new ActivationFunction[] { new Softmax(), new Relu(), new Softmax() }, new Softmax(), new CrossEntropy(),
                new MomentumStrategy(666, 1)));

            PseudoRandom pr = new PseudoRandom(0.1, 0.1, 2, 1, 0, 1, 0,
                                               0.1, 0.1, 0.1);

            NNBasicCrossoverOperator co = new NNBasicCrossoverOperator();

            (var nn3, var nn4) = co.Crossover(nn1, nn2, pr);

            var hp1 = (nn3 as NNChromosome).Hyperparameters;
            var hp2 = (nn4 as NNChromosome).Hyperparameters;

            Assert.AreEqual(3, hp1.inputSize);
            Assert.AreEqual(2, hp1.outputSize);
            Assert.AreEqual(3, hp1.InternalNeuronCounts.Length);
            Assert.AreEqual(3, hp1.InternalActivationFunctions.Length);
            Assert.AreEqual(13, hp1.InternalNeuronCounts[0]);
            Assert.AreEqual(10, hp1.InternalNeuronCounts[1]);
            Assert.AreEqual(12, hp1.InternalNeuronCounts[2]);
            Assert.AreEqual(0, hp1.InternalActivationFunctions[0].CompareTo(new Softmax()));
            Assert.AreEqual(0, hp1.InternalActivationFunctions[1].CompareTo(new Relu()));
            Assert.AreEqual(0, hp1.InternalActivationFunctions[2].CompareTo(new Softmax()));
            Assert.AreEqual(0, hp1.AggFunc.CompareTo(new Softmax()));
            Assert.AreEqual(0, hp1.LossFunction.CompareTo(new CrossEntropy()));
            Assert.AreEqual(0, hp1.GradientStepStrategy.CompareTo(new MomentumStrategy(666, 1)));

            Assert.AreEqual(3, hp2.inputSize);
            Assert.AreEqual(2, hp2.outputSize);
            Assert.AreEqual(2, hp2.InternalNeuronCounts.Length);
            Assert.AreEqual(2, hp2.InternalActivationFunctions.Length);
            Assert.AreEqual(14, hp2.InternalNeuronCounts[0]);
            Assert.AreEqual(15, hp2.InternalNeuronCounts[1]);
            Assert.AreEqual(0, hp2.InternalActivationFunctions[0].CompareTo(new Relu()));
            Assert.AreEqual(0, hp2.InternalActivationFunctions[1].CompareTo(new Sigma()));
            Assert.AreEqual(0, hp2.AggFunc.CompareTo(new Sigma()));
            Assert.AreEqual(0, hp2.LossFunction.CompareTo(new QuadDiff(666)));
            Assert.AreEqual(0, hp2.GradientStepStrategy.CompareTo(new ConstantGradientStep(777)));


        }
    }
}
