using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class UniformDiscreteRangeDistribution : DiscreteRangeDistribution
    {
        //TODO - C - names
        public UniformDiscreteRangeDistribution(Random random, int minnInc, int maxEx): base(random, minnInc, maxEx)
        {
            Min = minnInc;
            Max = maxEx;
        }
        public override double GetNext()
        {
            return Random.Next(Min, Max);
        }
    }
}
