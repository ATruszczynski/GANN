using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class CustomDiscreteDistribution : DiscreteDistribution
    {
        public double[] probs;
        public double[] values;

        public CustomDiscreteDistribution(Random random, double[] poss) : base(random)
        {
            probs = Utility.NormalisedCumulativeSum(poss);
        }

        public override double GetNext()
        {
            //TODO - B - test
            return values[Utility.Roulette(probs, Random)];
        }
    }
}
