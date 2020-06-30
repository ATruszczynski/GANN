using GANN.GA.FitnessFunctions;
using GANN.GA.GA_Elements;
using GANN.MathAT;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GANN.GA.SamplingStrategies
{
    public class RouletteSamplingStrategy : SamplingStrategy
    {
        double a;
        int maxDeg = -1;
        public RouletteSamplingStrategy(double aa = 0.01)
        {
            a = aa;
        }

        //TODO - C - could do something with calculating fitness multiple times?
        public override Chromosome Sample(Chromosome[] population, double[] fitnesses, Random random)
        {
            //TODO - C - validaiton
            Chromosome chosen = null;
            double[] fit = new double[fitnesses.Length];
            fit[0] = fitnesses[0] + a;
            for (int i = 1; i < fitnesses.Length; i++)
            {
                fit[i] += fit[i-1] + fitnesses[i] + a;
            }

            for (int i = 0; i < fit.Length; i++)
            {
                fit[i] /= fit[fit.Length - 1];
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

            int ind = Utility.Roulette(fit, random);
            //TODO - B - test
            return population[ind];
        }
    }
}
