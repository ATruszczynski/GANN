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
        public UniformDiscreteDistribution(Random random, int minnInc, int maxEx): base(random)
        {
            minInc = minnInc;
            this.maxEx = maxEx;
        }
        public override double GetNext()
        {
            //TODO - B - test
            return Random.Next(minInc, maxEx);
        }
    }
}
