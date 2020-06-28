using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public abstract class DiscreteDistribution: Distribution
    {
        public DiscreteDistribution(Random random) : base(random) { }
    }
}
