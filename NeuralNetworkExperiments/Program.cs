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
using GANN.MathAT;
using Accord.DataSets;
using GANN;

namespace NeuralNetworkExperiments
{
    class Program
    {
        //TODO - A - hyperparametes, range and distribution classes SUCKKKKKKK. Hyperparameters should have input output sizes too
        //TODO - A - make GANN class
        //TODO - A - make testing class
        static void Main(string[] args)
        {
            //MNIST m = new MNIST();

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

            TestGenerator.TTTTest();
            Logger.FlushClose();

        }
    }
}
