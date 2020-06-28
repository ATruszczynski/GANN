using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Distributions
{
    public abstract class ContinuousDistributon: Distribution
    {
        public ContinuousDistributon(Random random) : base(random) { }
    }
}
