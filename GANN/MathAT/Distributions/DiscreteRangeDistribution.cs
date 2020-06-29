using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public abstract class DiscreteRangeDistribution: DiscreteDistribution
    {
        public int Min;
        public int Max;
        public DiscreteRangeDistribution(Random random, int min, int max) : base(random)
        {
            Min = min;
            Max = max;
        }
    }
}
