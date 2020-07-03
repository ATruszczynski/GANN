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

            int ind = Utility.Roulette(fit, random);

            return population[ind];
        }
    }
}
