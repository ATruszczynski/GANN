using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class DiscreteDistribution : Distribution
    {
        public double[] probs;
        public double[] values;

        public DiscreteDistribution(double[] poss)
        {
            probs = Utility.NormalisedCumulativeSum(poss);
        }

        public override double GetNext(Random random)
        {
            //TODO - B - test
            return values[Utility.Roulette(probs, random)];
        }
    }
}
