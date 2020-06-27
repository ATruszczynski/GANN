using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.GA.SamplingStrategies
{
    public class RouletteSamplingStrategy : SamplingStrategy
    {
        double a;
        public RouletteSamplingStrategy(double aa = 0.01)
        {
            a = aa;
        }
        //TODO - C - could do something with calculating fitness multiple times?
        public override Chromosome Sample(Chromosome[] population, FitnessFunction fitnessFunction, Random random)
        {
            Chromosome chosen = null;

            double[] fitnesses = new double[population.Length];
            fitnesses[0] = fitnessFunction.ComputeFitness(population[0]) + a;

            for (int i = 1; i < fitnesses.Length; i++)
            {
                //TODO - B - correct adding a?
                fitnesses[i] = fitnesses[i - 1] + fitnessFunction.ComputeFitness(population[i]) + a;
            }

            for (int i = 0; i < fitnesses.Length; i++)
            {
                fitnesses[i] /= fitnesses[fitnesses.Length - 1];
            }

            //double p = random.NextDouble();

            ////while(cind + 1 < fitnesses.Length && fitnesses[cind + 1] <= p)
            ////{
            ////    cind++;
            ////}

            ////for(cind = population.Length - 1; cind >= 0; cind--)
            ////{
            ////    if (p <= fitnesses[cind])
            ////        break;
            ////}
            ////TODO - B - works with only 0 in fitnesses?

            ////TODO - B - test for last one having 0 prob
            //int ind = 0;
            //for(ind = 0; ind < population.Length; ind++)
            //{
            //    if (p < fitnesses[ind])
            //        break;
            //}

            int ind = Utility.Roulette(fitnesses, random);

            return population[ind];
        }
    }
}
