using GANN.GA.GA_Elements;
using GANN.GA.Operators.MutationOperators;
using GANN.MathAT.Distributions;
using GANN.MathAT.Ranges;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;
using GANN.NN.ParameterRanges;
using GANN.NN.Parameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.UtilityClasses;

namespace UnitTests.GANNElements
{
    [TestClass]
    public class MOOPUT
    {
        //TODO - A - upgrade ranges' interafaces
        [TestMethod]
        public void TestAddLayer()
        {
            PseudoRandom random = new PseudoRandom(0, 0, 10, 1);
            var HypereparametersRange = Get(random);
            NNChromosome nnc = new NNChromosome(new Hyperparameters(3, 2, new int[] { 4 }));

            NNBasicMutationOperator nnbmo = new NNBasicMutationOperator(HypereparametersRange);

            var resCh = nnbmo.Mutate(nnc, random);

            Hyperparameters hp = (resCh as NNChromosome).Hyperparameters;

            Assert.AreEqual(2, hp.InternalNeuronCounts.Length);
            Assert.AreEqual(10, hp.InternalNeuronCounts[0]);
            Assert.AreEqual(4, hp.InternalNeuronCounts[1]);
            Assert.AreEqual(2, hp.InternalActivationFunctions.Length);
            Assert.AreEqual(0, hp.InternalActivationFunctions[0].CompareTo(new Sigma()));
            Assert.AreEqual(0, hp.InternalActivationFunctions[1].CompareTo(new Relu()));
        }

        [TestMethod]
        public void TestRemoveLayer()
        {
            PseudoRandom random = new PseudoRandom(0, 1);
            var HypereparametersRange = Get(random);
            NNChromosome nnc = new NNChromosome(new Hyperparameters(3, 2, new int[] { 4, 5, 6 }, intActFuns: new ActivationFunction[] { new Softmax(), new Relu(), new Sigma() }));

            NNBasicMutationOperator nnbmo = new NNBasicMutationOperator(HypereparametersRange);

            var resCh = nnbmo.Mutate(nnc, random);

            Hyperparameters hp = (resCh as NNChromosome).Hyperparameters;

            Assert.AreEqual(2, hp.InternalNeuronCounts.Length);
            Assert.AreEqual(4, hp.InternalNeuronCounts[0]);
            Assert.AreEqual(6, hp.InternalNeuronCounts[1]);
            Assert.AreEqual(2, hp.InternalActivationFunctions.Length);
            Assert.AreEqual(0, hp.InternalActivationFunctions[0].CompareTo(new Softmax()));
            Assert.AreEqual(0, hp.InternalActivationFunctions[1].CompareTo(new Sigma()));
        }

        [TestMethod]
        public void TestChangeNeuronCount()
        {
            PseudoRandom random = new PseudoRandom(2, 1, 0.1, 2);
            var HypereparametersRange = Get(random);
            NNChromosome nnc = new NNChromosome(new Hyperparameters(3, 2, new int[] { 4, 6 }, intActFuns: new ActivationFunction[] { new Softmax(), new Relu()}));

            NNBasicMutationOperator nnbmo = new NNBasicMutationOperator(HypereparametersRange);

            var resCh = nnbmo.Mutate(nnc, random);

            Hyperparameters hp = (resCh as NNChromosome).Hyperparameters;

            Assert.AreEqual(2, hp.InternalNeuronCounts.Length);
            Assert.AreEqual(4, hp.InternalNeuronCounts[0]);
            Assert.AreEqual(2, hp.InternalNeuronCounts[1]);
            Assert.AreEqual(2, hp.InternalActivationFunctions.Length);
            Assert.AreEqual(0, hp.InternalActivationFunctions[0].CompareTo(new Softmax()));
            Assert.AreEqual(0, hp.InternalActivationFunctions[1].CompareTo(new Relu()));
        }

        [TestMethod]
        public void TestChangeActFun()
        {
            PseudoRandom random = new PseudoRandom(3, 0.6, 1);
            var HypereparametersRange = Get(random);
            NNChromosome nnc = new NNChromosome(new Hyperparameters(3, 2, new int[] { 4, 6 }));

            NNBasicMutationOperator nnbmo = new NNBasicMutationOperator(HypereparametersRange);

            var resCh = nnbmo.Mutate(nnc, random);

            Hyperparameters hp = (resCh as NNChromosome).Hyperparameters;

            Assert.AreEqual(2, hp.InternalNeuronCounts.Length);
            Assert.AreEqual(4, hp.InternalNeuronCounts[0]);
            Assert.AreEqual(6, hp.InternalNeuronCounts[1]);
            Assert.AreEqual(2, hp.InternalActivationFunctions.Length);
            Assert.AreEqual(0, hp.InternalActivationFunctions[0].CompareTo(new Sigma()));
            Assert.AreEqual(0, hp.InternalActivationFunctions[1].CompareTo(new Sigma()));
        }

        [TestMethod]
        public void TestChangeLossFun()
        {
            PseudoRandom random = new PseudoRandom(4, 0.6, 1);
            var HypereparametersRange = Get(random);
            NNChromosome nnc = new NNChromosome(new Hyperparameters(3, 2, new int[] { 4, 6 }));

            NNBasicMutationOperator nnbmo = new NNBasicMutationOperator(HypereparametersRange);

            var resCh = nnbmo.Mutate(nnc, random);

            Hyperparameters hp = (resCh as NNChromosome).Hyperparameters;

            Assert.AreEqual(2, hp.InternalNeuronCounts.Length);
            Assert.AreEqual(4, hp.InternalNeuronCounts[0]);
            Assert.AreEqual(6, hp.InternalNeuronCounts[1]);
            Assert.AreEqual(2, hp.InternalActivationFunctions.Length);
            Assert.AreEqual(0, hp.LossFunction.CompareTo(new QuadDiff(0.5)));
        }

        [TestMethod]
        public void TestChangeGradStrat()
        {
            PseudoRandom random = new PseudoRandom(5, 0.6, 2);
            var HypereparametersRange = Get(random);
            NNChromosome nnc = new NNChromosome(new Hyperparameters(3, 2, new int[] { 4, 6 }));

            NNBasicMutationOperator nnbmo = new NNBasicMutationOperator(HypereparametersRange);

            var resCh = nnbmo.Mutate(nnc, random);

            Hyperparameters hp = (resCh as NNChromosome).Hyperparameters;

            Assert.AreEqual(2, hp.InternalNeuronCounts.Length);
            Assert.AreEqual(4, hp.InternalNeuronCounts[0]);
            Assert.AreEqual(6, hp.InternalNeuronCounts[1]);
            Assert.AreEqual(2, hp.InternalActivationFunctions.Length);
            Assert.AreEqual(0, hp.GradientStepStrategy.CompareTo(new ConstantGradientStep(0.001)));
        }


        //TODO - 0 - allow change of aggregate function
        HyperparameterRanges Get(Random random)
        {
            //TODO - A - GA add handling of chromosomes with negative fitness
            HyperparameterRanges HypereparametersRange = new HyperparameterRanges(3, 2);
            HypereparametersRange.InternalLayerCountDistribution = new DiscreteRange(new UniformDiscreteRangeDistribution(random, 1, 4));
            HypereparametersRange.NeuronCountDistribution = new DiscreteRange(new UniformDiscreteRangeDistribution(random, 1, 200));
            HypereparametersRange.InternalActFuncDist = new SetRange<ActivationFunction>(new ActivationFunction[] { new Relu(), new Sigma() }, new UniformDiscreteRangeDistribution(random, 0, 2));
            HypereparametersRange.LossFuncDist = new SetRange<LossFunction>(new LossFunction[] { new QuadDiff(), new QuadDiff(0.5), new CrossEntropy() }, new UniformDiscreteRangeDistribution(random, 0, 3));
            HypereparametersRange.GradStratDist = new SetRange<GradientStepStrategy>(new GradientStepStrategy[] { new ConstantGradientStep(0.01), new ConstantGradientStep(1), new ConstantGradientStep(0.001), new MomentumStrategy(0.001, 0.1), new MomentumStrategy(0.01, 0.1) }, new UniformDiscreteRangeDistribution(random, 0, 5));
            HypereparametersRange.AggregateFunction = new SetRange<ActivationFunction>(new ActivationFunction[] { new Sigma() }, new UniformDiscreteRangeDistribution(random, 0, 1));
            return HypereparametersRange;
        }
    }
}
