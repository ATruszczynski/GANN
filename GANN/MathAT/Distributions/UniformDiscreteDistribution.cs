using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class UniformDiscreteDistribution : DiscreteRangeDistribution
    {
        //TODO - C - names
        public UniformDiscreteDistribution(Random random, int minnInc, int maxEx): base(random, minnInc, maxEx)
        {
            Min = minnInc;
            Max = maxEx;
        }
        public override double GetNext()
        {
            //TODO - B - test
            return Random.Next(Min, Max);
        }
    }
}
