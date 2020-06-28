using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public class UniformDiscreteDistribution : DiscreteDistribution
    {
        //TODO - B - discrete distributions are quite differently madae from each other
        public int minInc;
        public int maxEx;
        public UniformDiscreteDistribution(int minnInc, int maxEx)
        {
            minInc = minnInc;
            this.maxEx = maxEx;
        }
        public override double GetNext(Random random)
        {
            //TODO - B - test
            return random.Next(minInc, maxEx);
        }
    }
}
