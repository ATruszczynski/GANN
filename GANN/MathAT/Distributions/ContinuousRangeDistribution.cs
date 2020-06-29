using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public abstract class ContinuousRangeDistribution: ContinuousDistributon
    {
        public double Min;
        public double Max;
        public ContinuousRangeDistribution(Random random, double min, double max): base(random)
        {
            Min = min;
            Max = max;
        }
    }
}
