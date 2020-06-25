using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.SamplingStrategies
{
    public class RouletteSamplingStrategy : SamplingStrategy
    {
        //TODO - C - could do something with calculating fitness multiple times?
        public override Chromosome Sample(Chromosome[] population, FitnessFunction fitnessFunction, Random random)
        {
            Chromosome chosen = null;

            double[] fitnesses = new double[population.Length];
            fitnesses[0] = fitnessFunction.ComputeFitness(population[0]);

            for (int i = 1; i < fitnesses.Length; i++)
            {
                fitnesses[i] = fitnesses[i - 1] + fitnessFunction.ComputeFitness(population[i]);
            }

            for (int i = 0; i < fitnesses.Length; i++)
            {
                fitnesses[i] /= fitnesses[fitnesses.Length - 1];
            }

            double p = random.NextDouble();

            int cind = -1;
            while(cind + 1 < fitnesses.Length && fitnesses[cind + 1] <= p)
            {
                cind++;
            }

            return chosen;
        }
    }
}
