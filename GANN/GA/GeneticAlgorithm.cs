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
    public class GeneticAlgorithm
    {
        //TODO - B - enforce all paramaters present
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

        public Chromosome Run(int seed)
        {
            random = new Random(seed);

            Chromosome[] newPopulation = new Chromosome[PopulationCount];
            for (int iter = 0; iter < Iterations; iter++)
            {
                for (int pop = 0; pop < PopulationCount; pop++)
                {
                    Chromosome chosen = SamplingStrategy.Sample(population, FitnessFunction, random).DeepCopy();

                    chosen = MaybeMutate(chosen);
                    bool did = MaybeCrossover(chosen, out Chromosome[] crossRes);

                    newPopulation[pop] = crossRes[0];

                    if(did && pop < PopulationCount - 1)
                    {
                        pop++;
                        newPopulation[pop] = crossRes[1];
                    }
                }

                population = ReplacementStrategy.Replace(population, newPopulation);
            }

            double maxF = double.MinValue;
            Chromosome maxC = null;

            for (int i = 0; i < population.Length; i++)
            {
                //TODO - B - Remove args from fitness and any other where it makes no sense
                double f = FitnessFunction.ComputeFitness(population[i]);
                if(f > maxF)
                {
                    maxF = f;
                    maxC = population[i];
                }
            }

            return maxC;
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

        public bool MaybeCrossover(Chromosome c1, out Chromosome[] result)
        {
            bool did = false;

            result = new Chromosome[] { c1 };

            double pc = random.NextDouble();
            
            if(pc <= crossoverProbability)
            {
                did = true;
                Chromosome c2 = SamplingStrategy.Sample(population, FitnessFunction, random);
                c2 = MaybeMutate(c2);

                (c1, c2) = CrossoverOperator.Crossover(c1, c2, random);
                result = new Chromosome[] { c1, c2 };
            }

            return did;
        }
    }
}
