using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.GA.Operators.CrossoverOperators;
using GANN.GA.Operators.MutationOperators;
using GANN.GA.ReplacementStrategies;
using GANN.GA.SamplingStrategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA
{
    public abstract class GeneticAlgorithm
    {
        public double crossoverProbability = 0.5;
        public double mutationProbability = 1;

        public CrossoverOperator CrossoverOperator;
        public MutationOperator MutationOperator;
        public SamplingStrategy SamplingStrategy;
        public ReplacementStrategy ReplacementStrategy;
        public FitnessFunction FitnessFunction;

        public Chromosome[] population;
        public int PopulationCount { get => population.Length;  }
        public int Iterations;

        public Random random;

        public void Run(int seed)
        {
            random = new Random(seed);

            Chromosome[] newPopulation = new Chromosome[PopulationCount];
            for (int iter = 0; iter < Iterations; iter++)
            {
                for (int pop = 0; pop < PopulationCount; pop++)
                {
                    Chromosome chosen = SamplingStrategy.Sample(population, FitnessFunction);

                    chosen = MaybeMutate(chosen);
                    chosen = MaybeCrossover(chosen);

                    newPopulation[iter] = chosen;
                }

                population = ReplacementStrategy.Replace(population, newPopulation);
            }
        }

        public Chromosome MaybeMutate(Chromosome c)
        {
            double pm = random.NextDouble();

            if(pm <= mutationProbability)
            {
                c = MutationOperator.Mutate(c);
            }

            return c;
        }

        public Chromosome MaybeCrossover(Chromosome c1)
        {
            Chromosome result = c1;

            double pc = random.NextDouble();
            
            if(pc <= crossoverProbability)
            {
                Chromosome c2 = SamplingStrategy.Sample(population, FitnessFunction);
                c2 = MaybeMutate(c2);

                result = CrossoverOperator.Crossover(c1, c2);
            }

            return result;
        }
    }
}
