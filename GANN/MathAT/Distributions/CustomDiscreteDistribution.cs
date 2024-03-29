﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class CustomDiscreteDistribution : DiscreteDistribution
    {
        public double[] probs;
        public double[] values;

        public CustomDiscreteDistribution(Random random, double[] valuess, double[] poss) : base(random)
        {
            probs = Utility.NormalisedCumulativeSum(poss);
            values = valuess;
        }

        public override double GetNext()
        {
            return values[Utility.Roulette(probs, Random)];
        }
    }
}
