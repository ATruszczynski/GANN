using GANN.MathAT.Distributions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GANN.MathAT.Ranges
{
    public abstract class Range
    {
        public Distribution GeneratingDistribution;
        public Range(Distribution dist)
        {
            GeneratingDistribution = dist;
        }
    }
}
