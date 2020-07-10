using GANN.GA;
using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.GA.Operators.CrossoverOperators;
using GANN.GA.Operators.MutationOperators;
using GANN.GA.ReplacementStrategies;
using GANN.GA.SamplingStrategies;
using GANN.MathAT.Distributions;
using GANN.MathAT.Ranges;
using GANN.NN;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.LossFunctions;
using GANN.NN.ParameterRanges;
using GANN.NN.Parameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN
{
    class GANN
    {
        public GeneticAlgorithm GeneticAlgorithm;
        //public double[][] TrainInput;
        //public double[][] TrainOutput;
        //public double[][] TestInput;
        //public double[][] TestOutput;
        public int stepSize = 2;

        public HyperparameterRanges HypereparametersRange;

        Random Random;

        public GANN(double[][] trainInput, double[][] trainOutput, double[][] testInput, double[][] testOutput, Random random = null)
        {
            if (random == null)
                Random = new Random();
            else
                Random = random;
            HypereparametersRange = new HyperparameterRanges(trainInput[0].Length, trainOutput[0].Length);
            HypereparametersRange.InternalLayerCountDistribution = new DiscreteRange(new UniformDiscreteRangeDistribution(random, 0, 2));
            HypereparametersRange.NeuronCountDistribution = new DiscreteRange(new UniformDiscreteRangeDistribution(random, 1, 200));
            HypereparametersRange.ActFuncDist = new SetRange<ActivationFunction>(new ActivationFunction[] { new Relu() }, new UniformDiscreteRangeDistribution(random, 0, 1));
            HypereparametersRange.LossFuncDist = new SetRange<LossFunction>(new LossFunction[] { new QuadDiff(), new QuadDiff(0.5), new CrossEntropy() }, new UniformDiscreteRangeDistribution(random, 0, 3));
            HypereparametersRange.GradStratDist = new SetRange<GradientStepStrategy>(new GradientStepStrategy[] { new ConstantGradientStep(0.01), new ConstantGradientStep(1), new ConstantGradientStep(0.001), new MomentumStrategy(0.001, 0.1), new MomentumStrategy(0.01, 0.1) }, new UniformDiscreteRangeDistribution(random, 0, 5));
            HypereparametersRange.outputAct = new Sigma();

            GeneticAlgorithm = new GeneticAlgorithm();

            GeneticAlgorithm.CrossoverOperator = new NNBasicCrossoverOperator();

            GeneticAlgorithm.MutationOperator = new NNBasicMutationOperator(HypereparametersRange);
            GeneticAlgorithm.SamplingStrategy = new RouletteSamplingStrategy();
            GeneticAlgorithm.ReplacementStrategy = new GenerationalReplacementStrategy();

            NNFitnessFunc nnff = new NNFitnessFunc();
            nnff.trainInputs = trainInput;
            nnff.trainOutputs = trainOutput;
            nnff.testInputs = testInput;
            nnff.testOutputs = testOutput;
            nnff.epochs = 100;

            GeneticAlgorithm.FitnessFunction = nnff;

            GeneticAlgorithm.Iterations = 1;
        }

        public ANN GetGoodNN(int maxIt, double desiredScore, int popCount = 10)
        {
            NNChromosome[] population = new NNChromosome[popCount];
            GeneticAlgorithm.PopulationCount = popCount;
            for (int i = 0; i < popCount; i++)
            {
                population[i] = new NNChromosome((Hyperparameters)HypereparametersRange.GetNext());
            }
            GeneticAlgorithm.population = population;

            Hyperparameters nn = null;
            int it = 0;
            double score = -1;
            while(it < maxIt && score < desiredScore)
            {
                Chromosome cnn = null;
                (score, cnn) =  GeneticAlgorithm.Run(Random);
                nn = (cnn as NNChromosome).Hyperparameters;
                it++;
            }
            return new ANN(nn);
        }
    }
}
