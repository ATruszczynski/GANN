using GANN.GA;
using GANN.NN;
using System;
using static GANN.MathAT.ActFuns;
using GANN.GA.Operators.CrossoverOperators;
using GANN.GA.Operators.MutationOperators;
using GANN.GA.SamplingStrategies;
using GANN.GA.ReplacementStrategies;
using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.NN.ParameterRanges;
using GANN.MathAT.Ranges;
using GANN.MathAT.Distributions;
using GANN.NN.LossFunctions;
using GANN.NN.ActivationFunctions;
using GANN.NN.GradientStepStrategies;
using GANN.NN.Parameters;

namespace NeuralNetworkExperiments
{
    class Program
    {
        //TODO - A - hyperparametes, range and distribution classes SUCKKKKKKK
        //TODO - A - make GANN class
        //TODO - A - make testing class
        static void Main(string[] args)
        {
            //TestGenerator.TestScenario1();

            //GeneticAlgorithm ga = new GeneticAlgorithm();
            //ga.CrossoverOperator = new SinglePointForBinaryCrossoverOperator();
            //ga.MutationOperator = new SinglePointBinaryMutation();
            //ga.SamplingStrategy = new RouletteSamplingStrategy();
            //ga.ReplacementStrategy = new GenerationalReplacementStrategy();
            //ga.FitnessFunction = new InterchangableBinaryFF();

            //int pop = 100;
            //int len = 20;
            //Random random = new Random(1001);
            //ga.population = new Chromosome[pop];
            //for (int i = 0; i < pop; i++)
            //{
            //    bool[] array = new bool[len];
            //    for (int l = 0; l < len; l++)
            //    {
            //        int r = random.Next(2);
            //        if (r == 1)
            //            array[l] = true;
            //    }
            //    ga.population[i] = new BinaryChromosome(array);
            //}

            //ga.crossoverProbability = 0.9;
            //ga.mutationProbability = 0.05;

            //ga.Iterations = 50;

            //Console.WriteLine(ga.Run(1001).ToString());

            //foreach (var c in ga.population)
            //{
            //    Console.WriteLine(c.ToString());
            //}

            (var trainInput, var trainOutput) = TestGenerator.TTT(1000);
            (var testInput, var testOutput) = TestGenerator.TTT(100);

            Random random = new Random(1001);

            GeneticAlgorithm ga = new GeneticAlgorithm();
            ga.CrossoverOperator = new NNBasicCrossoverOperator();

            var hp = new HyperparameterRanges();
            hp.WeightDistribution = new ContinuousRange(new UniformContinuousDistribution(random, -1, 1));
            hp.StdDistribution = new ContinuousRange(new UniformContinuousDistribution(random, -1, 1));
            hp.InternalLayerCountDistribution = new DiscreteRange(new UniformDiscreteDistribution(random, 1, 5));
            hp.NeuronCountDistribution = new DiscreteRange(new UniformDiscreteDistribution(random, 1, 20));
            hp.ActFuncDist = new SetRange<ActivationFunction>(new ActivationFunction[] { new Relu() }, new UniformDiscreteDistribution(random, 0, 1));
            hp.LossFuncDist = new SetRange<LossFunction>(new LossFunction[] { new QuadDiff() }, new UniformDiscreteDistribution(random, 0, 1));
            hp.GradStratDist = new SetRange<GradientStepStrategy>(new GradientStepStrategy[] { new ConstantGradientStep(0.5), new ConstantGradientStep(1) }, new UniformDiscreteDistribution(random, 0, 2));

            ga.MutationOperator = new NNBasicMutationOperator(hp);
            ga.SamplingStrategy = new RouletteSamplingStrategy();
            ga.ReplacementStrategy = new GenerationalReplacementStrategy();

            NNFitnessFunc nnff = new NNFitnessFunc();
            nnff.trainInputs = trainInput;
            nnff.trainOutputs = trainOutput;
            nnff.testInputs = trainInput;
            nnff.testOutputs = trainOutput;
            ga.FitnessFunction = new NNFitnessFunc();

            ga.FitnessFunction = nnff;

            int pop = 10;
            int len = 20;
            ga.population = new NNChromosome[pop];
            for (int i = 0; i < pop; i++)
            {
                Hyperparameters param = hp.GetRandomHyperparameters(); 

                ga.population[i] = new NNChromosome(new ANN(param, random));
            }

            ga.crossoverProbability = 0.25;
            ga.mutationProbability = 0.5;

            ga.Iterations = 5;

            Console.WriteLine(ga.FitnessFunction.ComputeFitness(ga.Run(random)));
        }
    }
}
