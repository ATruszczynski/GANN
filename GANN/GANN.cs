using GANN.GA;
using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.GA.Operators.CrossoverOperators;
using GANN.GA.Operators.MutationOperators;
using GANN.GA.ReplacementStrategies;
using GANN.GA.SamplingStrategies;
using GANN.MathAT;
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
        //TODO - 0 - debug cross entropy + softmax
        //TODO - A - add verbosity parameters to networks and GANN
        //TODO - B - don't recalculate fitnesses of unchanged networks
        //TODO - A - add analysing part of population in preelims
        public GeneticAlgorithm GeneticAlgorithm;
        //public double[][] TrainInput;
        //public double[][] TrainOutput;
        //public double[][] TestInput;
        //public double[][] TestOutput;
        public int stepSize = 2;
        int initizalEp = 2; //TODO - B - parametrise
        int initPop = 20;

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
            HypereparametersRange.InternalActFuncDist = new SetRange<ActivationFunction>(new ActivationFunction[] { new Relu() }, new UniformDiscreteRangeDistribution(random, 0, 1));
            HypereparametersRange.LossFuncDist = new SetRange<LossFunction>(new LossFunction[] { new QuadDiff(), new QuadDiff(0.5), new CrossEntropy() }, new UniformDiscreteRangeDistribution(random, 0, 3));
            HypereparametersRange.GradStratDist = new SetRange<GradientStepStrategy>(new GradientStepStrategy[] { new ConstantGradientStep(0.01), new ConstantGradientStep(1), new ConstantGradientStep(0.001), new MomentumStrategy(0.001, 0.1), new MomentumStrategy(0.01, 0.1) }, new UniformDiscreteRangeDistribution(random, 0, 5));
            HypereparametersRange.AggregateFunction = new SetRange<ActivationFunction>(new ActivationFunction[] { new Sigma()}, new UniformDiscreteRangeDistribution(random, 0, 1));

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
            nnff.epochs = 2;
            nnff.batches = 5 * trainOutput[0].Length;

            GeneticAlgorithm.FitnessFunction = nnff;

            GeneticAlgorithm.Iterations = 1;
        }
        //TODO - B - resturcutre unit tests
        public ANN GetGoodANN(int maxIt, double desiredScore, int initPopCount = 10)
        {
            //TODO - A - test
            ANN nn = null;

            var popHps = Preeliminaries(10 * initPopCount, initPopCount);

            GeneticAlgorithm.population = new Chromosome[initPopCount];

            for (int i = 0; i < popHps.Count; i++)
            {
                GeneticAlgorithm.population[i] = new NNChromosome(popHps[i]);
            }

            for (int i = 0; i < maxIt; i++)
            {
                Console.WriteLine("desu");
                GeneticAlgorithm.PopulationCount = initPopCount; //TODO - A - some mechanism for change
                (GeneticAlgorithm.FitnessFunction as NNFitnessFunc).epochs = (int)Math.Pow(initizalEp, i + 1);
                (double score, Chromosome ch) =  GeneticAlgorithm.Run(Random);
                nn = new ANN((ch as NNChromosome).Hyperparameters);
                if (score > desiredScore)
                    break;
            }

            return nn;
        }
        //TODO - B - additional parametrization (epoch, batches, etc.)
        public List<Hyperparameters> Preeliminaries(int toTest, int toRemain)
        {
            Console.WriteLine("Preelims started");
            //TODO - B - solve clunky object arguments and returns with templates?
            //TODO - A - test
            LimitedCapacitySortedList<Hyperparameters> hps = new LimitedCapacitySortedList<Hyperparameters>(toRemain);
            for (int i = 0; i < toTest; i++)
            {
                Hyperparameters hp = (Hyperparameters)HypereparametersRange.GetNext();
                var score = GeneticAlgorithm.FitnessFunction.ComputeFitness(new NNChromosome(hp));
                hps.Add(score, hp);
                Console.WriteLine((double)i / (toTest - 1));
            }

            return hps.ExtractList();
        }
    }
}
