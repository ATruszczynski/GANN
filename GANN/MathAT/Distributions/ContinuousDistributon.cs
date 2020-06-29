using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    //TODO - A - remove hierarchy of distributions. It doesn't work logically
    public abstract class ContinuousDistributon: Distribution
    {
        public ContinuousDistributon(Random random) : base(random) { }
    }
}
