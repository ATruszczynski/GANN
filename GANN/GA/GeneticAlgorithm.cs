using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.GA.Operators.CrossoverOperators;
using GANN.GA.Operators.MutationOperators;
using GANN.GA.ReplacementStrategies;
using GANN.GA.SamplingStrategies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GANN.GA
{
    public class GeneticAlgorithm
    {
        //TODO - B - implement reset
        //TODO - A - test multiple runs
        //TODO - B - enforce all paramaters present
        public double crossoverProbability = 0.5;
        public double mutationProbability = 1;

        public CrossoverOperator CrossoverOperator;
        public MutationOperator MutationOperator;
        public SamplingStrategy SamplingStrategy;
        public ReplacementStrategy ReplacementStrategy;
        public FitnessFunction FitnessFunction;

        public Chromosome[] population;
        public int? PopulationCount;
        public int Iterations;

        public double BestScore = double.MinValue;
        public Chromosome BestSolution;
        object bestLock = new object();
        public int maxDeg = 1;
        //TODO - B - parallel control?
        public Random random;
        double[] fitnesses;

        public (double, Chromosome) Run(Random rrandom, bool reset = true)
        {
            random = rrandom;

            var tmpPopulation = new Chromosome[PopulationCount.Value];

            for (int i = 0; i < PopulationCount.Value; i++)
            {
                tmpPopulation[i] = population[i].DeepCopy();
            }
            population = tmpPopulation;

            for (int iter = 0; iter < Iterations; iter++)
            {
                CalculateFitnessesOfCurrentPopulationAndSaveBest();
                //TODO - B -  remove superfluous DeepCopies

                Chromosome[] newPopulation = new Chromosome[PopulationCount.Value];

                for (int pop = 0; pop < PopulationCount; pop++)
                {
                    Chromosome chosen = SamplingStrategy.Sample(population, fitnesses, random).DeepCopy();

                    chosen = MaybeMutate(chosen);
                    bool did = MaybeCrossover(chosen, fitnesses, out Chromosome[] crossRes);

                    newPopulation[pop] = crossRes[0];

                    if(did && pop < PopulationCount - 1)
                    {
                        pop++;
                        newPopulation[pop] = crossRes[1];
                    }
                }

                population = ReplacementStrategy.Replace(population, newPopulation);
            }

            CalculateFitnessesOfCurrentPopulationAndSaveBest();

            return (BestScore, BestSolution);
        }
        public Chromosome MaybeMutate(Chromosome c)
        {
            double pm = random.NextDouble();

            if(pm <= mutationProbability)
            {
                c = MutationOperator.Mutate(c, random);
            }

            return c;
        }
        //TODO - C - kinda awkward
        public bool MaybeCrossover(Chromosome c1, double[] fitnesses, out Chromosome[] result)
        {
            bool did = false;

            result = new Chromosome[] { c1 };

            double pc = random.NextDouble();
            
            if(pc <= crossoverProbability)
            {
                did = true;
                Chromosome c2 = SamplingStrategy.Sample(population, fitnesses, random).DeepCopy();
                c2 = MaybeMutate(c2);

                (c1, c2) = CrossoverOperator.Crossover(c1, c2, random);
                result = new Chromosome[] { c1, c2 };
            }

            return did;
        }

        void CalculateFitnessesOfCurrentPopulationAndSaveBest()
        {
            fitnesses = new double[PopulationCount.Value];

            Parallel.For(0, PopulationCount.Value, new ParallelOptions { MaxDegreeOfParallelism = maxDeg }, i =>
            {
                fitnesses[i] = FitnessFunction.ComputeFitness(population[i]);
                lock (bestLock)
                {
                    if (fitnesses[i] > BestScore)
                    {
                        BestScore = fitnesses[i];
                        BestSolution = population[i];
                    }
                }
            });
        }
    }
}
