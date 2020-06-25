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

namespace NeuralNetworkExperiments
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestGenerator.TestScenario1();

            GeneticAlgorithm ga = new GeneticAlgorithm();
            ga.CrossoverOperator = new SinglePointForBinaryCrossoverOperator();
            ga.MutationOperator = new SinglePointBinaryMutation();
            ga.SamplingStrategy = new RouletteSamplingStrategy();
            ga.ReplacementStrategy = new GenerationalReplacementStrategy();
            ga.FitnessFunction = new CustomFitnessFunction
                (
                    (c, arr) => 
                    {
                        BinaryChromosome b = (BinaryChromosome)c;
                        double sum = 0;
                        for (int i = 0; i < b.Array.Length; i++)
                        {
                            if (i % 2 == 0 && b.Array[i] == true)
                                sum++;
                            if (i % 2 == 1 && b.Array[i] == false)
                                sum++;
                        }
                        return sum; 
                    }
                );

            int pop = 100;
            int len = 20;
            Random random = new Random(1001);
            ga.population = new Chromosome[pop];
            for (int i = 0; i < pop; i++)
            {
                bool[] array = new bool[len];
                for (int l = 0; l < len; l++)
                {
                    int r = random.Next(2);
                    if (r == 1)
                        array[l] = true;
                }
                ga.population[i] = new BinaryChromosome(array);
            }

            ga.Iterations = 100;

            Console.WriteLine(ga.Run(1001).ToString());
        }
    }
}
